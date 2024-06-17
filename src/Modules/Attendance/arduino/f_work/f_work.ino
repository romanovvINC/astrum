#include <ESP8266HTTPClient.h>
#include <Adafruit_Fingerprint.h>
#include <SoftwareSerial.h>
#include <ESP8266WiFi.h>
#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

#define SCREEN_WIDTH 128 // OLED display width, in pixels
#define SCREEN_HEIGHT 32 // OLED display height, in pixels
#define OLED_RESET     D4 // Reset pin # (or -1 if sharing Arduino reset pin)
#define SCREEN_ADDRESS 0x3C ///< See datasheet for Address; 0x3D for 128x64, 0x3C for 128x32
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);

// const char* ssid = "66 Bit Private";
// const char* password = "66@networking";
const char* ssid = "Huhawei_Wi-Fi5";
const char* password = "77669000724";
const String serverAddress = "https://fingerdev.dock8.66bit.ru/fcheck"; 
const String apiToken = "4NzfPDUgBnJ5DKn";
bool wf = false;


SoftwareSerial mySerial(13, 15);

Adafruit_Fingerprint finger = Adafruit_Fingerprint(&mySerial);
WiFiClient client;
uint8_t id;

void setup() {
  Serial.begin(9600);
  if(!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
    Serial.println(F("SSD1306 allocation failed"));
    for(;;); // Don't proceed, loop forever
  }
  while (!Serial)
    ;  // For Yun/Leo/Micro/Zero/...

  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    printText("Connecting to WiFi...", 1);
    Serial.println("Connecting to WiFi...");
  }
  //printText("Connected to WiFi");
  printText("Connecting to WiFi...", 1);
  Serial.println("Connected to WiFi");

  delay(100);
  Serial.println("\n\n66bit finger detect test");

  // set the data rate for the sensor serial port
  finger.begin(57600);

  if (finger.verifyPassword()) {
    Serial.println("Found fingerprint sensor!");
  } else {
    Serial.println("Did not find fingerprint sensor :(");
    while (1) { delay(1); }
  }

  finger.getTemplateCount();
  Serial.print("Sensor contains ");
  Serial.print(finger.templateCount);
  Serial.println(" templates");
  Serial.println("Waiting for valid finger...");
}

void loop()  // run over and over again
{
  waitForFinger();
  getFingerprintID();
  delay(50);  //don't ned to run this at full speed.
}

uint8_t getFingerprintID() {
  uint8_t p = finger.getImage();
  switch (p) {
    case FINGERPRINT_OK:
      printText("Wait...",2);
      delay(500);
      Serial.println("Image taken");
      break;
    case FINGERPRINT_NOFINGER:
      Serial.println("No finger detected");
      return p;
    case FINGERPRINT_PACKETRECIEVEERR:
      Serial.println("Communication error");
      return p;
    case FINGERPRINT_IMAGEFAIL:
      Serial.println("Imaging error");
      return p;
    default:
      Serial.println("Unknown error");
      return p;
  }

  // OK success!

  p = finger.image2Tz();
  switch (p) {
    case FINGERPRINT_OK:
      Serial.println("Image converted");
      break;
    case FINGERPRINT_IMAGEMESS:
      Serial.println("Image too messy");
      return p;
    case FINGERPRINT_PACKETRECIEVEERR:
      Serial.println("Communication error");
      return p;
    case FINGERPRINT_FEATUREFAIL:
      Serial.println("Could not find fingerprint features");
      return p;
    case FINGERPRINT_INVALIDIMAGE:
      Serial.println("Could not find fingerprint features");
      return p;
    default:
      Serial.println("Unknown error");
      return p;
  }

  // OK converted!
  p = finger.fingerFastSearch();
  if (p == FINGERPRINT_OK) {
    Serial.println("Found a print match!");
  } else if (p == FINGERPRINT_PACKETRECIEVEERR) {
    Serial.println("Communication error");
    return p;
  } else if (p == FINGERPRINT_NOTFOUND) {
    accessDeniedText();
    Serial.println("Did not find a match");
    return p;
  } else {
    Serial.println("Unknown error");
    return p;
  }

  // found a match!
  Serial.print("Found ID #");
  Serial.print(finger.fingerID);
  Serial.print(" with confidence of ");
  Serial.println(finger.confidence);
  sendFingerprintID(finger.fingerID);
  return finger.fingerID;
}

// returns -1 if failed, otherwise returns ID #
int getFingerprintIDez() {
  uint8_t p = finger.getImage();

  if (p != FINGERPRINT_OK) return -1;

  p = finger.image2Tz();
  if (p != FINGERPRINT_OK) return -1;

  p = finger.fingerFastSearch();
  if (p != FINGERPRINT_OK) return -1;

  // found a match!
  Serial.print("Found ID #");
  Serial.print(finger.fingerID);
  Serial.print(" with confidence of ");
  Serial.println(finger.confidence);
  sendFingerprintID(finger.fingerID);

  return finger.fingerID;
}


void sendFingerprintID(int fingerprintID) {
  if (WiFi.status() == WL_CONNECTED) {
    WiFiClient client;
    HTTPClient http;

    String serverPath = serverAddress + "?fingerId=" + String(fingerprintID) + "&token=" + apiToken;

    http.begin(client, serverPath.c_str());

    int httpResponseCode = http.GET();

    if (httpResponseCode > 0) {
      Serial.print("HTTP Response code: ");
      Serial.println(httpResponseCode);
      String payload = http.getString();
      Serial.println(payload);
      printText(payload,1);
      delay(5000);
    } else {
      Serial.print("Error code: ");
      Serial.println(httpResponseCode);
    }
    // Free resources
    http.end();
  } else {
    Serial.println("WiFi Disconnected");
  }
}

void printText(String text, int size) {
  const char* textLiteral = text.c_str();
  display.clearDisplay();
  display.setTextWrap(true);
  display.setTextColor(SSD1306_WHITE);
  int fontSize = size;
  while (fontSize > 0) {
    display.setTextSize(fontSize);
    display.setCursor(10, 0);
    display.println(textLiteral);
    if (display.height() <= SCREEN_HEIGHT) {
      break; // Текст влазит
    }
    fontSize--;
  }
  display.display();     // Show initial text
}

void accessDeniedText(void){
  display.stopscroll();
  display.clearDisplay();
  display.setTextSize(2); // Draw 2X-scale text
  display.setTextColor(SSD1306_WHITE);
  display.setCursor(40, 0);
  display.println(F("Access"));
  display.setCursor(40, 16);
  display.println(F("Denied !"));
  display.display();      // Show initial text
  blinkText(3000, 100);
}

void waitForFinger(void) {
  display.clearDisplay();
  display.setTextSize(2); // Draw 2X-scale text
  display.setTextColor(SSD1306_WHITE);
  display.setCursor(10, 0);
  if(wf){
    display.println(F("Dai palec"));
  }
  else{
     display.println(F("Dai palec :)"));
  }
  wf = !wf;
  display.display();      // Show initial text
  delay(100);
  //display.startscrolldiagright(0x00, 0x07);
}

void blinkText(unsigned long duration, int interval) {
  unsigned long startTime = millis();
  while (millis() - startTime < duration) {
    display.invertDisplay(true);
    display.display();
    delay(interval);
    display.invertDisplay(false);
    display.display();
    delay(interval);
  }
}
