using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public abstract class ChoiceManager : MonoBehaviour 
{
    public static ChoiceManager CreateChoiceManager(Transform from)
    {
        GameObject go = GameObject.Instantiate(ModelsLibrary.ML.choiceBox, from.transform.position + Vector3.up * 2f + ((Camera.main.transform.position - from.position).normalized * .5f), (Camera.main.transform.rotation), from);
        ChoiceManager choiceManager = go.GetComponent<ChoiceManager>();
        if (choiceManager != null)
        {
            return choiceManager;
        }
        else
        {
            return go.AddComponent<ChoiceManager>();
        }
    }

	[System.Serializable]
	public enum ChoicesDisplayMode
	{
		IMMEDIATE,
		DELAYED
	}
	
	[SerializeField]
    public GameObject _textBoxModel;
    // Use this for initialization

    public delegate void InputDelegate(int input);
    public delegate void AddDelegate(Transform t);
    public InputDelegate Input;
    public AddDelegate AddBox;

    public bool IsBusy {
        get
        {
            return (transform.childCount != 0);
        }
    }
    public virtual void DisplayChoices()
	{
        TextBox [] textBoxes = GetComponentsInChildren<TextBox>();
		foreach(TextBox ctb in textBoxes)
		{
            ctb.ReadLine();
        }
    }

    private TextBox InstantiateTextBox(string s)
    {
        TextBox box;
        if (_textBoxModel == null)
        {
            GameObject go = new GameObject();
            go.transform.SetParent(transform);
            box = go.AddComponent<TextBox>();
        }
        else
        {
            GameObject go = Instantiate(_textBoxModel, transform.position+(Vector3.down*0.35f*(transform.childCount+1)), transform.rotation, transform);
            box = go.GetComponent<TextBox>();
        }
        box.FeedLine(s);

        return box;
    }

    public virtual void FeedChoices(List<Choice> choices)
	{
		foreach(Choice c in choices)
		{
            TextBox box = InstantiateTextBox(c.text);	
        }

        DisplayChoices();
    }

	public virtual void ClearChoices()
	{
		foreach(Transform t in transform)
		{
            Destroy(t.gameObject);
        }
	}
}
