INCLUDE globals.ink

{ friendship_meter >= 0 : -> goodDeath | -> badDeath }

=== goodDeath ===
{ section == "beforeZombie1" : So insignificant... Aren't you... Though I wonder... do you know the secret and have given up already?}
{ section == "zombie1" : It seems as though the challenge was a bit too much for you, if only you know what was ahead...}
{ section == "walk2" : So close, yet so far...}
{ section == "zombie2" : The Darkness may be a better friend than those pesky creatures ay... if only you could be with your friend}
{ section == "zombie3" : So close... but to what...}
->END

=== badDeath ===
{ section == "beforeZombie1" : So insignificant... Aren't you... Greed is misplaced in you, attempting to use others when you cannot use yourself properly... embarassing}
{ section == "zombie1" : Too much to deal with when there isn't someone to stab in the back?}
{ section == "walk2" : Had time to contemplate on your choices? Is it regre that's brought you bac-I mean, here?}
{ section == "zombie2" : Fear not, betrayal is common place here...}
{ section == "zombie3" : Maybe in the next run...}
->END
