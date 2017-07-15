using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickableTextBox : TextMeshBox, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool _isAvailable;
    public Color _colorHover;
    public Color _colorPressed;

    public ChoiceManager.InputDelegate Input;

    public void Start()
    {
        RealStart();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("HOVER HERE");
        if(_isAvailable)
        {
            _textComponent.color = _colorHover;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("DOWN HERE");
        if(_isAvailable)
        {
            _textComponent.color = _colorPressed;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("UP HERE");

        if(_isAvailable && Input != null)
        {
            Input(transform.GetSiblingIndex());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("EXIT HERE");

        if(_isAvailable)
        {
            _textComponent.color = _colorDefault;
        }
    }
  
}
