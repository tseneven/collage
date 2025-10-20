#include <iostream>

struct Handlers {
	void (*handler)();
};

struct NodeStringHandler {
	Handlers handler;
	NodeStringHandler* next;

	NodeStringHandler(Handlers h, NodeStringHandler* n = nullptr)
		: handler(h), next(n) {}
};

class HandlerList {
public:
	NodeStringHandler* head = nullptr;


	void push_back(Handlers handler) {
		NodeStringHandler* newNode = new NodeStringHandler(handler);

		if (head == nullptr) {
			head = newNode;
			return;
		}

		NodeStringHandler* current = head;
		while (current->next != nullptr) {
			current = current->next;
		}

		current->next = newNode;
	}

	void delete_front() {
		head->handler.handler();
		if (head->next == nullptr) {
			head = nullptr;
			return;
		}
		head = head->next;
	}
};


struct EventSystem
{
	HandlerList list = {};
	int count = 0;
};

void registerHandlers(EventSystem &system, void (*handler)()) {
	if (system.count < 5) {

		Handlers handlerWithType = {handler};
		system.list.push_back(handlerWithType);
		system.count++;
	}
	else {
		std::cout << "Ну допустим, закончилась память" << std::endl;
		system.list.delete_front();
		system.count--;
		registerHandlers(system, handler);
	}
}

void handler(const std::string& message) {
	std::cout << message << std::endl;
}

void triggerEvent(EventSystem& system) {
	NodeStringHandler* tmp = system.list.head;

	while (tmp != nullptr) {
		tmp->handler.handler();
		tmp = tmp->next;
	}

	system.count = 0;
	system.list.head = nullptr; 
}


void onUserLogin() {
	handler("вошел в систему");
}


void onUserLogout() {
	handler("вышел из системы");
}

void onError(){
	handler("Произошла ошибка");
}


int main()
{
	EventSystem events = {};
	setlocale(0, "rus");

	while (true) {
		while (true) {
			std::cout << "Добавить событие? Y/N" << std::endl;
			char action;
			std::cin >> action;
			if (action == 'Y') {
				std::cout << "1 - Логин" << std::endl;
				std::cout << "2 - Выход" << std::endl;
				std::cout << "3 - Ошибка" << std::endl;


				int intAction;

				std::cin >> intAction;

				switch (intAction) {
				case 1:

					registerHandlers(events, onUserLogin);

					break;
				case 2:

					registerHandlers(events, onUserLogout);


					break;
				case 3:

					registerHandlers(events, onError);
					
					break;
				default:
					break;

				}

			}
			else {
				break;
			}
		}

		triggerEvent(events);
	}




	return 0;
}
