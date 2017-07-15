using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChoiceManager : ChoiceManager 
{
	public enum CHOICES_DISPLAY_MODE
	{
		IMMEDIATE,
		DELAYED
	}
	
    public CHOICES_DISPLAY_MODE _choicesDisplayMode;
    // Use this for initialization
    void Start () 
	{
		
	}

    public override void DisplayChoices()
    {
        ClickableTextBox[] textBoxes = GetComponentsInChildren<ClickableTextBox>();
        for (int i = 0; i < textBoxes.Length; i++) //(ClickableTextBox ctb in textBoxes)
        {
            ClickableTextBox ctb = textBoxes[i];
            Debug.Log(_choicesDisplayMode);
            if (_choicesDisplayMode == CHOICES_DISPLAY_MODE.DELAYED)
            {
                ctb.ReadLine(i * 0.5f + 0.25f, 1f);
            }
            else
            {
                ctb.ReadLine();
            }
        }
    }
}
