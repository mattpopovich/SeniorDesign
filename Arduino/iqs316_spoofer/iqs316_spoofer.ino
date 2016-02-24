#include <Wire.h>

byte reqAddr = 0x00;
byte data = 0;

void setup(){
  Wire.begin(0x74);             // join i2c bus with address 0x74
  Wire.onReceive(receiveEvent); // register event
  Wire.onRequest(requestEvent); // register event
  Serial.begin(9600);           // start serial for output
}

void loop(){
  Serial.print("DATA = ");
  Serial.println(data,HEX);
  delay(100);
}

// function that executes whenever data is received from master
// this function is registered as an event, see setup()
void receiveEvent(int howMany){
  switch(howMany){
    case 1:
      reqAddr = Wire.read();
      break;
    default:
      while(Wire.available()) // loop through all but the last
        Wire.read();// print the character
      break;
  }// switch
}

// function that executes whenever data is requested from master
// this function is registered as an event, see setup()
void requestEvent(){
  for(int i=0;i<17;i++){
    Wire.write(i);
  }// for
}
