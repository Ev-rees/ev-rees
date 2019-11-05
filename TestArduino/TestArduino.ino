#include <SoftwareSerial.h>
#include <SerialCommand.h>
SerialCommand sCmd;

int analogPin = A1;
int analogPin2 = A0;
int val = 0;
int val2 = 0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  while (!Serial);
}

void loop() {
  val = analogRead(analogPin);
  val2 = analogRead(analogPin2);

  //Serial.println(val);
  Serial.println((String)val + "," + val2);
  delay(40);
}
