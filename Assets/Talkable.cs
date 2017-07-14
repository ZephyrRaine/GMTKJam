using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class Talkable : MonoBehaviour {

    // Use this for initialization
    TextAsset _inkScript;
    TextBox _textBox;
    void Start () 
	{
        GetComponentInChildren<Interactable>()._dEnter += PlayerNearby;
        GetComponentInChildren<Interactable>()._dExit += PlayerLeave;
    }



    void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position, transform.position);
		// Gizmos.DrawIcon(transform.position, )
    }

    void PlayerNearby(GameObject player)
	{
        player.GetComponent<Controller>()._dInteractOnce += PlayerTalk;
    }

	void PlayerTalk()
	{
		if(_textBox == null)
		{
        	_textBox =TextBox.CreateTextBox(transform, _inkScript);
        }
		else
		{

		}
    }

	void PlayerLeave(GameObject player)
	{
		if(_textBox != null)
		{
        	Destroy(_textBox.gameObject);
		}
        player.GetComponent<Controller>()._dInteractOnce -= PlayerTalk;
    }

	void PlayerSwitch()
	{

	}

    // Update is called once per frame
    void Update () {
		
	}

}
