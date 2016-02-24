#include <Wire.h>
#include "PinChangeInt.h"
#define IRDY                2
#define I2CA0               4
#define MCLR                7

// These are defines for doing a multi read of the samples
// GROUP_CNT:
//   number of groups that are enabled for sampleing
// GROUP_SAMPLE_LEN:
//   number of bytes to read starting from address 0x3D (group#)
//   17 = 1 group# + 8 samples + 8 LTA
//   9  = 1 group# + 8 samples
#define GROUP_CNT           5
#define GROUP_SAMPLE_LEN    17

// array to hold sample data
byte samples[GROUP_CNT*GROUP_SAMPLE_LEN];

int interrupted = 0;

void enableISR(){
  digitalWrite(IRDY, HIGH);//use internal pullups
  // attach a PinChange Interrupt
  PCintPort::attachInterrupt(IRDY, isr, RISING);
}

void disableISR(){
  // detach a PinChange Interrupt
  PCintPort::detachInterrupt(IRDY);
}

void setup(){
  pinMode(IRDY, INPUT);     //set the pin to input
  digitalWrite(IRDY, HIGH); //use internal pullups
  
  pinMode(I2CA0,OUTPUT);
  pinMode(MCLR,OUTPUT);
  digitalWrite(I2CA0,0);
  digitalWrite(MCLR,0);
  delay(10);
  digitalWrite(MCLR,1);
  
  Serial.begin(9600);// initialize UART
  Serial.println("UART is running!");
  Wire.begin();
  Serial.println(readRegister(0x00));
  writeRegister(0xd0,0x0f);
//  writeRegister(0xd2,0x05);// chan_active_0 ch0-ch3
//  writeRegister(0xd3,0x01);// chan_active_1 ch4-ch7
//  writeRegister(0xd4,0x00);// chan_active_2 ch8-ch11
//  writeRegister(0xd5,0x00);// chan_active_3 ch12-ch15
//  writeRegister(0xd6,0x04);// chan_active_4 ch16-ch19
  writeRegister(0xc4,0x26);// force PROX mode
}

void loop(){
//  if(interrupted){
//    Serial.println(readRegister(0x00));
//    interrupted = 0;
//  }// if
  
  readSamples(GROUP_CNT,GROUP_SAMPLE_LEN,samples);
  
  Serial.println("NEW SAMPLE!");
  for(int i=0;i<GROUP_SAMPLE_LEN;i++){
    Serial.print(samples[0*GROUP_SAMPLE_LEN+i]);
    Serial.print('\t');
    Serial.print(samples[1*GROUP_SAMPLE_LEN+i]);
    Serial.print('\t');
    Serial.print(samples[2*GROUP_SAMPLE_LEN+i]);
    Serial.print('\t');
    Serial.print(samples[3*GROUP_SAMPLE_LEN+i]);
    Serial.print('\t');
    Serial.println(samples[4*GROUP_SAMPLE_LEN+i]);
  }// for
}

void isr(){ // handle pin change interrupt for D0 to D7 here
  interrupted = 1;
}

void writeRegister(byte registerAddress, byte data){
  while(digitalRead(IRDY)==0){};// wait for IQS to be ready
  
  Wire.beginTransmission(0x74);
  Wire.write(registerAddress);
  Wire.write(data);
  Wire.endTransmission();
  delay(1);
}

byte readRegister(byte registerAddress){
  byte c;
  while(digitalRead(IRDY)==0){};// wait for IQS to be ready
  
  Wire.beginTransmission(0x74);   // transmit to device #4
  Wire.write(registerAddress);    // request device info
  Wire.endTransmission(false);    // send restart transmitting
  
  Wire.requestFrom(0x74,1,true);
  while (Wire.available()) {      // slave may send less than requested
    c = Wire.read();          // receive a byte as character
  }// while
  
  return c;
}

void readArray(byte startingAddress, int length, byte* array){
  int cnt = 0;
    
  while(digitalRead(IRDY)==0){};// wait for IQS to be ready
  
  Wire.beginTransmission(0x74);// send START and CONTROL byte
  Wire.write(startingAddress); // send starting address
  Wire.endTransmission(false); // send RESTART
  
  Wire.requestFrom(0x74,length,true);// request bytes from IQS
  while (Wire.available()) {
    byte c = Wire.read();      // read byte
    array[cnt] = cnt;
    cnt++;
  }// while
  
  for(int i=0;i<length;i++){
    array[i] = i;
  }
}

void readSamples(int groups, int length, byte* array){
  for(int g=0; g<groups; g++)
    readArray(0x3d,length, &array[g*length]);
}
