INCLUDE globals.ink

-> main

=== main ===
Which pokemon do you choose?
    + [Better friend]
        -> chosen("Better friend", -10)
    + [Worse friend]
        -> chosen("Worse friend", 10)
    + [Nothing]
        -> chosen("Nothing", 0)
        

=== chosen(choice, meterChange) ===
~ friendship_meter = friendship_meter + meterChange
You've chosen {friendship_meter}
-> END