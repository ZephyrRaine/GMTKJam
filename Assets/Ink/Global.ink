INCLUDE SINGLE_LINES.ink

VAR IDENTITY = "CHILD"
VAR RANDOM_COUNT = 11


VAR GOOD = false
VAR OPEN = false
VAR SAVED = false
VAR CANMOVE = false

EXTERNAL CANSWITCH()
EXTERNAL TRIGGEREVENT(id)

=== MOM
= DIALOGUE
{
    -IDENTITY == "CHILD":
    You won't go anywhere today...
    ...
    Sorry...
    ->DONE
    -else:
    Oh.
    It's you.
    Great...
    {CANSWITCH()}
    ->DONE
}
->DONE
= SL
{
    - IDENTITY == "CHILD":
    I'm sorry
    ->DONE
    - IDENTITY == "DAD":
    NONE
    ->DONE
    - else:
    NONE
    ->DONE
}

=== DAD
= DIALOGUE
{
    - IDENTITY == "CHILD":
    {
        - MOM.DIALOGUE:
        I thought <i>you</i> could go out.
        I know <size=6>I can</size>
        {CANSWITCH()}
        ->DONE
        - else:
        You should go now.
        ->DONE
    }
    - IDENTITY == "MOM" && !OPEN:
    Can we go out now?
    +["Let's stay here for a while..."] "It's frightening outside."
        "I don't want to loose you."
        ->DONE
    +["Sure..."] "...we all gotta die anyway.
    ~OPEN = true
        {TRIGGEREVENT("MURZONE0")}
        ->DONE
    -else:
    NONE
    ->DONE
}
= SL
{
    -IDENTITY == "CHILD":
    {
    - MOM.DIALOGUE:
        i'm sorry
        ->DONE
    - else:
        thanks
        ->DONE
    }
    -else :
    NONE
    ->DONE
}

===SUPPORT
= DIALOGUE
{
    - SAVED:
    {
        - CANMOVE:
        Well...
        I must admit I feel lighter...
        {CANSWITCH()}
        ->DONE
        - else:
        What do you need this time?
        My job ain't enough?
        What do you <b><size="12">WANT</size></b>??
        ->DONE
    }
    - else :
        I'm doing a very important job.
        +["What is it?"]
        I'm not sure.
        But sometimes I just want to go home and see my family.
        ++["What happens if you move?"]
        I don't know...
        Probably something terrible?
        "..."
        ->DONE
        ++["Why don't you quit?"] 
        I'm afraid.
        "..."
        Oh right, because I guess you're never afraid of anything?
        ->DONE
        ++["I'm sorry..."] <size="6">"I don't really care."</size>
        {TRIGGEREVENT("BARIL")}
        ~SAVED = true
        Oh.
        What did you.
        Oh god.
        Oh god.
        ->DONE
        +["Sorry"] "I'm leaving now."
            Well, you better be.
            ->DONE
        
}
= SL
NONE
->DONE

=== DUDE
= DIALOGUE
Dude. 
I've got a secret to share with you.
{
    - IDENTITY == "DUDERANDOM":
    There's some weird stuff going on up there...
    Wanna check it out?
    {CANSWITCH()}
    ->DONE
    - else:
    Well, I mean, with Dude.
    ->DONE
}
= SL
NONE
->DONE
=== DUDERANDOM
= DIALOGUE
Dude, 
I realised that the key to life is seeing what IS there, rather than what isn't. 
Then you appreciate everything. 
Dude. 
Then, a few hours later, I realised doing this is hard, when you realise you are nothing.
{CANSWITCH()}
->DONE

=== GOD 
= SL
"I was almost waiting."
->DONE
= DIALOGUE
    "Here am I."
    "You've been waiting..."
{
    -GOOD:
    "And you did it..."
    +["Why are you talking like me?"]
    "Maybe you're the one talking like me..."
    {CANSWITCH()}
    ->DONE
    +["What is going on?"]
    "You tell me..."
    {CANSWITCH()}
    ->DONE
    - else:
    "But not enough"
    ->DONE
}
=== LOST
= SL
Haa, it feels good to be alone...
->DONE
= DIALOGUE
Oh.
Great.
Someone. Already.
You're looking for something?
Oh, I see, thinking I'm lost, maybe?
Well, fuck you, sir.
I never get lost.
{CANSWITCH()}
->DONE

== function CANSWITCH() ==
~return

== function TRIGGEREVENT(id) ==
~return