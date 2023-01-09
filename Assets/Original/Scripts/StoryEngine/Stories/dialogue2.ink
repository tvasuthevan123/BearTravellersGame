INCLUDE globals.ink

{ firstTime == true : -> first }
{ hasMedkit == true : -> medkitDialogue | ->getMedkitDialogue}

=== first ===
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
        friend: thank you, I really needed that #speaker:Friend
        Me Your welcome, now lets focus on getting out #speaker:Player
        ->END
    * [Use Medkit]
        ~ giveMedkit = false
        Me: I'm sorry, I was injured back there and needed it for myself #speaker:Player
        friend: …. You used it for yourself? Do you not see my condition here. Fine, whatever... let's work on getting out of here #speaker:Friend
        Me: ... #speaker:Player
        ->END
-> END

=== getMedkitDialogue ===
friend: The medkit, behind you, over that way - can you get it please? #speaker:Friend
-> END
