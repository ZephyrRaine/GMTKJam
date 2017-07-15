using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelsLibrary : MonoBehaviour {

    // Use this for initialization
    static ModelsLibrary _instance;
    public static ModelsLibrary ML
	{
		get 
		{
            return _instance;
        }
	}

    public GameObject textBox;
    public GameObject choiceBox;
    public Material controllerMaterial;
    public Material randomMaterial;

    void Awake () 
	{
		if(_instance == null)
        	_instance = this;
		else
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
