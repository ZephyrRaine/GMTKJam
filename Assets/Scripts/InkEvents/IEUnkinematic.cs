using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEUnkinematic : InkEvent {
    public override void Trigger()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
