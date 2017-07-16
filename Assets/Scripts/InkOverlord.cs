using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;

public class InkOverlord : MonoBehaviour {

    static InkOverlord _instance;

    public static InkOverlord IO
	{
		get 
		{
            return _instance;
        }
	}

    public void ChangeVariable(string key, object v)
    {
        _inkStory.variablesState[key] = v;
    }

    [SerializeField] TextAsset _storyScript;
    Story _inkStory; 
    Person _receiver;

    int _count = 0;
    int _maxCount = 7;

    public void incrementCount()
    {
_count++;
transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = _count.ToString() + "/" + _maxCount.ToString();
Camera.main.GetComponent<AudioSource>().Play();
    }

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
        if(_inkStory.HasFunction(knot+".SL"))
        {
            _inkStory.ChoosePathString(knot + ".SL");
            string s = _inkStory.Continue();
            s = s.Trim();
            Debug.Log(s+ "- " + s.Length);
            if(s == "NONE")
            {
                return string.Empty;
            }
            else 
                return s;
        }
        else
        {
            Debug.Log(knot + ".SL not found");
            return string.Empty;
        }
    }

    public string GetCurrentIdentity()
    {
        if(_inkStory != null)
        {
            return _inkStory.variablesState["IDENTITY"].ToString();
        }
        return "NaN";
    }
    
    public void SwitchIdentity(string identity)
    {
        _inkStory.variablesState["IDENTITY"] = identity;
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
    List<string> RandomPersonExisting;
    List<string> RandomPersonAvailable;
    // Use this for initialization
    void Awake()
    { 
	
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        _inkStory = new Story(_storyScript.text);
        _inkStory.BindExternalFunction("CANSWITCH", () =>
       {
           if(!_receiver.IsSwitchable)
               incrementCount();
           _receiver.MakeSwitchable();
       });

       _inkStory.BindExternalFunction("TRIGGEREVENT", (string id) =>
       {
           InkEventWatcher.Trigger(id);
       });
    }

    void Start()
    {
        transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = 0 + "/" + _maxCount.ToString();
    }

    public string RequestRandomPerson()
    {
        if(RandomPersonExisting == null)
        {
            RandomPersonExisting = new List<string>();
            for (int i = 0; i < (int)_inkStory.variablesState["RANDOM_COUNT"]; i++)
            {
                RandomPersonExisting.Add("RANDOM" + i.ToString());
            }
            RandomPersonAvailable = new List<string>(RandomPersonExisting);
        }

        if(RandomPersonAvailable.Count == 0)
        {
            RandomPersonAvailable = new List<string>(RandomPersonExisting);
        }

        int index = UnityEngine.Random.Range(0, RandomPersonAvailable.Count);
        string knot = RandomPersonAvailable[index];
        RandomPersonAvailable.Remove(knot);
        return knot;
    }

    public string GetRealKnot(string knotPath)
    {
        string trueKnot = knotPath + ".DIALOGUE";
        if (_inkStory.HasFunction(trueKnot))
        {
            return trueKnot;
        }
        return string.Empty;
    }
	public bool RequestKnot(Person receiver, string knotPath)
	{
		if(_receiver != null)
		{
            Debug.LogError("Request denied");
        }
		else
		{
            string trueKnot = GetRealKnot(knotPath);
            if(trueKnot != string.Empty)
            {
                _inkStory.ChoosePathString(trueKnot);
                _receiver = receiver;
                return true;
            }
        }
        return false;
    }

    public void Revoke(TextBox receiver)
    {
        _receiver = null;
    }

}
