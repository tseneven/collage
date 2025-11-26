#include <iostream>
using namespace std;

struct Auto {
    char title[255];
    char govNumber[255];
    int date;
    int stm;
};

struct Node {
    Auto data;
    Node* next;
    Node* prev;

    Node(Auto value) : data(value), next(nullptr), prev(nullptr) {}
};

class DoublyLinkedList {
public:
	Node* head = nullptr;
	Node* tail = nullptr;

	void push_back(Auto n) {
		Node* tmp = new Node(n);
		if (head == nullptr) {
			head = new Node(n);
			return;
		}
		if (tail == nullptr) {
			Node* tmp = new Node(n);
			tmp->prev = head;
			head->next = tmp;
			tail = tmp;
			return;
		}
		tail->next = tmp;
		tmp->prev = tail;
		tail = tmp;
	}

	void bubble_sort_date_up() {
		if (head == nullptr || head->next == nullptr)
		{
			std::cout << "Not sort" << std::endl;
			return;
		}

		int count = 0;
		Node* tmpCount = head;
		while (tmpCount != nullptr) {
			count++;
			tmpCount = tmpCount->next;
		}

		for (int i = 0; i < count - 1; i++) {
			Node* tmp = head;
			bool swapped = false;

			while (tmp->next != nullptr) {
				if (tmp->data.date > tmp->next->data.date) {
					Node* first = tmp;
					Node* second = tmp->next;

					first->next = second->next;
					second->prev = first->prev;

					if (second->next != nullptr)
						second->next->prev = first;

					if (first->prev != nullptr)
						first->prev->next = second;

					second->next = first;
					first->prev = second;

					if (head == first)
						head = second;
					if (first->next == nullptr)
						tail = first;

					swapped = true;
				}
				else {
					tmp = tmp->next;
				}
			}

			if (!swapped) break;
		}
	}
	void bubble_sort_date_down() {
		if (head == nullptr || head->next == nullptr)
		{
			std::cout << "Not sort" << std::endl;
			return;
		}

		int count = 0;
		Node* tmpCount = head;
		while (tmpCount != nullptr) {
			count++;
			tmpCount = tmpCount->next;
		}

		for (int i = 0; i < count - 1; i++) {
			Node* tmp = head;
			bool swapped = false;

			while (tmp->next != nullptr) {
				if (tmp->data.date < tmp->next->data.date) {
					Node* first = tmp;
					Node* second = tmp->next;

					first->next = second->next;
					second->prev = first->prev;

					if (second->next != nullptr)
						second->next->prev = first;

					if (first->prev != nullptr)
						first->prev->next = second;

					second->next = first;
					first->prev = second;

					if (head == first)
						head = second;
					if (first->next == nullptr)
						tail = first;

					swapped = true;
				}
				else {
					tmp = tmp->next;
				}
			}

			if (!swapped) break;
		}
	}


	void bubble_sort_stm_up() {
		if (head == nullptr || head->next == nullptr)
		{
			std::cout << "Not sort" << std::endl;
			return;
		}

		int count = 0;
		Node* tmpCount = head;
		while (tmpCount != nullptr) {
			count++;
			tmpCount = tmpCount->next;
		}

		for (int i = 0; i < count - 1; i++) {
			Node* tmp = head;
			bool swapped = false;

			while (tmp->next != nullptr) {
				if (tmp->data.stm > tmp->next->data.stm) {
					Node* first = tmp;
					Node* second = tmp->next;

					first->next = second->next;
					second->prev = first->prev;

					if (second->next != nullptr)
						second->next->prev = first;

					if (first->prev != nullptr)
						first->prev->next = second;

					second->next = first;
					first->prev = second;

					if (head == first)
						head = second;
					if (first->next == nullptr)
						tail = first;

					swapped = true;
				}
				else {
					tmp = tmp->next;
				}
			}

			if (!swapped) break;
		}
	}
	void bubble_sort_stm_down() {
		if (head == nullptr || head->next == nullptr)
		{
			std::cout << "Not sort" << std::endl;
			return;
		}

		int count = 0;
		Node* tmpCount = head;
		while (tmpCount != nullptr) {
			count++;
			tmpCount = tmpCount->next;
		}

		for (int i = 0; i < count - 1; i++) {
			Node* tmp = head;
			bool swapped = false;

			while (tmp->next != nullptr) {
				if (tmp->data.stm < tmp->next->data.stm) {
					Node* first = tmp;
					Node* second = tmp->next;

					first->next = second->next;
					second->prev = first->prev;

					if (second->next != nullptr)
						second->next->prev = first;

					if (first->prev != nullptr)
						first->prev->next = second;

					second->next = first;
					first->prev = second;

					if (head == first)
						head = second;
					if (first->next == nullptr)
						tail = first;

					swapped = true;
				}
				else {
					tmp = tmp->next;
				}
			}

			if (!swapped) break;
		}
	}




	void display_list() {
		Node* tmp = head;

		if (tmp == nullptr) {
			cout << "list empty" << endl;
			return;
		}

		while (tmp->next != nullptr) {
			cout << tmp->data.title << endl;
			cout << tmp->data.govNumber << endl;
			cout << tmp->data.date << endl;
			cout << tmp->data.stm << endl;
			tmp = tmp->next;
		}
		cout << tail->data.title << endl;
		cout << tail->data.govNumber << endl;
		cout << tail->data.date << endl;
		cout << tail->data.stm << endl;
	}
};


int main()
{

	DoublyLinkedList list = {};

	short action = 0;

	while (true) {
		cout << "Enter action" << endl;
		cout << "1 - add element" << endl;
		cout << "2 - display list" << endl;
		cout << "3 - sort list date up" << endl;
		cout << "4 - sort list date down" << endl;
		cout << "5 - sort list stm up" << endl;
		cout << "6 - sort list stm down" << endl;
		cout << "7 - exit" << endl;

		cin >> action;



		switch (action) {
		case 1:
			Auto n;
			cout << "enter marka" << endl;
			cin >> n.title;
			cout << "enter govNumber" << endl;
			cin >> n.govNumber;
			cout << "enter date" << endl;
			cin >> n.date;
			cout << "enter stoimost" << endl;
			cin >> n.stm;
			list.push_back(n);
			break;
		case 2:
			list.display_list();
			break;
		case 3:
			list.bubble_sort_date_up();
			break;
		case 4:
			list.bubble_sort_date_down();
			break;
		case 5:
			list.bubble_sort_stm_up();
			break;
		case 6:
			list.bubble_sort_stm_down();
			break;
		default:
			return 0;
		}

	}

	return 0;
}

