#include <iostream>
#include <Windows.h>
#include <random>
using namespace std;

struct Turtule {
    int x;
    int number;
};

volatile Turtule indexs[10] = { {1, 1}, {1, 2}, {1, 3}, {1, 4}, {1, 5}, {1, 6}, {1, 7}, {1, 8}, {1, 9}, {1, 10}, };
volatile int winner = 0;
volatile bool winners;
volatile int finished = 0;
volatile short field[50] = {};


void showField(short field[], int zd) {


    for (int i = 0; i < 50; i++) {
        field[i] = 0;

        for (int d = 0; d < 10; d++) {
            if (i == indexs[d].x) {
                field[i] += 1;
            }   
        }
    }

    for (int j = 0; j < 50; j++) {
        cout << field[j];
    }
    cout << endl;

    cout << finished << " " << winners << " " << winner << endl;


    if (finished == 10) {
        cout << "Победитель номер " << winner  << endl;
    }

    Sleep(zd);

}
float randomFloat(float a, float b)
{
    static mt19937 gen(random_device{}());
    uniform_real_distribution<float> dis(a, b);
    return dis(gen);
}

int randomInt(int a, int b)
{
    static mt19937 gen(random_device{}());
    uniform_int_distribution<int> dis(a, b);
    return dis(gen);
}



DWORD WINAPI Turtle(LPVOID number) {
    
    while (true) {

        int id = (int)number;

        if (indexs[id].x >= 50) {
            if (!winners) {
                winner = id;
                winners = true;              
            }
            finished++;
            indexs[id].x = 49;
            return 0;
        }
        indexs[(int)number].x += randomInt(0, 2);
    
        int zd = (int)(randomFloat(1, 3) * 1000);
        
        cout << zd << "<- Задержка " << indexs[(int)number].x <<"<- значение X" << endl;
        Sleep(zd);


    }
    return 0;

}

DWORD WINAPI showFieldThread() {
    while (true) {
        showField((short*)field, 3000);

        system("cls");

    }

    return 0;
}




int main()
{
    setlocale(0, "rus");
    HANDLE hThread;
    DWORD IDThread;

    for (int i = 0; i < 10; i++) {
        hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)Turtle, (void*)i, 0, &IDThread);
    }
    hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)showFieldThread, NULL, 0, &IDThread);


    WaitForSingleObject(hThread, INFINITE);


    return 0;
}
