#define WIN32_LEAN_AND_MEAN
#define DEFAULT_PORT "27016"
#define DEFAULT_BUFLEN 512
#define MAX_USERS 256

#include <iostream>
#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <iphlpapi.h>
#include <conio.h>
#include <vector>

#pragma comment(lib, "Ws2_32.lib")

using namespace std;

struct addrinfo * result = NULL, * ptr = NULL, hints;
int iResult;
WSADATA wsaData;

int currIndex = 0;
int userCount = 0;

struct user {
	SOCKET userSocket;
	char userName[32];
	int index;
};

user users[MAX_USERS];

bool serverOn = true;

CRITICAL_SECTION csUsers;

void SendToUsers(char buf[DEFAULT_BUFLEN]) {
	EnterCriticalSection(&csUsers);

	for (int i = 0; i < userCount; i++) {
		if (users[i].userSocket != INVALID_SOCKET) {
			send(users[i].userSocket, buf, (int)strlen(buf), 0);
		}
	}	LeaveCriticalSection(&csUsers);

}

DWORD WINAPI ClientHandler(LPVOID lpParam) {
	int userIndex = (int)(intptr_t)lpParam;  

	while (serverOn) {
		SOCKET sock = INVALID_SOCKET;
		char username[32] = "";

		EnterCriticalSection(&csUsers);
		for (int i = 0; i < userCount; i++) {
			if (users[i].index == userIndex) {
				sock = users[i].userSocket;
				strcpy_s(username, sizeof(username), users[i].userName);
				break;
			}
		}
		LeaveCriticalSection(&csUsers);

		if (sock == INVALID_SOCKET) 
			break;

		char buf[DEFAULT_BUFLEN];
		int iResult = recv(sock, buf, DEFAULT_BUFLEN - 1, 0);

		if (iResult > 0) {
			buf[iResult] = '\0';

			if (strcmp(buf, "/users") == 0) {
				char userList[DEFAULT_BUFLEN] = "Active users: ";

				EnterCriticalSection(&csUsers);
				for (int i = 0; i < userCount; i++) {
					if (users[i].userSocket != INVALID_SOCKET) {
						strcat_s(userList, sizeof(userList) - strlen(userList) - 1, users[i].userName);
						if (i < userCount - 1)
							strcat_s(userList, sizeof(userList) - strlen(userList) - 1, ", ");
					}
				}
				LeaveCriticalSection(&csUsers);

				send(sock, userList, (int)strlen(userList), 0);
			}
			else {
				cout << buf << endl;
				SendToUsers(buf);  
			}
		}
		else {
			break;
		}
	}

	EnterCriticalSection(&csUsers);
	for (int i = 0; i < userCount; i++) {
		if (users[i].index == userIndex) {
			cout << "Клиент отключен: " << users[i].userName << endl;

			char msg[DEFAULT_BUFLEN];
			_snprintf_s(msg, DEFAULT_BUFLEN, _TRUNCATE,
				"[Server]: user %.30s left the chat", users[i].userName);

			SendToUsers(msg);

			closesocket(users[i].userSocket);
			users[i].userSocket = INVALID_SOCKET;

			for (int j = i; j < userCount - 1; j++) {
				users[j] = users[j + 1];
			}
			userCount--;
			break;
		}
	}	LeaveCriticalSection(&csUsers);

	return 0;
}
int main()
{
	setlocale(0, "rus");

	InitializeCriticalSection(&csUsers);

	printf("Инициализация сокета...\n");

	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0) {
		printf("WSAStartup failed: %d\n", iResult);
		return 1;
	}
	printf("Создание сокета...\n");

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	hints.ai_flags = AI_PASSIVE;

	iResult = getaddrinfo(NULL, DEFAULT_PORT, &hints, &result);

	if (iResult != 0) {
		printf("getaddrinfo failed: %d\n", iResult);
		WSACleanup();
		return 1;
	}

	SOCKET ListenSocket = INVALID_SOCKET;

	ListenSocket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);
	if (ListenSocket == INVALID_SOCKET) {
		printf("Error at socket(): %ld\n", WSAGetLastError());
		freeaddrinfo(result);
		WSACleanup();
		return 1;
	}
	printf("Привязка сокета...\n");

	iResult = bind(ListenSocket, result->ai_addr, (int)result->ai_addrlen);
	if (iResult == SOCKET_ERROR) {
		printf("bind failed: %d\n", WSAGetLastError());
		freeaddrinfo(result);

		closesocket(ListenSocket);
		WSACleanup();

		return 1;
	}
	freeaddrinfo(result);
	printf("Сокет слушается!\n");

	if (listen(ListenSocket, SOMAXCONN) == SOCKET_ERROR) {
		printf("Listen failed: %ld\n", WSAGetLastError());
		closesocket(ListenSocket);
		WSACleanup();
		return 1;
	}



	while (true) {
		if (_kbhit()) {
			_getch();
			serverOn = false;
			break;

		}
		

		SOCKET ClientSocket;
		char buf[DEFAULT_BUFLEN];

		ClientSocket = accept(ListenSocket, NULL, NULL);
		if (ClientSocket == INVALID_SOCKET) {
			printf("accept failed: %d\n", WSAGetLastError());
			closesocket(ListenSocket);
			WSACleanup();
			return 1;
		}
		iResult = recv(ClientSocket, buf, DEFAULT_BUFLEN - 1, 0);
		if (iResult > 0) {
			buf[iResult] = '\0';
		}

		cout << "Клиент " << buf << " подключен" << endl;

		char msg[DEFAULT_BUFLEN];
		_snprintf_s(msg, DEFAULT_BUFLEN, _TRUNCATE, "[Server]: user %s has joined", buf);
		SendToUsers(msg);
		user currUser = {};
		currUser.userSocket = ClientSocket;
		currUser.index = currIndex;
		strcpy_s(currUser.userName, buf);

		EnterCriticalSection(&csUsers);
		if (userCount < MAX_USERS) {
			users[userCount].userSocket = ClientSocket;
			users[userCount].index = userCount;
			strcpy_s(users[userCount].userName, sizeof(users[userCount].userName), buf);

			DWORD idThread;
			HANDLE hThread = CreateThread(NULL, 0, ClientHandler,
				(LPVOID)(intptr_t)users[userCount].index, 0, &idThread);

			if (hThread) CloseHandle(hThread);

			userCount++;
		}
		LeaveCriticalSection(&csUsers);

		DWORD idThread;
		HANDLE hThread = CreateThread(NULL, 0, ClientHandler, (LPVOID)(intptr_t)currIndex, 0, &idThread);
		currIndex++;
		CloseHandle(hThread);
	}
	closesocket(ListenSocket);
	DeleteCriticalSection(&csUsers);
	return 0;
}


