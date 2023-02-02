INCLUDE globals.ink

Friend: I'm here, I'm here - the zombies hit hard... I'm not sure if I'm gonna make it...
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
Me: I got you, I wouldn't leave you behind
Friend: This is what you deserve, I must make it out of here
#Cutscene:3/true
Me: Nooooo..... ->END

=== goodSave ===
Me: I got you buddy, I'm not leaving you behind
Friend: Thank you... I thought I was a goner
Me: Lets get out of here #Cutscene:2/true
->END

=== badLeave ===
Me: I'm sorry, it's too close, I don't have time...
Friend: Then neither of us are making it out 
Me: How could you...
Friend: You've done nothing but betray me since we got here, you deserve this #Cutscene:4/true
->END

=== goodLeave ===
Me: I'm sorry, it's too close, I don't have time...
Friend: That's okay, at least one of us is gonna make it out of this hell hole
#Cutscene:1/true 
Me: I'll try and find a way to get you back... ->END 
