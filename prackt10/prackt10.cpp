#include <iostream>
#include <Windows.h>
#include <random>


#define MAX_LOGS 1000
#define MAX_TEXT 128

using namespace std;


struct LogRecord {
    DWORD threadId;
    int priority;
    DWORD tick;
    char message[MAX_TEXT]; 
};

struct LogBuffer {
    LogRecord records[MAX_LOGS]; 
    LONG index;
};


LogBuffer lb = { {}, 0 };
char messeages[3][MAX_TEXT] = { "Привет", "Пока", "иче))" };
CRITICAL_SECTION cs;


int randomInt(int a, int b)
{
    static mt19937 gen(random_device{}());
    uniform_int_distribution<int> dis(a, b);
    return dis(gen);
}

DWORD WINAPI logger(LPVOID csBool) {


    bool value = *(bool*)csBool;

    while (true) {
        cout << "CS = " << value << " логгер " << GetCurrentThreadId() << " ОТРАБОТАЛ" << endl;

        if (value)EnterCriticalSection(&cs);
        LogRecord lr = { 
            GetCurrentThreadId(), 
            GetThreadPriority(GetCurrentThread()), 
            GetTickCount() 
        };

        strcpy_s(lr.message, messeages[randomInt(0, 2)]);

        if (lb.index >= MAX_LOGS) {
            lb.index = 0;
        }
        lb.records[lb.index] = lr;
        lb.index++;
        if (value) LeaveCriticalSection(&cs);
        Sleep(randomInt(10, 100));
    }
    return 0;
}

DWORD WINAPI listener(LPVOID csBool){
    bool value = *(bool*)csBool;

    while (true) {
        if (value) EnterCriticalSection(&cs);
        cout << "Index: " << lb.index << endl;
        if (lb.index >= 5) {
            for (int i = lb.index - 1; i >= lb.index - 5; i--) {
                cout << "ID: " << lb.records[i].threadId << endl;
                cout <<"Сообщение: " << lb.records[i].message << endl;
                cout<< "Приоритет: " << lb.records[i].priority << endl;
                cout<< "Тик: " << lb.records[i].tick << endl;
            }

        }
        else {
            for (int i = lb.index - 1; i >= 0; i--) {
                cout << "ID: " << lb.records[i].threadId << endl;
                cout << "Сообщение: " << lb.records[i].message << endl;
                cout << "Приоритет: " << lb.records[i].priority << endl;
                cout << "Тик: " << lb.records[i].tick << endl;
            }
        }
        if (value) LeaveCriticalSection(&cs);
        Sleep(100);
        system("cls");
    }
    return 0;
}

int main()
{
    setlocale(0, "rus");

    cout << "Использовать критические секции?" << endl;
    cout << "y/n" << endl;

    char ansChar;

    cin >> ansChar;

    bool ans;

    
    switch (ansChar) {
    case('y'):
        ans = true;
        break;
    case('n'):
        ans = false;
        break;
    default:
        cout << "Unknown command" << endl;
        return 1;
    }


    HANDLE hTreadLoggerT1;
    DWORD threadIdLoggerT1;

    HANDLE hTreadLoggerT2;
    DWORD threadIdLoggerT2;

    HANDLE hTreadLoggerT3;
    DWORD threadIdLoggerT3;


    HANDLE hTreadListener;
    DWORD threadIdListener;

    if (ans) InitializeCriticalSection(&cs);

    if (hTreadLoggerT1 = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)logger, &ans, 0, &threadIdLoggerT1)) {
    }
    else {
        std::cout << "Error" << endl;
        return GetLastError();
    }

    SetThreadPriority(hTreadLoggerT1, THREAD_PRIORITY_HIGHEST);

    if (hTreadLoggerT2 = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)logger, &ans, 0, &threadIdLoggerT2)) {
    }
    else {
        std::cout << "Error" << endl;
        return GetLastError();
    }

    SetThreadPriority(hTreadLoggerT2, THREAD_PRIORITY_NORMAL);

    if (hTreadLoggerT3 = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)logger, &ans, 0, &threadIdLoggerT3)) {
    }
    else {
        std::cout << "Error" << endl;
        return GetLastError();
    }

    SetThreadPriority(hTreadLoggerT3, THREAD_PRIORITY_BELOW_NORMAL);


    if (hTreadListener = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)listener, &ans, 0, &threadIdListener)) {
    }
    else {
        std::cout << "Error" << endl;
        return GetLastError();
    }

    SetThreadPriority(hTreadLoggerT3, THREAD_PRIORITY_LOWEST);


    WaitForSingleObject(hTreadLoggerT1, INFINITE);
    WaitForSingleObject(hTreadLoggerT2, INFINITE);
    WaitForSingleObject(hTreadLoggerT3, INFINITE);

    WaitForSingleObject(hTreadListener, INFINITE);

    CloseHandle(hTreadLoggerT1);
    CloseHandle(hTreadLoggerT2);
    CloseHandle(hTreadLoggerT3);

    CloseHandle(hTreadListener);

    if(ans) DeleteCriticalSection(&cs);

    return 0;
}

