using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScroll : Dialog, IDialog, IButtonEvent
{
    [SerializeField]
    private Transform scrollContent;

    [SerializeField]
    private GameObject baseButton;
    public override void Hide(Action complete = null)
    {
        base.Hide(complete);
    }
    public override void Show(Action complete = null)
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        base.Show(complete);
    }
    public override Dialog Init(string title, Action<object> ok, Action<object> cancel)
    {
        return base.Init(title, ok, cancel);
    }
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
    public void OnClicked(object action)
    {
        var convert = (JCircuitBoardItem)action;
        Utils.Log("convert", convert.name);
        if (okFunc != null)
        {
            okFunc(convert);
        }
    }
}
