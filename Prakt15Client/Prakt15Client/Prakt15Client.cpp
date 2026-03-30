#define WIN32_LEAN_AND_MEAN
#define DEFAULT_PORT "27016"
#define DEFAULT_BUFLEN 512
#define DEFAULT_IP "26.16.158.224"

#include <iostream>
#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <iphlpapi.h>

#pragma comment(lib, "Ws2_32.lib")

using namespace std;

struct addrinfo* result = NULL, * ptr = NULL, hints;
int iResult;
WSADATA wsaData;
char nickname[255];
char display[255];

HANDLE recvServerThread;
DWORD idRecvServerThread;

HANDLE cooldownServerThread;
DWORD idCooldownServerThread;


SOCKET ConnectSocket = INVALID_SOCKET;

bool serverOn = false;

bool isCooldown = false;

DWORD WINAPI Cooldown(LPVOID) {
	while (serverOn) {
		if (isCooldown) {
			Sleep(2000);
			isCooldown = false;
		}
	}
	return 0;
}



DWORD WINAPI RecvServer(LPVOID) {
		char recvbuf[DEFAULT_BUFLEN];
		do {
			iResult = recv(ConnectSocket, recvbuf, DEFAULT_BUFLEN - 1, 0);
			if (iResult > 0) {
				recvbuf[iResult] = '\0';
				if (strcmp(recvbuf, "ping") != 0 /*&& strcmp(recvbuf, display)*/) {
					printf("%s\n", recvbuf);
				}
			}
			else if (iResult == 0)
				printf("Connection closed\n");
			else
				printf("recv failed: %d\n", WSAGetLastError());
		} while (iResult > 0 && serverOn);

	
	return 0;
}

int main()
{
	setlocale(0, "rus");
	
	printf("Введите ник\n");

	cin >> nickname;

	printf("Инициализация сокета...\n");

	iResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (iResult != 0) {
		printf("WSAStartup failed: %d\n", iResult);
		return 1;
	}
	printf("Создание сокета...\n");

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	iResult = getaddrinfo(DEFAULT_IP, DEFAULT_PORT, &hints, &result);
	if (iResult != 0) {
		printf("getaddrinfo failed: %d\n", iResult);
		WSACleanup();
		return 1;
	}


	ptr = result;

	ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype, ptr->ai_protocol);
	if (ConnectSocket == INVALID_SOCKET) {
		printf("Error at socket(): %ld\n", WSAGetLastError());
		freeaddrinfo(result);
		WSACleanup();
		return 1;
	}

	iResult = connect(ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
	if (iResult == SOCKET_ERROR) {
		printf("connect failed: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		return 1;
	}

	serverOn = true;

	iResult = send(ConnectSocket, nickname, (int)strlen(nickname), 0);
	if (iResult == SOCKET_ERROR) {
		printf("send failed: %d\n", WSAGetLastError());
		closesocket(ConnectSocket);
		WSACleanup();
		return 1;
	}
	printf("Bytes Send: %ld\n", iResult);

	recvServerThread = CreateThread(NULL, 0, RecvServer, NULL, 0, &idRecvServerThread);
	cooldownServerThread = CreateThread(NULL, 0, Cooldown, NULL, 0, &idCooldownServerThread);


	char sendbuf[DEFAULT_BUFLEN];

	while (true) {
		cin >> sendbuf;

		if (sendbuf[0] == '/') {
			if (strcmp(sendbuf, "/exit") == 0) {
				serverOn = false;
				shutdown(ConnectSocket, SD_SEND);
				WaitForSingleObject(recvServerThread, INFINITE); 
				closesocket(ConnectSocket);
				WSACleanup();
				break;
			}
			else if (strcmp(sendbuf, "/users") == 0) {
				send(ConnectSocket, sendbuf, (int)strlen(sendbuf), 0);
				continue;
			}
			else {
				cout << "Неизвестная команда" << endl;
				continue;
			}
		}

		strcpy_s(display, "[");      
		strcat_s(display, nickname);  
		strcat_s(display, "]: ");      
		strcat_s(display, sendbuf);
		
		if (!isCooldown) {
			iResult = send(ConnectSocket, display, (int)strlen(display), 0);
			if (iResult == SOCKET_ERROR) {
				printf("send failed: %d\n", WSAGetLastError());
				break;
			}
			isCooldown = true;
		}
		else {
			cout << "Не спамь!" << endl;
		}
	}

	WaitForSingleObject(recvServerThread, INFINITE);

	closesocket(ConnectSocket);
	WSACleanup();
	CloseHandle(recvServerThread);
	CloseHandle(cooldownServerThread);

	return 0;
}
