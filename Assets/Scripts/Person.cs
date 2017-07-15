using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
public class Person : MonoBehaviour {

    // Use this for initialization
    TextBox _textBox;

    [SerializeField]
    string _inkKnot;
    private bool _engaged;
    private bool _isWaiting;

    public delegate void FeedbackDelegate();
    public FeedbackDelegate _dFinishedTalking;

    public string Identity 
    {
        get 
        {
            return _inkKnot;
        }
    }

    bool _lastLine;

    bool? _isEngageable;

    bool _isSwitchable = false;
    public bool IsSwitchable
    {
        get
        {
            return _isSwitchable;
        }
    }
    public void MakeSwitchable()
    {
        _isSwitchable = true;
        GetComponentInChildren<ParticleSystem>().Play();
    }
    public bool IsEngageable
    {
        get
        {
            if(_isEngageable == null)
            {
                _isEngageable = InkOverlord.IO.GetRealKnot(_inkKnot) != string.Empty; 
            }
            return _isEngageable.Value;
        }
    }

    private ChoiceManager _choiceManager;
    private string _singleLine;

    void Start () 
	{
        _dFinishedTalking += Disengage;
        if(_inkKnot == string.Empty)
        {
            _inkKnot = InkOverlord.IO.RequestRandomPerson();
            MakeSwitchable();
        }
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
            nextLine = nextLine.Trim();
            if(nextLine == "NONE")
            {
                Disengage();
                return;
            }
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
        if (_inkKnot != string.Empty)
            _singleLine = InkOverlord.IO.GetSpecialSingleLine(_inkKnot);
        else
            _singleLine = string.Empty;

		if(_textBox == null && _singleLine != string.Empty)
		{
        	_textBox = TextBox.CreateTextBox(transform);
            _textBox.FeedLine(_singleLine);
            if(_inkKnot.StartsWith("RANDOM"))
            {
                _textBox.ReadLine(Random.Range(0f, 1.5f), Random.Range(0.1f, 0.5f));
            }
            else
            {
                _textBox.ReadLine();
            }
        }
    }

    public void Engage()
    {
        if (_inkKnot != string.Empty)
        {
           if(IsEngageable)
           {
                if (_textBox != null)
                {
                    Destroy(_textBox.gameObject);
                }
                _textBox = TextBox.CreateTextBox(transform);
                InkOverlord.IO.RequestKnot(this, _inkKnot);
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
        if(IsSwitchable)
            GetComponentInChildren<ParticleSystem>().Play();
    }

    public void NotNearby(GameObject player)
    {
        if(player != null) 
            GetComponentInChildren<ParticleSystem>().Stop();
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

    bool _released;
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

        if (_engaged)
        {
            Debug.Log("ENGAGED");
            if (_lastLine)
            {
                if (_released && Input.GetButtonDown("Interact"))
                {

                    if (_dFinishedTalking != null)
                        _dFinishedTalking();
                }
            }
            else if (_isWaiting && Input.GetButtonDown("Interact"))
            {
                _isWaiting = false;
                _released = false;
                _textBox.ReadLine();
            }

            if (Input.GetButtonUp("Interact"))
            {
                _released = true;
            }

        }
	}

}
