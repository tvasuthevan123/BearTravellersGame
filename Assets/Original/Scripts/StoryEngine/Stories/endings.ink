INCLUDE globals.ink

Friend: I'm here, I'm here - the zombies hit hard... I'm not sure if I'm gonna make it... #speaker:Friend
{ friendship_meter >= 0 : -> goodChoices | -> badChoices }
#end:true


=== badChoices ===
    * [Save them]
        -> badSave
    * [Leave them]
        -> badLeave
->END

=== goodChoices ===
    * [Save them]
        -> goodSave
    * [Leave them]
        -> goodLeave
->END

=== badSave ===
Me: I got you, I wouldn't leave you behind #speaker:Player
Friend: This is what you deserve, I must make it out of here #speaker:Friend
Me: Nooooo..... #Cutscene:3/true #speaker:Player 
->END

=== goodSave ===
Me: I got you buddy, I'm not leaving you behind #speaker:Player
Friend: Thank you... I thought I was a goner #speaker:Friend
Me: Lets get out of here  #Cutscene:2/true #speaker:Player
->END

=== badLeave ===
Me: I'm sorry, it's too close, I don't have time... #speaker:Player
Friend: Then neither of us are making it out #speaker:Friend
Me: How could you... #speaker:Player
Friend: You've done nothing but betray me since we got here, you deserve this  #Cutscene:4/true #speaker:Friend 
->END

=== goodLeave ===
Me: I'm sorry, it's too close, I don't have time... #speaker:Player
Friend: That's okay, at least one of us is gonna make it out of this hell hole #speaker:Friend
Me: I'll try and find a way to get you back... #Cutscene:1/true #speaker:Player 
->END