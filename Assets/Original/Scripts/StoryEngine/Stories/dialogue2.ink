INCLUDE globals.ink

{ firstTime == true : 
    -> first
    - else:
    { pickMedkit == true:
        friend: I see you picked up the medkit. #speaker:Friend
        { hasMedkit == true:
            -> medkitDialogue
            -else :
            -> useMedkitDialogue
        }
    - else:
            ->getMedkitDialogue
    }
}


=== first ===
~ firstTime = false
Me: Jim are you okay? You look awful #speaker:Player
friend: no I'm not, they nearly got me back there, they came from the darkness #speaker:Friend
Me: who came from the darkness? #speaker:Player
friend: not who, but what… don’t you remember the ENEMIES from Lost in the Woods, they're here #speaker:Friend
Me: how did this happen? How have we ended up here, do you remember anything? #speaker:Player
friend: no my memory is foggy, I only remember waking up in these woods and I've been searching since *cough* #speaker:Friend
{pickMedkit == true:
    friend: I see you picked up the medkit. #speaker:Friend
    { hasMedkit == true :
        -> medkitDialogue
    -else :
        -> useMedkitDialogue
    }
    - else:
    ->getMedkitDialogue
}
-> END

=== medkitDialogue ===
friend: Please can I have the medkit? I'm really low on health, feels like it anyway #speaker:Friend
    * [Give Medkit]
        ~ giveMedkit = true
        ~ friendship_meter += 7
        friend: thank you, I really needed that #speaker:Friend
        Me: your welcome, now lets focus on getting out #speaker:Player
        ->darknessComing
    * [Use Medkit]
        Me: I'm sorry #speaker:Player #MedkitUsage:true
        -> useMedkitDialogue

=== useMedkitDialogue ===
~ giveMedkit = false
~ friendship_meter -= 7
Me: I'm sorry, I was injured back there and needed it for myself #speaker:Player
friend: …. You used it for yourself? Do you not see my condition here. Fine, whatever... let's work on getting out of here #speaker:Friend
Me: ... #speaker:Player
->darknessComing

=== getMedkitDialogue ===
friend: could you hand me that medkit you picked up? I could really use it right now #speaker:Friend
friend: The medkit, behind you, by the light - can you get it please? #speaker:Friend
-> END

=== darknessComing ===
Me: something weird is going on, I can't seem to get over this fence, it's like theres a force stopping me									
Friend: yeah I've noticed it too, it's like we're really the characters from Lost in the Woods.
Me: do you remember what the game was about?
Friend: no, we only just started playing it… I remember finding it on a random game forum #cutscene:darknessApproaches1
Friend: the darkness, it's moving towards us… it's closing in on us					
Friend: I think we need to continue forward and try and find a way out
Me: okay good idea… #cutscene:0 #disable:true
->END
