using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour {

	public static TextBox CreateTextBox(Transform from, TextAsset script)
	{
        GameObject go = GameObject.Instantiate(ModelsLibrary.ML.textBox, from.transform.position + Vector3.up * 2f + ((Camera.main.transform.position-from.position).normalized * .5f),  (Camera.main.transform.rotation), from);
        TextBox textBox = go.GetComponent<TextBox>();
        if(textBox != null)
		{
            return textBox;
        }
		else
		{
            return go.AddComponent<TextBox>();
        }
    }
	// Use this for initialization
	void Start () 
	{

	}
	// Update is called once per frame
	void Update () 
	{
		
	}
}
