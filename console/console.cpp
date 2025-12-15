#include <iostream>
#include<Windows.h>
#include <conio.h>

using namespace std;

int main(int args, char *argv[])
{
	cout << "s - stop" << endl; 
	cout << "r - start" << endl;
	HANDLE hThread;
	char c;
	hThread = (HANDLE)atoi(argv[1]);
	while (true) {
		cin >> c;
		if (c == 's') {
			SuspendThread(hThread);
		}
		if (c == 'r') {
			ResumeThread(hThread);
		}
	}
	TerminateThread(hThread, 0);
	CloseHandle(hThread);
	_getch();
	return 0;
}

