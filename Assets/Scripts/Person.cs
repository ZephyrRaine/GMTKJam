using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class Person : MonoBehaviour {

    // Use this for initialization
    TextBox _textBox;

    [SerializeField]
    int _singleLine;
    [SerializeField]
    string _inkKnot;
    private bool _engaged;
    private bool _isWaiting;

    public delegate void FeedbackDelegate();
    public FeedbackDelegate _dFinishedTalking;

    bool _lastLine;

    public bool _isEngageable
    {
        get
        {
            return _inkKnot != string.Empty;
        }
    }

    private ChoiceManager _choiceManager;

    void Start () 
	{
        _dFinishedTalking += Disengage;
    }

    void GetLine()
    {
        GetLine(false);
    }
    void GetLine(bool isImmediate)
    {
        if(InkOverlord.IO.canContinue)
        {
            string nextLine = InkOverlord.IO.NextLine();
        

            if (isImmediate)
            {
                _textBox.ReadLine(nextLine);
            }
            else
            {
                _isWaiting = true;
                _textBox.FeedLine(nextLine);
            }

            //Tags parsing
        }
        else if (!_choiceManager.IsBusy && InkOverlord.IO.hasChoices)
        {
            List<Choice> list = InkOverlord.IO.GetChoices();
            _choiceManager.FeedChoices(list);
        }
        else
        {
            _lastLine = true;
        }
    }

    public void Choice(int choice)
    {
        _choiceManager.ClearChoices();
        InkOverlord.IO.MakeChoice(choice);
        GetLine(true);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position, transform.position);
		// Gizmos.DrawIcon(transform.position, )
    }
	public void Talk()
	{
		if(_textBox == null && _singleLine != -1)
		{
        	_textBox = TextBox.CreateTextBox(transform);
            _textBox.FeedLine(InkOverlord.IO.GetSingleLine(_singleLine));
            _textBox.ReadLine();
        }
    }

    public void Engage()
    {
        if (_inkKnot != string.Empty)
        {
            if (_textBox == null)
            {
                _textBox = TextBox.CreateTextBox(transform);
                InkOverlord.IO.RequestKnot(_textBox, _inkKnot);
                _choiceManager = ChoiceManager.CreateChoiceManager(transform);
                _choiceManager.Input += Choice;
                _textBox.finishedCallback += GetLine;
                GetLine(true);
                _engaged = true;
                _lastLine = false;
            }
        }
    }
    public void Disengage()
    {
        NotNearby(null);
        _engaged = false;
    }
    public void IsNearby(GameObject player)
    {
        
    }

    public void NotNearby(GameObject player)
    {
        if(_engaged)
        {
            InkOverlord.IO.Revoke(_textBox);
            _engaged = false;
        }
        if (_textBox != null)
        {
            Destroy(_textBox.gameObject);
        }
        if(_choiceManager != null)
        {
            Destroy(_choiceManager.gameObject);
        }
    }

    // Update is called once per frame
    void Update () 
    {
        if(_textBox != null)
        {
            _textBox.transform.rotation = (Camera.main.transform.rotation);
            _textBox.transform.position = transform.position + Vector3.up * 2f + ((Camera.main.transform.position - transform.position).normalized * .75f);
        }
        if(_choiceManager != null)
        {
            _choiceManager.transform.rotation = (Camera.main.transform.rotation);
            _choiceManager.transform.position = transform.position + Vector3.up * 2f + ((Camera.main.transform.position - transform.position).normalized * .75f);
        }

        Debug.Log("UPDATE");
        if (_engaged)
        {
            Debug.Log("ENGAGED");
            if (Input.GetButtonDown("Interact"))
            {
                if(_lastLine)
                {
                    if(_dFinishedTalking != null)
                        _dFinishedTalking();
                }
                else if (_isWaiting)
                {
                    _isWaiting = false;
                    _textBox.ReadLine();
                }
            }
            else if (_textBox._isReading)
            {
                //    _textBox.DisplayImmediate();
            }
        }
	}

}
