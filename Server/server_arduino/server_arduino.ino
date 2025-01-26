#include <WiFi.h>
#include <WiFiUdp.h>

#define BLUE_LED 2
#define ACTIVITY_LED 5
#define HOT_PIN 18
#define COLD_PIN 19
#define BLINK_MS 5
#define SHUTDOWN_MS 5000

const char* WIFI_SSID = "ZORRO";
const char* WIFI_PASSWORD = "fb7141ae80";
const unsigned int UDP_PORT = 10869; // Port to listen on


WiFiUDP udp;          // Create a UDP instance
char PACKET_DATA[255]; // Buffer to store incoming messages
unsigned long LAST_RECEIVED_MSG_TIME = 0;

void set_hot_cold(int state) {
  digitalWrite(COLD_PIN, state == 1);
  digitalWrite(HOT_PIN, state == 2);
}

void setup() {
  // put your setup code here, to run once:
  pinMode(BLUE_LED, OUTPUT);
  pinMode(ACTIVITY_LED, OUTPUT);
  pinMode(HOT_PIN, OUTPUT);
  pinMode(COLD_PIN, OUTPUT);

  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);

  digitalWrite(BLUE_LED, LOW);
  digitalWrite(ACTIVITY_LED, LOW);
  set_hot_cold(0);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
  }

  udp.begin(UDP_PORT);

  digitalWrite(BLUE_LED, HIGH);
}

void loop() {
  unsigned long now = millis();

  // Check for new data.
  if (udp.parsePacket()) {
    LAST_RECEIVED_MSG_TIME = now;
    int len = udp.read(PACKET_DATA, sizeof(PACKET_DATA));

    char cmd = PACKET_DATA[0];
    switch (cmd) {
      case 'O':
        set_hot_cold(0);
        break;
      case 'C':
        set_hot_cold(1);
        break;
      case 'H':
        set_hot_cold(2);
        break;
      default:
        break;
    }
  }

  // If we haven't heard in a while, turn off hot/cold.
  if (now - LAST_RECEIVED_MSG_TIME >= SHUTDOWN_MS) {
    set_hot_cold(0);
  }

  // network activity led
  digitalWrite(ACTIVITY_LED, now - LAST_RECEIVED_MSG_TIME < BLINK_MS ? HIGH : LOW);
}
