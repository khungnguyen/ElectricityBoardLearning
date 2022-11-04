using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScroll : Dialog, IButtonEvent
{
    [SerializeField]
    private Transform scrollContent;

    [SerializeField]
    private GameObject baseButton;
    public ButtonBase AddButton(GameObject button)
    {
        var buttonbase = Instantiate(button, scrollContent).GetComponent<ButtonBase>();
        buttonbase.OnClicked += OnClicked;
        return buttonbase;
    }
    public ButtonBase AddButton()
    {
        return AddButton(baseButton);
    }
    public void OnClicked(object data)
    {
        if (okFunc != null)
        {
            okFunc(data);
        }
    }
}
