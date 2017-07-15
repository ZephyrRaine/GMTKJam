using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	[HideInInspector]
    public float _radius = 0f;

    public delegate void TriggerDelegate(GameObject other);
    public TriggerDelegate _dEnter;
    public TriggerDelegate _dExit;
	public void ApplyRadius()
	{
        transform.localScale = new Vector3(_radius, 0.1f, _radius);
    }
    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.GetComponent<Controller>() != null)
        {
            Debug.Log("SALUUUT");
            GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 0f, 0.35f);
            if(_dEnter != null)
                _dEnter(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<Controller>() != null)
        {
            Debug.Log("AFEZF");
            GetComponent<MeshRenderer>().material.color = new Color(0,0,0,0);
            if (_dExit != null)
                _dExit(other.gameObject);
        }
    }

    // Update is called once per frame

}
