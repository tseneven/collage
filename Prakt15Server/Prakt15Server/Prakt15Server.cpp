#define WIN32_LEAN_AND_MEAN
#define DEFAULT_PORT "27016"
#include <iostream>
#include <windows.h>
#include <winsock2.h>
#include <ws2tcpip.h>
#include <iphlpapi.h>
#include <conio.h>

#pragma comment(lib, "Ws2_32.lib")

using namespace std;

struct addrinfo * result = NULL, * ptr = NULL, hints;
int iResult;
WSADATA wsaData;

int main()
{
	setlocale(0, "rus");
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
		if (_getch()) {
			break;
		}
		else {
			continue;
		}

		SOCKET ClientSocket;

		ClientSocket = accept(ListenSocket, NULL, NULL);
		if (ClientSocket == INVALID_SOCKET) {
			printf("accept failed: %d\n", WSAGetLastError());
			closesocket(ListenSocket);
			WSACleanup();
			return 1;
		}
		printf("Клиент подключен!\n");

		const char* ping = "ping";

		while (iResult = send(ClientSocket, ping, (int)strlen(ping), 0) != SOCKET_ERROR) {
			cout << "Клиент подключен" << endl;
			Sleep(1000);
		}


	}
	closesocket(ListenSocket);
	return 0;
}
