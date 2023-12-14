void setup() {
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  pinMode(A2, INPUT);
  pinMode(A3, INPUT);

  Serial.begin(9600);
}


void loop() {
  int fabric0 = analogRead(A0);  
  int fabric1 = analogRead(A1);  
  int fabric2 = analogRead(A2);  
  int fabric3 = analogRead(A3);  

  String output = String(fabric0) + " " + String(fabric1) + " " + String(fabric2) + " " + String(fabric3);
  Serial.println(output);
  delay(200);
}
