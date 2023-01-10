INCLUDE globals.ink

{ firstTime == true : -> first }
{ hasMedkit == true : -> medkitDialogue | ->getMedkitDialogue}

=== first ===
~ firstTime = false
Me: FRIEND are you okay? You look awful #speaker:Player
friend: no I'm not, they nearly got me back there, they came from the darkness #speaker:Friend
Me: who came from the darkness? #speaker:Player
friend: not who, but what… don’t you remember the ENEMIES from GAMENAME, they're here #speaker:Friend
Me: how did this happen? How have we ended up here, do you remember anything? #speaker:Player
friend: no my memory is foggy, I only remember waking up in these woods and I've been searching since *cough* #speaker:Friend
friend: could you hand me that medkit behind you? I could really use it right now #speaker:Friend
-> END

=== medkitDialogue ===
friend: Please can I use the medkit? I'm really low on health, feels like it anyway #speaker:Friend
    * [Give Medkit]
        ~ giveMedkit = true
        ~ friendship_meter += 15
        friend: thank you, I really needed that #speaker:Friend
        Me Your welcome, now lets focus on getting out #speaker:Player
        ->darknessComing
    * [Use Medkit]
        ~ giveMedkit = false
        ~ friendship_meter -= 15
        Me: I'm sorry, I was injured back there and needed it for myself #speaker:Player
        friend: …. You used it for yourself? Do you not see my condition here. Fine, whatever... let's work on getting out of here #speaker:Friend
        Me: ... #speaker:Player
        ->darknessComing

=== getMedkitDialogue ===
friend: The medkit, behind you, over that way - can you get it please? #speaker:Friend
-> END

=== darknessComing ===
Me: something weird is going on, I can't seem to get over this fence, it's like theres a force stopping me									
Friend: yeah I've noticed it too, it's like we're really the characters from GAMENAME.
Me: do you remember what the game was about?
Friend: no, we only just started playing it… I remember finding it on a random game forum
#cutscene:darknessApproaches1
Friend: the darkness, it's moving towards us… it's closing in on us					
Friend: I think we need to continue forward and try and find a way out
Me: okay good idea…
#cutscene:friendRuns
#disable:true
->END
