#include <iostream>
#include<Windows.h>
#include <random> 

using namespace std;

struct States{
	bool attack = false; // Состояние атаки
	bool sleep = true; // Состояние ожидания
	bool critAttack = false; // Состояние критической атаки
};
struct Bayum {
	long health         = 9000000000; // здоровье
	int resist          = 44; // сопротивление атакам
	int damage          = 73843; // урон обычной атакой
	int specialDamage   = 150000; // урон спецатакой
	int attackCooldown  = 5; // время между обычными атаками (сек)
	int specialCooldown = 10; // время между спецатаками (сек)
	States state;
};

struct Player {
	long health         = 500000; // здоровье игрока
	int damage          = 12000; // урон обычной атакой
	int specialDamage   = 30000; // урон спецприёмом (опционально)
	int attackCooldown  = 2; // время между обычными атаками (сек)
	int specialCooldown = 5; // время между спецатаками (сек)
	int defense         = 20; // защита игрока (уменьшает урон босса)
	int dodgeChance     = 15; // шанс уклониться от атаки босса (%)
	char name[64]; // имя игрока (для логов)
	int dealtDamage = 0; // Нанесенный урон
};

struct BossThread {
	HANDLE hBossThread; // Дискриптер процесса босса
	DWORD IDBossThread;	// ID потока

	HANDLE hBossAttackThread; // Дискриптер процесса атаки босса
	DWORD IDBossAttackThread;	// ID потока

	HANDLE hBossSpecialAttackThread; // Дискриптер процесса специальной атаки босса
	DWORD IDBossSpecialAttackThread;	// ID потока

};

struct PlayerThread {
	int id;
	Player player; // Игрок
	HANDLE hPlayerThread; // Дискриптер процесса игрока
	DWORD IDPlayerThread; // ID потока

	HANDLE hPlayerAttackThread; // Дискриптер процесса атаки игрока
	DWORD IDPlayerAttackThread; // ID потока

	bool isDead = false;
};

int countPlayers; // Количество игроков
int alivePlayers; // Количество живых игроков


HANDLE event; // Дискриптер события
PlayerThread players[10]; // Массив игроков

BossThread bossThread; // Поток босса
Bayum boss; // Босс
CRITICAL_SECTION CriticalSection; // Критическая секция

HANDLE hStatsThread; // Дискриптер процесса текущей статистики
DWORD IDStatsThread; // ID потока

HANDLE hBossAttackEvent;      // событие обычной атаки
HANDLE hBossSpecialEvent;     // событие критической атаки
HANDLE hPlayerReadyEvent;	  // событие для синхронизации игроков


int randomInt(int a, int b) // Функция рандома
{
	static mt19937 gen(random_device{}());
	uniform_int_distribution<int> dis(a, b);
	return dis(gen);
}

DWORD WINAPI BossAttack(LPVOID) // Функция атаки босса
{
	while (true) {
		WaitForSingleObject(hBossAttackEvent, INFINITE);
		if (boss.state.attack == true) {
			int dodgeChance = randomInt(0, 100);
			EnterCriticalSection(&CriticalSection);
			if (alivePlayers > 0)
			{
				if (dodgeChance <= 15) {
					cout << "Босс промахнулся" << endl;
				}
				else {
					bool isAttack = false;
					while (!isAttack) {
						int rndPlayer = randomInt(1, countPlayers);
						int effectiveDamage = boss.damage * (100 - players[rndPlayer - 1].player.defense) / 100;

						if (!players[rndPlayer - 1].isDead)
						{
							isAttack = true;
							players[rndPlayer - 1].player.health -= effectiveDamage;
							cout << "Босс наносит удар по игроку " << rndPlayer << endl;
						}

					}
				}
			}
			LeaveCriticalSection(&CriticalSection);
			boss.state.attack = false;
			boss.state.critAttack = true;
			
		}
		SetEvent(hBossSpecialEvent);
		Sleep(boss.attackCooldown * 1000);

	}
	return 0;

}

DWORD WINAPI SpecialBossAttack(LPVOID) // Функция специальной атаки босса	
{
	while (true) {	
		WaitForSingleObject(hBossSpecialEvent, INFINITE);

		if (boss.state.critAttack == true) {
			ResetEvent(hPlayerReadyEvent);

			EnterCriticalSection(&CriticalSection);
				if (countPlayers > 1) {
					int effectiveDamage = boss.specialDamage * (1 - 0.05 *(alivePlayers - 1));
					for (int i = 0; i < countPlayers; i++) {
						players[i].player.health -= effectiveDamage;
					}
				}
				else {
					for (int i = 0; i < countPlayers; i++) {
						players[i].player.health -= boss.specialDamage;
					}
				}

				cout << "Босс наносит критический удар" << endl;
				LeaveCriticalSection(&CriticalSection);
				boss.state.critAttack = false;
				boss.state.sleep = true;
				SetEvent(hPlayerReadyEvent);
		}
		SetEvent(hBossAttackEvent);
		Sleep(boss.specialCooldown * 1000);
	}
	return 0;
}

