#include <iostream>
#include <Windows.h>
using namespace std;

HANDLE hEvents[2];
bool isWork = true;

int main()
{
	setlocale(0, "rus");
	int action;

	hEvents[0] = OpenEvent(EVENT_ALL_ACCESS, NULL, L"EventOneExamB6");
	hEvents[1] = OpenEvent(EVENT_ALL_ACCESS, NULL, L"EventTwoExamB6");

	if (hEvents[0] == NULL) {
		cout << GetLastError() << endl;
		return 0;
	}

	if (hEvents[1] == NULL) {
		cout << GetLastError() << endl;
		return 0;
	}

	while (isWork) {
		cout << "1 - Вывод счетчика" << endl;
		cout << "2 - Стоп/Старт счетчика" << endl;
		cout << "3 - Закрыть программу" << endl;

		cin >> action;

		switch (action) {
		case 1:
		{
			SetEvent(hEvents[1]);
			break;
		}
		case 2:
		{
			SetEvent(hEvents[0]);
			break;
		}
		case 3: {
			for (int i = 0; i < 2; i++)
			{
				CloseHandle(hEvents[i]);
			}
			isWork = false;
			return 0;
		}
		}
	}
	return 0;

}
