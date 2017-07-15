using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextBoxScalerSimple : MonoBehaviour {

    private TMP_Text textReader;
    private RectTransform tr;
    float curHeight;
    float startHeight;
    float timer = 0.0f;
    
	// Use this for initialization
	void Start () 
    {
        textReader = GetComponent<TMP_Text>();
        tr = GetComponent<RectTransform>();
        curHeight = tr.sizeDelta.y;
        startHeight = curHeight;
        
        GetComponent<TextMeshBox>().newLineCallback += ResetHeight;
    }

    void ResetHeight()
    {
        curHeight = startHeight;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if(textReader.isTextOverflowing)
        {
            curHeight += 1f;
        }
        if(curHeight != textReader.rectTransform.sizeDelta.y)
            textReader.rectTransform.sizeDelta = new Vector2(tr.sizeDelta.x, curHeight);
        
        

        /*
                group.spacing = NarrativeUI.ui.settings.lineSpacing;
                if(textReader.preferredHeight + textReader.fontSize != curHeight && timer == 0.0f)
                {
                    timer = 0.25f;
                }
                else if(timer != 0.0f)
                {
                    timer -= Time.deltaTime;

                    if(timer <= 0.0f)
                    {
                        timer = 0.0f;
                        curHeight = textReader.preferredHeight + textReader.fontSize +20f;
                    }
                }*/
        //     Debug.Log(tr.sizeDelta = new Vector2(tr.sizeDelta.x, reader.preferredHeight+reader.fontSize));
    }
}
