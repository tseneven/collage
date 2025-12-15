#include <iostream>
#include <Windows.h>
#include <psapi.h>
#include <conio.h>

using namespace std;

volatile int countC = 0;


struct Process {
	STARTUPINFO si;
	PROCESS_INFORMATION piApp;
};

struct Node {
	Process process;
	Node* prev;

	Node(Process value) : process(value), prev(nullptr){}
};

class Processes {

public:
	Node* tail = nullptr;

    void addPart(const wchar_t* appName)
    {
        Process proc;
        ZeroMemory(&proc.si, sizeof(proc.si));
        proc.si.cb = sizeof(proc.si);
        ZeroMemory(&proc.piApp, sizeof(proc.piApp));

        if (!CreateProcess(appName, NULL, NULL, NULL, FALSE,
            CREATE_NEW_CONSOLE, NULL, NULL, &proc.si, &proc.piApp))
        {
            return;
        }

        Node* node = new Node(proc);

        node->prev = tail;
        tail = node;
    }

    void closeProcess(const wstring& nameProcess)
    {
        Node* current = tail;
        Node* prev = nullptr;

        while (current)
        {
            wchar_t path[MAX_PATH];
            GetModuleFileNameEx(current->process.piApp.hProcess, 0, path, MAX_PATH);

            if (wstring(path) == nameProcess)
            {
                TerminateProcess(current->process.piApp.hProcess, 0);
                CloseHandle(current->process.piApp.hProcess);
                CloseHandle(current->process.piApp.hThread);

                if (prev == nullptr)
                    tail = current->prev;
                else
                    prev->prev = current->prev;

                delete current;
                return;
            }

            prev = current;
            current = current->prev;
        }
    }
     

};
void counter() {
    while (true) {
        countC++;
        Sleep(500);
        cout << countC << endl;
    }
}
STARTUPINFO si;
    PROCESS_INFORMATION piApp;
    HANDLE hThread, hCopyThread;    
    DWORD IDThread;

    // Путь до нового проекта

    wchar_t path[500] = L"C:\\Users\\Neven\\Desktop\\Ucheba\\SP\\prakt9\\x64\\Debug\\console.exe ";
    wchar_t pathM[250] = L"";

void openCounter() {


    hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)counter, NULL, 0, &IDThread);


    if (!SetHandleInformation(hThread, HANDLE_FLAG_INHERIT, HANDLE_FLAG_INHERIT))
    {
        _getch();
    }

    ZeroMemory(&si, sizeof(si));
    si.cb = sizeof(si);

    _itow_s((int)hThread, pathM, 10);
    wcscat_s(path, pathM);



    if (!CreateProcess(NULL, path, NULL, NULL, TRUE, CREATE_NEW_CONSOLE, NULL, NULL, &si, &piApp)) {
        
        cout << "ERROR: " << GetLastError() << endl;
    }

    // Создать новый поток, который будет считать

    //!setHandleInformation c потоком

    //_itow_s(); // (int)поток, назавание процесса, 10

    //wcscat_s(); // названме процесса, поток
    //Создать процесс с новой консолью


    // В новом проекте в мейне char *args[]
    // HANDLE = (HANDLE)atoi(args[1])
    // и им можно управлять
}




int main()
{
    setlocale(0, "RUS");
    Processes processes;
    openCounter();

    cout << "1 - открыть ворд \n2 - закрыть ворд \n3 - открыть powerpoint \n4 - закрыть powerpoint \n5 - открыть обсидиан \n6 - закрыть обсидиан \n7 - открыть эксель \n8 - закрыть эксель \n" << endl;


    while (true) {
        Sleep(500);


        for (int i = 0; i < 5; i++)
        {
            if (_kbhit()) {
                char a = _getch();

                switch (a) {
                case '1':
                {
                    processes.addPart(L"C:\\Program Files\\Microsoft Office\\root\\Office16\\WINWORD.EXE");
                    break;
                }
                case '2':
                {
                    processes.closeProcess(L"C:\\Program Files\\Microsoft Office\\root\\Office16\\WINWORD.EXE");
                    break;
                }
                case '3':
                {
                    processes.addPart(L"C:\\Program Files\\Microsoft Office\\root\\Office16\\POWERPNT.EXE");
                    break;
                }
                case '4':
                {
                    processes.closeProcess(L"C:\\Program Files\\Microsoft Office\\root\\Office16\\POWERPNT.EXE");
                    break;
                }
                case '5':
                {
                    processes.addPart(L"C:\\Program Files\\Obsidian\\Obsidian.exe");
                    break;
                }
                case '6':
                {
                    processes.closeProcess(L"C:\\Program Files\\Obsidian\\Obsidian.exe");
                    break;
                }
                case '7':
                {
                    processes.addPart(L"C:\\Program Files\\Microsoft Office\\root\\Office16\\EXCEL.EXE");
                    break;
                }
                case '8':
                {
                    processes.closeProcess(L"C:\\Program Files\\Microsoft Office\\root\\Office16\\EXCEL.EXE");
                    break;
                }
                default:
                {
                    i = 5;
                    break;
                }
                }
            }
        }
    }
}
