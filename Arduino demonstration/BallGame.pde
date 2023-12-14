import processing.serial.*;

Serial arduinoPort;

String serialData=" ";
float fabric1;
float fabric2;
float fabric3;
float fabric4;

void setup() {
  size(1920, 1080);
  arduinoPort = new Serial(this, "/dev/cu.usbmodem11301", 115200);
}

void draw() {
  if (arduinoPort.available() > 0) {
    serialData = arduinoPort.readStringUntil('\n');
    ArrayList<Float> sensorValue = new ArrayList<Float>();
    String[] vallist = serialData.trim().split(" ");
    if (vallist != null) {
      for (String s : vallist) {
        float f = Float.parseFloat(s);
        sensorValue.add(f);
      }
    }
    println(sensorValue);
    fabric1 = sensorValue.get(0);
    fabric2 = sensorValue.get(1);
    fabric3 = sensorValue.get(2);
    fabric4 = sensorValue.get(3);
    
    background(255);
    
    // We need to simply determine the range of resistance changes in the fabric sensor as the controller progresses through the three stages. 
    // The parameters in the conditional statement are determined by the range of resistance change.
    // Phase 1
    if(fabric1>=650 && fabric1<700){
      float per1 = (700-fabric1)/50;
      
      rectMode(CORNER);
      noStroke();
      fill(247, 178, 103);
      rect(361, 391, 250*per1, 98 );
    }
    
    // Phase 2    
    if(fabric1<650 && fabric2>=500 && fabric2<550){
      float per2 = (550-fabric2)/50;
      
      rectMode(CORNER);
      noStroke();
      fill(247, 157, 101);
      rect(610, 391, 250*per2, 98 );
      
      rectMode(CORNER);
      noStroke();
      fill(247, 178, 103);
      rect(361, 391, 250, 98 );
    }
    
    // Phase 3
    if(fabric1<650 && fabric2<500 && fabric3>=720&&fabric3<770){
      float per3 = (770-fabric3)/50;
      
      rectMode(CORNER);
      noStroke();
      fill(244, 132, 95);
      rect(860, 391, 250*per3, 98 );
      
      rectMode(CORNER);
      noStroke();
      fill(247, 157, 101);
      rect(610, 391, 250, 98 );
      
      rectMode(CORNER);
      noStroke();
      fill(247, 178, 103);
      rect(361, 391, 250, 98 );
    }
    
    // Phase 4
    if(fabric1<650 && fabric2<500 && fabric3<720 && fabric4>=580&&fabric4<630){
      float per4 = (630-fabric4)/50;
      
      rectMode(CORNER);
      noStroke();
      fill(242, 112, 89);
      rect(1110, 391, 250*per4, 98 );
      
      rectMode(CORNER);
      noStroke();
      fill(244, 132, 95);
      rect(860, 391, 250, 98 );
      
      rectMode(CORNER);
      noStroke();
      fill(247, 157, 101);
      rect(610, 391, 250, 98 );
      
      rectMode(CORNER);
      noStroke();
      fill(247, 178, 103);
      rect(361, 391, 250, 98 );
    }
    
    rectMode(CORNER);
    noFill();  
    strokeWeight(4);
    stroke(100, 100, 100);
    rect(360, 390, 1000, 100, 8);
    
  }
}
