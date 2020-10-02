#define startbtn 5
#define smain  2
#define amain  3
#define coin  7
bool xuly = false;
bool tt1=false;
bool tt2 =false;
bool mycoin =false;
bool mycoin1 =false;
bool mystart =false;
bool mystart1 =false;
int point =0;
int counter=0;
int countstop=0;
void setup() {
  Serial.begin(9600);
  pinMode(coin,INPUT_PULLUP);
  pinMode(smain, INPUT);
  pinMode(amain, OUTPUT); 
  pinMode(startbtn, INPUT_PULLUP); 
 digitalWrite(amain, LOW);
   delay(3000);
}
unsigned long timeled=0;
unsigned long timecountticket=0;
void checkcoin()
{
   if(digitalRead(coin)==LOW)
        {
          mycoin=true;  
          delay(100);
        }
         if(digitalRead(coin)==HIGH)
        {
           mycoin1=true;       
        }
        if(mycoin==true&&mycoin1==true)
        {
          mycoin1=false;
          mycoin=false;         
          Serial.println("x");         
         delay(10);        
        }
}
void checkbtn()
{
    if(digitalRead(startbtn)==LOW)
        {
           mystart=true; 
           delay(50);
        }
        if(digitalRead(startbtn)==HIGH)
        {
           mystart1=true;                             
        }
        if(mystart==true&&mystart1==true)
        {
          mystart=false;
          mystart1=false;
         Serial.println("s"); 
          delay(10);
         }
  }
 static boolean  chongnhieu=false;
 bool trangthai=false;
void loop() {     
      checkcoin();
       checkbtn();
  if (Serial.available() > 1) {
      // Serial.readBytes(buff, 5);     
       point = Serial.parseInt();       
       digitalWrite(amain, HIGH); 
       delay(100);
       xuly = true;       
    }       
   while (xuly == true)
      {  
        chongnhieu=true;
            checkcoin();
            checkbtn();
          if (chongnhieu)
          {
            delay(2);
          }
          if (digitalRead(smain)!=trangthai)
          {
            trangthai=!trangthai;
            if(trangthai)
            {
              if(trangthai)
              {
                 counter++;  
                 countstop++;  
                }  
            }  
            chongnhieu=false;
          }
          else
          {
           countstop--; 
           }
         //////////////////////////
        if(counter>=point)
          {
            digitalWrite(amain, LOW);           
            point=0;
            xuly=false;          
            counter=0;
            countstop=0;
            Serial.println("d");     
            break;          
          } 
          if(countstop>100)
          {
            if(counter<2)
            {
               countstop=0; 
                digitalWrite(amain, LOW);           
                point=0;
                xuly=false;               
                counter=0;           
                Serial.println("e");    
                  break; 
               }
          }
        }
            
  delay(20);     
}
