void setup() {
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  pinMode(A2, INPUT);
  pinMode(A3, INPUT);

  Serial.begin(115200);
}


void loop() {
  int fabric0 = analogRead(A0);  
  int fabric1 = analogRead(A1);  
  int fabric2 = analogRead(A2);  
  int fabric3 = analogRead(A3);  

  Serial.print(fabric0);
  Serial.print(" ");
  Serial.print(fabric1);
  Serial.print(" ");
  Serial.print(fabric2);
  Serial.print(" ");
  Serial.print(fabric3);
  Serial.println();

  delay(200);
}