DWORD WINAPI CurrentStats(LPVOID) // Функция статистики
{
	while (true) {
		if (boss.state.sleep == true) {
			EnterCriticalSection(&CriticalSection);
				cout << endl;
				cout << "------------------------------" << endl;
				cout << "Здоровье босса " << boss.health << endl;
				for (int i = 0; i < countPlayers; i++) {
					if (!players[i].isDead) {
						cout << "Здоровье игрока " << i + 1 << " " << players[i].player.health << endl;
					}
					else {
						cout << "Игрок " << i + 1 << " " << "умер" << endl;
					}
				}
				cout << "------------------------------" << endl;
				cout << endl;
				LeaveCriticalSection(&CriticalSection);
				boss.state.sleep = false;
				boss.state.attack = true;
			
		}
	}
	return 0;
}



DWORD WINAPI bossThreadFunc(LPVOID)
{
	bossThread.hBossAttackThread = CreateThread(NULL, 0, BossAttack, NULL, 0, &bossThread.IDBossAttackThread);
	bossThread.hBossSpecialAttackThread = CreateThread(NULL, 0, SpecialBossAttack, NULL, 0, &bossThread.IDBossSpecialAttackThread);


	while (true) {
		
		if (boss.health < 0) {
			CloseHandle(bossThread.hBossAttackThread);
			CloseHandle(bossThread.hBossSpecialAttackThread);
			CloseHandle(bossThread.hBossThread);
		}
	}
	return 0;
}
DWORD WINAPI playersAttack(LPVOID player) {
	int thisPlayer = (int)player;

	while (true) {
		WaitForSingleObject(hPlayerReadyEvent, INFINITE);

		EnterCriticalSection(&CriticalSection);

		if (players[thisPlayer].isDead == true) {
			LeaveCriticalSection(&CriticalSection);
			return 0;
		}

		if (boss.health <= 0) {
			LeaveCriticalSection(&CriticalSection);
			return 0;
		}

			int effectiveDamage = players[thisPlayer].player.damage * (100 - boss.resist) / 100;
			boss.health -= effectiveDamage;
			players[thisPlayer].player.dealtDamage += effectiveDamage;
			cout << "Игрок " << thisPlayer + 1 << " наносит удар по боссу " << endl;

			LeaveCriticalSection(&CriticalSection);
		
		
		Sleep(players[thisPlayer].player.attackCooldown * 1000);
	}
	return 0;
}



DWORD WINAPI playersThreadFunc(LPVOID player)
{
	int thisPlayer = (int)player;
	
	players[thisPlayer].hPlayerAttackThread = CreateThread(NULL, 0, playersAttack, (void*)thisPlayer, 0, &players[thisPlayer].IDPlayerAttackThread);

	while (true) {
		EnterCriticalSection(&CriticalSection);
		if (players[thisPlayer].player.health < 0) {
			cout << "Игрок "<< thisPlayer + 1 << " погиб" << endl;
			players[thisPlayer].isDead = true;
			alivePlayers--;
			LeaveCriticalSection(&CriticalSection);
			return 0;
		}
		LeaveCriticalSection(&CriticalSection);
	}
	return 0;
}

void sortPlayersByDamage(PlayerThread arr[], int n) {
	for (int i = 0; i < n - 1; i++) {
		for (int j = 0; j < n - i - 1; j++) {
			if (arr[j].player.dealtDamage < arr[j + 1].player.dealtDamage) {
				PlayerThread temp = arr[j];
				arr[j] = arr[j + 1];
				arr[j + 1] = temp;
			}
		}
	}
}

int main()
{
	setlocale(0, "rus");
	while (true) {
		cout << "Введите количество игроков(от 1 до 10)" << endl;
		cin >> countPlayers;
		alivePlayers = countPlayers;

		if ((int)countPlayers > 0 && (int)countPlayers <= 10) {
			break;
		}
		else {
			cout << "Вне диапазона" << endl;
		}

	}
	InitializeCriticalSection(&CriticalSection);
	hBossAttackEvent = CreateEvent(NULL, FALSE, TRUE, NULL);   
	hBossSpecialEvent = CreateEvent(NULL, FALSE, FALSE, NULL);
	hPlayerReadyEvent = CreateEvent(NULL, TRUE, TRUE, NULL);

	hStatsThread = CreateThread(NULL, 0, CurrentStats, NULL, 0, &IDStatsThread);
	bossThread.hBossThread = CreateThread(NULL, 0, bossThreadFunc, NULL, 0, &bossThread.IDBossThread);
	for (int i = 0; i < countPlayers; i++) {
		players[i].id = i + 1;
		players[i].hPlayerThread = CreateThread(NULL, 0, playersThreadFunc, (void*)i, 0, &players[i].IDPlayerThread);
	}
	while (true) {
		if (boss.health <= 0 || alivePlayers <= 0) {
			if (boss.health <= 0) cout << "Босс побежден!" << endl;
			else cout << "Все игроки погибли" << endl;

			sortPlayersByDamage(players, countPlayers);

			cout << "Топ 3 игроков по урону:" << endl;
			for (int i = 0; i < countPlayers; i++) {
				cout << i + 1 << ". Игрок " << players[i].id
					<< " нанес " << players[i].player.dealtDamage << " урона" <<endl;
			}

			CloseHandle(hBossAttackEvent);
			CloseHandle(hBossSpecialEvent);
			ExitProcess(0);
		}
	}
	return 0;
}