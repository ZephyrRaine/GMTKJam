using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barilchecked : MonoBehaviour {

    public MeshRenderer _meshToTrigger;
    void OnTriggerEnter(Collider other)
	{
		if(other.tag == "BARIL")
		{
            InkEventWatcher.Trigger("SUPPORT");
            InkOverlord.IO.ChangeVariable("CANMOVE", 1);
            _meshToTrigger.enabled = true;
        }
	}
}
