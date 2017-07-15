INCLUDE SINGLE_LINES.ink

VAR identity = "CHILD"

=== MOM_SL
{
    - identity == "DAD":
        i'm glad to see you, it's over now...
        ->DONE
    - else:
        i'm sorry
        ->DONE
}
->DONE
=== MOM
you won't go anywhere today...
...
sorry...
->DONE
=== DAD_SL
{
    - MOM:
        i'm sorry
    - else:
        thanks
}
->DONE
=== DAD
{
    - MOM:
    i thought <i>you</i> could go out.
    i know <b>I</b> can
    ->DONE
    - else:
    you should go now.
    ->DONE
}


