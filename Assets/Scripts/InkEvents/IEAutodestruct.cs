using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEAutodestruct : InkEvent
{
    public override void Trigger()
    {
        Destroy(gameObject); 
    }
}
