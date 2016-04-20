

void setup() {
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
}


void loop() {
  int c = Serial.read();
  Serial.print("Just read: ");
  Serial.println(c);
  delay(1000);
}



