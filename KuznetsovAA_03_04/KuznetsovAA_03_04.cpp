
#include <iostream>
#include <Windows.h>

using namespace std;

HANDLE hEvents[2];

int countEx = 0;

HANDLE hThreads[3];

DWORD idThreads[3];

bool isPause = false;

DWORD WINAPI counter(LPVOID) {
	while (countEx < 100) {
		countEx++;
		Sleep(1000);
	}
	return 0;
}

DWORD WINAPI hThreadEventOne(LPVOID) {
	while (countEx < 100) {
		WaitForSingleObject(hEvents[0], INFINITE);
		if (isPause) {
			ResumeThread(hThreads[0]);
			isPause = false;
		}
		else {
			SuspendThread(hThreads[0]);
			isPause = true;
		}
		ResetEvent(hEvents[0]);
	}
	return 0;
}

DWORD WINAPI hThreadEventTwo(LPVOID) {
	while (countEx < 100) {
		WaitForSingleObject(hEvents[1], INFINITE);
		cout << countEx << endl;
		ResetEvent(hEvents[1]);
	}
	return 0;
}

int main()
{
	hEvents[0] = CreateEvent(NULL, FALSE, FALSE, L"EventOneExamB6");
	hEvents[1] = CreateEvent(NULL, FALSE, FALSE, L"EventTwoExamB6");

	if (hEvents[0] == NULL) {
		cout << GetLastError() << endl;
		return 0;
	}

	if (hEvents[1] == NULL) {
		cout << GetLastError() << endl;
		return 0;
	}

	hThreads[0] = CreateThread(NULL, 0, counter, NULL, 0, &idThreads[0]);
	hThreads[1] = CreateThread(NULL, 0, hThreadEventOne, NULL, 0, &idThreads[1]);
	hThreads[2] = CreateThread(NULL, 0, hThreadEventTwo, NULL, 0, &idThreads[2]);

	if (hThreads[0] == NULL) {
		cout << GetLastError() << endl;
		return 0;
	}

	if (hThreads[1] == NULL) {
		cout << GetLastError() << endl;
		return 0;
	}

	if (hThreads[2] == NULL) {
		cout << GetLastError() << endl;
		return 0;
	}

	WaitForMultipleObjects(3, hThreads, TRUE, INFINITE);

	for (int i = 0; i < 3; i++) {
		CloseHandle(hThreads[i]);
	}

	for (int i = 0; i < 2; i++) {
		CloseHandle(hEvents[i]);
	}
	return 0;
}
