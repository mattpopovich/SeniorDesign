// Wire Master Writer
// by Nicholas Zambetti <http://www.zambetti.com>

// Demonstrates use of the Wire library
// Writes data to an I2C/TWI slave device
// Refer to the "Wire Slave Receiver" example for use with this

// Created 29 March 2006

// This example code is in the public domain.

#include <Wire.h>
#include "PinChangeInt.h"
#define IRDY  2
#define I2CA0 4
#define MCLR  7
#define GROUP_CNT      5
#define BYTES_TO_READ  13

byte samples[GROUP_CNT][BYTES_TO_READ];

int interrupted = 0;

void enableISR(){
  digitalWrite(IRDY, HIGH); //use the internal pullup resistor
  // attach a PinChange Interrupt
  PCintPort::attachInterrupt(IRDY, isr, RISING);
}

void disableISR(){
  // detach a PinChange Interrupt
  PCintPort::detachInterrupt(IRDY);
}

void setup(){
  pinMode(IRDY, INPUT);     //set the pin to input
  digitalWrite(IRDY, HIGH); //use the internal pullup resistor
  
  pinMode(I2CA0,OUTPUT);
  pinMode(MCLR,OUTPUT);
  digitalWrite(I2CA0,0);
  digitalWrite(MCLR,0);
  delay(10);
  digitalWrite(MCLR,1);
  
  Serial.begin(9600);// initialize UART
  Serial.println("UART is running!");
  Wire.begin(); // join i2c bus (address optional for master)
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
  
  multiReadArray(0x3d,GROUP_CNT,BYTES_TO_READ,(byte**)samples);
  
  Serial.println("NEW SAMPLE!");
  for(int i=0;i<BYTES_TO_READ;i++){
    Serial.print(samples[0][i]);
    Serial.print('\t');
    Serial.print(samples[1][i]);
    Serial.print('\t');
    Serial.print(samples[2][i]);
    Serial.print('\t');
    Serial.print(samples[3][i]);
    Serial.print('\t');
    Serial.println(samples[4][i]);
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

void multiReadArray(byte startingAddress, int groups, int length, byte** array){
  for(int g=0; g<groups; g++){
    for(byte cnt=0;cnt<length;cnt++){
      array[g][cnt] = cnt;
    }
    
//    int cnt = 0;
//    while(digitalRead(IRDY)==0){};// wait for IQS to be ready
//    
//    Wire.beginTransmission(0x74);   // transmit to device #4
//    Wire.write(startingAddress);    // request device info
//    Wire.endTransmission(false);    // send restart transmitting
//    
//    Wire.requestFrom(0x74,length,true);
//    while (Wire.available()) {      // slave may send less than requested
//      byte c = Wire.read();          // receive a byte as character
//      array[g][cnt] = c;
//      cnt++;
//    }// while
  }
}
