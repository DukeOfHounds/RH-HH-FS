## Inspiration
We were inspired by the desire to create even more advanced user interactions in VRChat, a game that we all like to play. 
## What it does
The temp-i-touch makes use of an ESP32, a couple relays, and a peltier module to apply hot or cold temperatures to the back of the users hand.
## How we built it
We used an ESP32, 2 relays, and a peltier module. The ESP32 controls the actuation of the relays, switching the direction of current through the peltier modules. This allows us to choose where to apply hot or cold temperatures to the user.
## Challenges we ran into
Originally we were planning to use the Qualcomm hardware, however after some troubles we switched over to attempting to use openBCI hardware. Again, we weren't happy with the hardware we acquired, and pivoted yet again to thermal haptics.
## Accomplishments that we're proud of
Our glove and control module are remarkably small and lightweight, being almost entirely self-contained. We managed to minimize external wired connections, with only one USB-C wire for power to the ESP32.
## What we learned
Our group is primarily composed of software-focused individuals, with this being our first real attempt on working with hardware. 
## What's next for Temp-i-Touch
Our next steps would involve an improved glove and housing for the electronics. In addition, we believe it would be relatively trivial to remove the need for the USB power source, making the device entirely self-contained. Granular temperature control should also be possible, allowing for a wider variety of desired temperature setpoints.

