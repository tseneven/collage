#include <iostream>
#include <windows.h>
#include <conio.h>


struct SnakePart
{
    int x;
    int y;
};

struct AppleCoord {
    int x;
    int y;
};

struct Node
{
    SnakePart data;
    Node* prev;

    Node(SnakePart value) :data(value), prev(nullptr){}
};

class Snake {
public:
    Node* tail = nullptr;
    bool xUpAxis = false;
    bool yUpAxis = true;
    bool xDownAxis = false;
    bool yDownAxis = false;
    bool appleEated = true;
    AppleCoord apple = {};

public:
    void move() {
        int prevX = tail->data.x;
        int prevY = tail->data.y;

        if (yUpAxis)tail->data.y--;
        if (yDownAxis)tail->data.y++;
        if (xUpAxis)tail->data.x++;
        if (xDownAxis)tail->data.x--;

        Node* tmp = tail->prev;
        while (tmp != nullptr) {
            int curX = tmp->data.x;
            int curY = tmp->data.y;
            tmp->data.x = prevX;
            tmp->data.y = prevY;
            prevX = curX;
            prevY = curY;
            tmp = tmp->prev;
        }

        if (tail->data.x == apple.y && tail->data.y == apple.x) {
            addPart();
            appleEated = true;
        }
    }

    bool display(int field[15][15]) {

        // Очистка поля
        for (int i = 0; i < 15; i++) {
            for (int j = 0; j < 15; j++) {
                field[i][j] = 0;             
            }

        }

        field[apple.x][apple.y] = 2;



        Node* tmp = tail;
        bool gameOver = false;
        bool notFirstItteration = false;

        // Перебор змейки
        while (tmp != nullptr) {
            if (tmp->data.x >= 0 && tmp->data.x < 15 && tmp->data.y >= 0 && tmp->data.y < 15) {
                field[tmp->data.y][tmp->data.x] = 1;
                if (tail->data.x >= 0 && tail->data.x < 15 && tail->data.y >= 0 && tail->data.y < 15) {
                    gameOver = true;
                }
            }

            if (notFirstItteration && tmp != nullptr && appleEated != true) {
                if (tail->data.x == tmp->data.x && tail->data.y == tmp->data.y) {
                    gameOver = false;
                }
            }

            tmp = tmp->prev;
            notFirstItteration = true;
        }

        if (appleEated == true) {
            
            apple = generateApple();

            field[apple.x][apple.y] = 2;
            appleEated = false;
        }

        // Выводим поле
        for (int i = 0; i < 15; i++) {
            for (int j = 0; j < 15; j++) {  
                std::cout << field[i][j];
            }
            std::cout << std::endl;
        }


        if (!gameOver) {
            std::cout << "Lose" << std::endl;
            return false;
        }

        return true;
    }

    void addPart() {
        if (!tail) return;

        Node* tmp = tail;
        while (tmp->prev != nullptr) {
            tmp = tmp->prev;
        }

        SnakePart newPart = tmp->data;
        tmp->prev = new Node(newPart);
    }

    AppleCoord generateApple() {
        srand(time(NULL));

        int a = 0; // От а
        int b = 14; // До b

        int x = rand() % (b - a + 1) + a;
        int y = rand() % (b - a + 1) + a;

        //std::cout << x << " " << y << std::endl;

        return {x,y};
    }
};





int main()
{
    int field[15][15] = {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    };

    Snake snake = {};
    SnakePart part = {7, 7};

    snake.tail = new Node(part);


    while(true) {
        snake.move();
        bool action = snake.display(field);
        Sleep(500);


        if (action == false) {
            return 0;
        }

        for (int i = 0; i < 5; i++)
        {
            if (_kbhit()) {
                char a = _getch();

                switch (a) {
                case 'w':
                {
                    if (snake.yUpAxis == true || snake.yDownAxis == true) {
                        break;
                    }
                    snake.xUpAxis = false;
                    snake.xDownAxis = false;
                    snake.yUpAxis = true;
                    snake.yDownAxis = false;
                    i = 5;
                    break;
                }
                case 's':
                {
                    if (snake.yUpAxis == true || snake.yDownAxis == true) {
                        break;
                    }
                    snake.xUpAxis = false;
                    snake.xDownAxis = false;
                    snake.yUpAxis = false;
                    snake.yDownAxis = true;
                    i = 5;
                    break;
                }
                case 'a':
                {
                    if (snake.xUpAxis == true || snake.xDownAxis == true) {
                        break;
                    }
                    snake.xUpAxis = false;
                    snake.xDownAxis = true;
                    snake.yUpAxis = false;
                    snake.yDownAxis = false;
                    i = 5;
                    break;
                }
                case 'd':
                {
                    if (snake.xUpAxis == true || snake.xDownAxis == true) {
                        break;
                    }
                    snake.xUpAxis = true;
                    snake.xDownAxis = false;
                    snake.yUpAxis = false;
                    snake.yDownAxis = false;
                    i = 5;
                    break;
                }
                case 'g':
                    snake.addPart();
                    break;
                default:
                {
                    i = 5;
                    break;
                }
                }
            }
        }
            


        
        system("cls");
    }


    return 0;
}

