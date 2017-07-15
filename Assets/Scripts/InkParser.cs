using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class InkParser : MonoBehaviour 
{
    public TextAsset _jsonStoryAsset;
    public string _startingPath;
    public bool _playOnStart;
    private Story _inkStory;
    private TextMeshBox _textBox;
    private ChoiceManager _choiceManager;
    private bool _isWaiting;
    // Use this for initialization
    void Start () 
	{
        _inkStory = new Story(_jsonStoryAsset.text);
        

        _inkStory.ChoosePathString(_startingPath);
     
        //External functions binding
    }

    public void Update()
    {
        
    }

    void GetLine()
    {
    }
	// Update is called once per frame
	
}
