#include <iostream>
#include <Windows.h>
#include <random> 

using namespace std;

#define MAX_CLIENTS 20
#define CLUB_CAPACITY 4

int randomInt(int a, int b) // Функция рандома
{
    static mt19937 gen(random_device{}());
    uniform_int_distribution<int> dis(a, b);
    return dis(gen);
}

struct ClientRecord {
    DWORD threadId; // Идентификатор потока
    DWORD arriveTick; // Время прихода посетителя
    DWORD startTick; // Время начала обслуживания
    DWORD endTick; // Время завершения обслуживания
    BOOL served; // Был ли обслужен
    BOOL timeout; // Ушел ли по таймауту
};

struct ClubState {
    ClientRecord clients[MAX_CLIENTS]; // Информация о посетителях
    LONG currentVisitors; // Текущее число занятых мест
    LONG maxVisitors; // Максимум одновременно занятых мест
    LONG servedCount; // Количество обслуженных посетителей
    LONG timeoutCount; // Количество ушедших по таймауту
    HANDLE hThreadVisitor[MAX_CLIENTS];
    DWORD idThreadVisitor[MAX_CLIENTS];

};

HANDLE hThreadObserver;
DWORD idThreadObserver;

ClubState clubState;

bool closedThread = false;

CRITICAL_SECTION cs;
HANDLE event;

DWORD WINAPI Visitor(LPVOID id) // Функция посетителя
{
    int thisID = (int)id;

    clubState.clients[thisID].arriveTick = GetTickCount64();
    clubState.clients[thisID].served = FALSE;
    clubState.clients[thisID].timeout = FALSE;

    DWORD startWait = GetTickCount64();

    while (true)
    {
        EnterCriticalSection(&cs);

        if (clubState.currentVisitors < CLUB_CAPACITY)
        {
            clubState.currentVisitors++;

            if (clubState.currentVisitors > clubState.maxVisitors)
                clubState.maxVisitors = clubState.currentVisitors;

            clubState.clients[thisID].startTick = GetTickCount64();
            clubState.clients[thisID].served = TRUE;

            LeaveCriticalSection(&cs);

            Sleep(randomInt(2000, 5000));

            EnterCriticalSection(&cs);

            clubState.clients[thisID].endTick = GetTickCount64();
            clubState.currentVisitors--;
            clubState.servedCount++;

            LeaveCriticalSection(&cs);

            SetEvent(event); 
            return 0;
        }

        LeaveCriticalSection(&cs);
        DWORD endWait = GetTickCount64();

        if (endWait - startWait > 3000)
        {
            EnterCriticalSection(&cs);
            clubState.clients[thisID].timeout = TRUE;
            clubState.timeoutCount++;
            LeaveCriticalSection(&cs);

            return 0;
        }
    }
}

DWORD WINAPI Observer(LPVOID) // Функция наблюдателя
{
    while (true) {
        cout << endl;
        cout << "Текущее число занятых мест: " << clubState.currentVisitors << endl;
        cout << "Максимум одновременно занятых мест: " << clubState.maxVisitors << endl;
        cout << "Количество обслуженных посетителей: " << clubState.servedCount << endl;
        cout << "Количество ушедших по таймауту: " << clubState.timeoutCount << endl;
        cout << endl;
        if (closedThread == true) {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
            return 0;
        }
        Sleep(500);
    }
    return 0;
}

int main()
{
    setlocale(0, "rus");

    hThreadObserver = CreateThread(NULL, 0, Observer, NULL, 0, &idThreadObserver);
    SetThreadPriority(hThreadObserver, THREAD_PRIORITY_LOWEST);
    InitializeCriticalSection(&cs);
    event = CreateEvent(NULL, FALSE, TRUE, NULL);

    for (int i = 0; i < 8; i++) {

        clubState.hThreadVisitor[i] = CreateThread(NULL, 0, Visitor, (void*)i, 0, &clubState.idThreadVisitor[i]);
        clubState.clients[i].threadId = GetThreadId(clubState.hThreadVisitor[i]);
        SetThreadPriority(clubState.hThreadVisitor[i], THREAD_PRIORITY_NORMAL);
    }

    for (int i = 8; i < 16; i++) {
        clubState.hThreadVisitor[i] = CreateThread(NULL, 0, Visitor, (void*)i, 0, &clubState.idThreadVisitor[i]);
        clubState.clients[i].threadId = GetThreadId(clubState.hThreadVisitor[i]);
        SetThreadPriority(clubState.hThreadVisitor[i], THREAD_PRIORITY_BELOW_NORMAL);
    }
    for (int i = 16; i < 20; i++) {
        clubState.hThreadVisitor[i] = CreateThread(NULL, 0, Visitor, (void*)i, 0, &clubState.idThreadVisitor[i]);
        clubState.clients[i].threadId = GetThreadId(clubState.hThreadVisitor[i]);
        SetThreadPriority(clubState.hThreadVisitor[i], THREAD_PRIORITY_HIGHEST);
    }

    WaitForMultipleObjects(20, clubState.hThreadVisitor, TRUE, INFINITE);
    for (int i = 0; i < MAX_CLIENTS; i++) {
        CloseHandle(clubState.hThreadVisitor[i]);
    }
    closedThread = true;

    WaitForSingleObject(hThreadObserver, INFINITE);
    CloseHandle(hThreadObserver);

    cout << "Максимум одновременно занятых мест: " << clubState.maxVisitors << endl;
    cout << "Количество обслуженных посетителей: " << clubState.servedCount << endl;
    cout << "Количество ушедших по таймауту: " << clubState.timeoutCount << endl;
    cout << endl;


    for (int i = 0; i < MAX_CLIENTS; i++) {
        if (clubState.clients[i].timeout == true) {
            cout << clubState.clients[i].threadId << " не был обслужен" << endl;
        }
    }
    return 0;
}

