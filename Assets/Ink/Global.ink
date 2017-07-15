INCLUDE SINGLE_LINES.ink

VAR IDENTITY = "CHILD"
VAR RANDOM_COUNT = 11

VAR OPEN = false

EXTERNAL CANSWITCH()
EXTERNAL TRIGGEREVENT(id)

=== MOM
= DIALOGUE
{
    -IDENTITY == "CHILD":
    you won't go anywhere today...
    ...
    sorry...
    ->DONE
    -else:
    NONE
    {CANSWITCH()}
    ->DONE
}
->DONE
= SL
{
    - IDENTITY == "CHILD":
    i'm sorry
    ->DONE
    - IDENTITY == "DAD":
    i'm glad to see you, it's over now...
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
        - MOM:
        i thought <i>you</i> could go out.
        i know <b>I can</b>
        {CANSWITCH()}
        ->DONE
        - else:
        you should go now.
        ->DONE
    }
    - IDENTITY == "MOM" && !OPEN:
    can we go out now?
    +let's stay here for a while...
        it's frightening outside
        i don't want to loose you
        ->DONE
    +sure, let's go
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
    - MOM:
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

== function CANSWITCH() ==
~return

== function TRIGGEREVENT(id) ==
~return