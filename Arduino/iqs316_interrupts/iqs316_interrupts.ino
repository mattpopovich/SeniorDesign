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
#define GROUP_CNT           4
#define GROUP_SAMPLE_LEN    17
#define SAMPLE_DATA_LEN     8   // sample data occupies 8 bytes
#define LTA_DATA_LEN        8   // LTA data occupies 8 bytes
//#define GUI_PRINT            // toggle GUI and DEBUG printing

byte samples[GROUP_CNT*GROUP_SAMPLE_LEN];// array for sample data
byte arg[4];// for holding menu arguments

void enableISR(){
  // attach a PinChange Interrupt
  PCintPort::attachInterrupt(IRDY, isr, HIGH);
}

void disableISR(){
  // detach a PinChange Interrupt
  PCintPort::detachInterrupt(IRDY);
}

void setup(){
  pinMode(IRDY, INPUT);     //set the pin to input
  digitalWrite(IRDY, HIGH); //use internal pullups

  pinMode(6, OUTPUT);
  pinMode(I2CA0,OUTPUT);
  pinMode(MCLR,OUTPUT);
  digitalWrite(I2CA0,0);
  digitalWrite(MCLR,1);
  resetIQS();
  
  Serial.begin(9600);// initialize UART
  Serial.setTimeout(20);// set timeout to 20 milli seconds
  Serial.println(F("UART is running!"));
  Serial.print(F("Waiting for IQS... "));
  Wire.begin();
  while(readRegister(0x00)!=27)
    delay(10);
  Serial.println(F("OK"));
  
  writeRegister(0xd0,0x0f);
//  writeRegister(0xd2,0x05);// chan_active_0 ch0-ch3
//  writeRegister(0xd3,0x01);// chan_active_1 ch4-ch7
//  writeRegister(0xd4,0x00);// chan_active_2 ch8-ch11
//  writeRegister(0xd5,0x00);// chan_active_3 ch12-ch15
//  writeRegister(0xd6,0x04);// chan_active_4 ch16-ch19
  writeRegister(0xc4,0x26);// force PROX mode

//  enableISR();
}

void loop(){
  readSamples(GROUP_CNT,GROUP_SAMPLE_LEN,samples);
}

void serialEvent(){
//  disableISR();
  /* ENTER CRITICAL SECTION */
  #ifndef GUI_PRINT
  Serial.println("");
  #endif
  switch(Serial.read()){
    case 's':
      sendSamples(Serial.read()-48);
      break;
    case 'l':
      sendLTA(Serial.read()-48);
      break;
    case 'w':
      arg[0] = atoi();// get address byte
      arg[1] = atoi();// get data byte
//      Serial.print("Writing 0x");
//      Serial.print(arg[1],HEX);
//      Serial.print(" to address 0x");
//      Serial.println(arg[0],HEX);
      writeRegister(arg[0],arg[1]);
      break;
    case 'r':
      arg[0] = atoi();// get address byte
      #ifdef GUI_PRINT
      Serial.write(readRegister(arg[0]));
      #else
      Serial.println(readRegister(arg[0]),HEX);
      #endif
      break;
    case 'R':
      resetIQS();
      break;
    case '?':
      Serial.println(F("s#:    get group #'s sample data"));
      Serial.println(F("l#:    get group #'s LTA data"));
      Serial.println(F("w##$$: write 0x$$ to address 0x##"));
      Serial.println(F("r##:   read address 0x##"));
      Serial.println(F("R:     reset IQS"));
      Serial.println(F("?:     help"));
      break;
    default:
      break;
  }// switch
  /* EXIT CRITICAL SECTION */
//  enableISR();
}

// Reads a byte from UART, and interprets as a HEX digit
byte atoi(){
  byte c[2];
  Serial.readBytes(c,2);
  
  c[0] = hexCharToInt(c[0])<<4;
  c[0] |= hexCharToInt(c[1]);

  return c[0];
}

byte hexCharToInt(char c){
  if(c>=48&&c<=57)
    c -= 48;
  else if(c>=65&&c<=70)
    c -= 55;
  else if(c>=97&&c<=102)
    c -= 87;
  else
    c = 0xff;

  return c;
}

void resetIQS(){
  digitalWrite(MCLR,0);
  delay(10);
  digitalWrite(MCLR,1);
}

void sendSamples(byte group){
  byte* ptr = getGroupPtr(group);

  if(ptr){
    // send group #
    #ifdef GUI_PRINT
    Serial.write(ptr[0]);// for GUI tool
    #else
    Serial.println(ptr[0]);// for pretty debugging
    #endif
    ptr += 1;// move to beginning of LTA data
    for(byte i=0;i<SAMPLE_DATA_LEN;i++){
      #ifdef GUI_PRINT
      Serial.write(ptr[i]);// for GUI tool
      #else
      Serial.println(ptr[i]);// for pretty debugging
      #endif
    }// for
  } else {
    #ifdef GUI_PRINT
    Serial.write(0xff);// -1 for group to tell GUI error occured
    #else
    Serial.println(F("SAMPLE DOESN'T EXIST!"));
    #endif
  }// if
}

void sendLTA(byte group){
  byte* ptr = getGroupPtr(group);

  if(ptr){
    // send group #
    #ifdef GUI_PRINT
    Serial.write(ptr[0]);// for GUI tool
    #else
    Serial.println(ptr[0]);// for pretty debugging
    #endif
    ptr += 1+SAMPLE_DATA_LEN;// move to beginning of LTA data
    for(byte i=0;i<LTA_DATA_LEN;i++){
      #ifdef GUI_PRINT
      Serial.write(ptr[i]);// for GUI tool
      #else
      Serial.println(ptr[i]);// for pretty debugging
      #endif
    }// for
  } else {
    #ifdef GUI_PRINT
    Serial.write(0xff);// -1 for group to tell GUI error occured
    #else
    Serial.println(F("SAMPLE DOESN'T EXIST!"));
    #endif
  }// if
}

// Searches the 'samples' array for 'group' data
//  returns pointer to that group's data set (group#,samples,LTA)
//  if group exists in the array
//  returns a null pointer if the data doesn't exist
byte* getGroupPtr(byte group){
  byte* result = NULL;

  // search sample array for the beginning of group data
  for(byte i=0;i<GROUP_CNT;i++){
    if(samples[i*GROUP_SAMPLE_LEN]==group){
      result = &samples[i*GROUP_SAMPLE_LEN];
    }// if
  }// for

  return result;
}

void isr(){ // handle pin change interrupt for D0 to D7 here
  disableISR();
  Serial.print('i');
  readSamples(GROUP_CNT,GROUP_SAMPLE_LEN,samples);
  enableISR();
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

  digitalWrite(6,HIGH);
  while(digitalRead(IRDY)==0){};// wait for IQS to be ready
  digitalWrite(6,LOW);
  
  Wire.beginTransmission(0x74);// send START and CONTROL byte
  Wire.write(startingAddress); // send starting address
  Wire.endTransmission(false); // send RESTART
  
  Wire.requestFrom(0x74,length,true);// request bytes from IQS
  while (Wire.available()) {
    byte c = Wire.read();      // read byte
    array[cnt] = c;
    cnt++;
  }// while
}

void readSamples(int groups, int length, byte* array){
  for(int g=0; g<groups; g++)
    readArray(0x3d,length, &array[g*length]);
}