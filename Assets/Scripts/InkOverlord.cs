using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class InkOverlord : MonoBehaviour {

    static InkOverlord _instance;
    public static InkOverlord IO
	{
		get 
		{
            return _instance;
        }
	}

    [SerializeField] TextAsset _storyScript;
    Story _inkStory;
    TextBox _receiver;

	public bool canContinue
	{
		get 
		{
            return _inkStory.canContinue;
        }
	}
    public bool hasChoices
    {
        get
        {
            return _inkStory.currentChoices.Count > 0;
        }
    }
	public string NextLine()
	{
       return _inkStory.Continue();
    }

	public List<Choice> GetChoices()
	{
        return _inkStory.currentChoices;
    }

    public string GetSpecialSingleLine(string knot)
    {
        _inkStory.ChoosePathString(knot+"_SL");
        if(_inkStory.canContinue)
        {
            return _inkStory.ContinueMaximally();
        }
        else
        {
            return string.Empty;
        }
    }

    public void SwitchIdentity(string identity)
    {
        _inkStory.variablesState["identity"] = identity;
    }

	public bool MakeChoice(int index)
	{
		if(index < _inkStory.currentChoices.Count)
		{
            _inkStory.ChooseChoiceIndex(index);
            return true;
        }
        Debug.LogError("INVALID CHOICE INDEX");
        return false;
    }

    // Use this for initialization
    void Awake()
    { 
	
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        _inkStory = new Story(_storyScript.text);
    }

	public void RequestKnot(TextBox receiver, string knotPath)
	{
		if(_receiver != null)
		{
            Debug.LogError("Request denied");
        }
		else
		{
        	_receiver = receiver;
            _inkStory.ChoosePathString(knotPath);
        }
    }

    public void Revoke(TextBox receiver)
    {
        _receiver = null;
    }

    public string GetSingleLine(int index)
    {
        _inkStory.ChoosePathString("SINGLE_LINES.SL" + index.ToString());
        return _inkStory.Continue();
    }
}
