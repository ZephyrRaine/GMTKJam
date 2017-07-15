using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DeprecatedTextBox : MonoBehaviour 
{
    Text _textComponent = null;
    public float _readingSpeed = 0f;
    float _characterTimer = 0f;
    public string _currentString = string.Empty;
    int _currentIndex = 0;

    bool _scaleWidth;
    bool _scaleHeight;
	
    void Start() 
	{
        _textComponent = GetComponent<Text>();
        _textComponent.text = "";
    }

	public void FeedLine(string s)
	{
        _currentString = s;
    }
	
	// Update is called once per frame
	void Update () 
	{
		if(_currentString != string.Empty && _textComponent.text != _currentString)
		{
           _characterTimer -= Time.deltaTime;
		   if(_characterTimer <= 0f)
		   {
                _textComponent.text += _currentString[_currentIndex++];
                _characterTimer = _readingSpeed;
            }
        }
	}
}
