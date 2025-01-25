import RPi.GPIO as GPIO
import socket, time
from threading import Thread

# ====
# GPIO
# ====

def set_all_pins(state):
    for channel in CHANNELS:
        GPIO.output(channel, state)

def set_hot_cold(pair, state):
    cold, hot = HOT_COLD_PAIRS[pair]
    GPIO.output(cold, state == 1)
    GPIO.output(hot, state == 2)

def pwm_handler():
    while RUN_PWM:
        for pair, config in PWM.items():
            start, period, state = config
            t = time.time() - start
            on = (t % (2*period)) < period
            set_hot_cold(pair, state if on else 0)

        time.sleep(PWM_TIME)

def shutoff():
    global PWM
    PWM = {}
    set_all_pins(False)

# ==========
# NETWORKING
# ==========

def receive_data(sock):
    data = None
    try: 
        data, _ = sock.recvfrom(4096)
    except TimeoutError:
        # No activity, cut all power!
        print ("Timeout, shutting off now...")
        shutoff()
        return

    data_str = None
    try:
        data_str = data.decode()
    except UnicodeDecodeError:
        print("Unable to decode data!")
   
    try:
       handle_data_str(data_str)
    except Exception as err:
        print(f"Error while running command {err}")

def handle_data_str(data_str):
    data_str = data_str.strip()
    args = data_str.split(" ")
    print(f"> {data_str}")
    if len(args) == 0:
        return

    cmd = args[0]
    if cmd in COMMANDS:
        COMMANDS[cmd](args[1:])

# ========
# COMMANDS
# ========

def state_cmd(args, state):
    pair = int(args[0])
    if len(args) == 1:
        set_hot_cold(pair, state)
    elif len(args) == 2:
        period = float(args[1])
        PWM[pair] = (time.time(), period, state)

def off_cmd(args):
    if len(args) == 1:
        pair = int(args[0])
        set_hot_cold(pair, 0)
        if pair in PWM:
            del PWM[pair]
    else:
        shutoff()

def hot_cmd(args):
    state_cmd(args, 2)

def cold_cmd(args):
    state_cmd(args, 1)

# ====
# MAIN
# ====

SERV_ADDR = "0.0.0.0"
SERV_PORT = 10869
SERV_TIMEOUT = 5
CHANNELS = [11, 13, 15, 16]
HOT_COLD_PAIRS = [(11, 13), (15, 16)]
RUN_PWM = True
PWM_TIME = 0.5
PWM = {}
COMMANDS = {
    "HOT": hot_cmd,
    "COLD": cold_cmd,
    "OFF": off_cmd
}

def main():
    # Configure channels
    GPIO.setmode(GPIO.BOARD)
    for channel in CHANNELS:
        GPIO.setup(channel, GPIO.OUT)
    set_all_pins(False)

    # Start socket
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.bind((SERV_ADDR, SERV_PORT))
    sock.settimeout(SERV_TIMEOUT)

    print(f"Listening on {SERV_ADDR}:{SERV_PORT}...")

    # Start PWM thread
    pwm_thread = Thread(target=pwm_handler)
    pwm_thread.start()

    while True:
        receive_data(sock)
    s.close()

if __name__ == "__main__":
    main()

