#define RED_LED_PIN 7
#define YELLOW_LED_PIN 6
#define GREEN_LED_PIN 2

int delayTimeGreen = 0;
int delayTimeRed = 0;

int state = 0;

bool isSwitchGreen = true;
bool isUp = true;
bool isConfigured = false;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);

  pinMode(RED_LED_PIN, OUTPUT);
  pinMode(YELLOW_LED_PIN, OUTPUT);
  pinMode(GREEN_LED_PIN, OUTPUT);

  // короче пусть будет и изначальная задача времени
  Serial.println("\nНастройка времени горения зеленого: ");

  while (Serial.available() == 0) {
    // ожидание ввода времени
  }

  delayTimeGreen = Serial.parseInt();
  Serial.read();
  Serial.println("\nНастройка времени горения красного: ");

  while (Serial.available() == 0) {
    // ожидание ввода времени
  }
  delayTimeRed = Serial.parseInt();
  Serial.read();


  isConfigured = true;
}


void loop() {
  // put your main code here, to run repeatedly:
  if (isConfigured) {
    if (isUp == true) {
      if (state == 0) {  // Зеленый с движением вверх
        digitalWrite(RED_LED_PIN, LOW);
        digitalWrite(GREEN_LED_PIN, HIGH);
        delay(delayTimeGreen - 3000);
        for (int i = 0; i < 3; i++) {
          delay(500);
          digitalWrite(GREEN_LED_PIN, HIGH);
          delay(500);
          digitalWrite(GREEN_LED_PIN, LOW);
        }
        state++;
      } else if (state == 1) {  // Желтый с движением вверх
        digitalWrite(YELLOW_LED_PIN, HIGH);
        digitalWrite(GREEN_LED_PIN, LOW);
        delay(3000);
        state++;
      } else if (state == 2) {  // Красный с движением вверх
        digitalWrite(RED_LED_PIN, HIGH);
        digitalWrite(YELLOW_LED_PIN, LOW);
        delay(delayTimeRed);
        state = 0;
        isUp = false;
      }
    } else {
      if (state == 0) {  // Красный + желтый с движением вниз
        digitalWrite(YELLOW_LED_PIN, HIGH);
        delay(3000);
        state++;
      } else if (state == 1) {  // Зеленый с движением вниз
        digitalWrite(RED_LED_PIN, LOW);
        digitalWrite(YELLOW_LED_PIN, LOW);
        digitalWrite(GREEN_LED_PIN, HIGH);
        state = 0;
        isUp = true;
      }
    }
    if (Serial.available() > 0) {
      if (isSwitchGreen) {
        Serial.println("Задана новая скорость зеленого");
        delayTimeGreen = Serial.parseInt();
        Serial.read();
        isSwitchGreen = false;
      } else {
        Serial.println("Задана новая скорость красного");
        delayTimeRed = Serial.parseInt();
        Serial.read();
        isSwitchGreen = true;
      }
    }
  }
}
