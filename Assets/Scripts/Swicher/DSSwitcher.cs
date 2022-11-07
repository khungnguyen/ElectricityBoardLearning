using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DSSwitcher : SwitcherBase, ISwitcherEvent//,IPointerEnterHandler,IPointerExitHandler
{
   
    public TMP_Text tmpStep;
    public ViewModelButton viewButton;

    public Animator animator;

    public GameObject stepParent;

    public GameObject highLight;

    public Action<SwitcherBase> onViewButtonClick;

    protected int step = -1;
    void Awake()
    {
        if (stepParent)
        {
            stepParent.SetActive(false);
        }
    }

    void Start()
    {
        if (status == ESwitcherStatus.ON)
        {
            ChangeStatus(status);
        }
        if (viewButton != null)
        {
            viewButton.OnModelDetailClick += OnViewButtonClick;
        }
        highLight.SetActive(false);
        base.Start();
    }

    public override void OnSwitcherClicked()
    {
        base.OnSwitcherClicked();
        ChangeStatus(status);
    }
    public override void ChangeStatus(ESwitcherStatus status)
    {
        Utils.Log(this, gameObject.name, status);
        switch (status)
        {
            case ESwitcherStatus.ON:
            case ESwitcherStatus.OFF:
                animator.SetTrigger(status.ToString());
                break;
        }
        base.ChangeStatus(status);
    }
    public void SetStepText(int i)
    {
        tmpStep.SetText(i.ToString());
        step = i;
    }
    public bool HasUsed()
    {
        return step != -1;
    }
    public void ShowStepInstruciton(bool show)
    {
        stepParent.SetActive(show);
    }
    void OnValidate()
    {
        var off = transform.Find("OnOff/Off");
        var on = transform.Find("OnOff/On");
        if (off)
        {
            off.gameObject.SetActive(status == ESwitcherStatus.OFF);
        }
        if (on)
        {
            on.gameObject.SetActive(status == ESwitcherStatus.ON);
        }
    }
    protected void ResetTrigger()
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }
    }
   
    public override void SetName(string t)
    {
        if (viewButton != null)
        {
            viewButton.SetButtonName(t);
        }
        base.SetName(t);
    }
    public void SetViewButtonListener(Action<SwitcherBase> action)
    {
        onViewButtonClick = action;
    }
    private void OnViewButtonClick()
    {
        onViewButtonClick?.Invoke(this);
    }
     public void OnMyMouseEnter()
    {
        highLight.SetActive(true);
    }
    public void OnMyMouseExit()
    {
        highLight.SetActive(false);

    }

    // void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    // {
    //      OnMyMouseEnter();
    // }
    // void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    // {
    //     OnMyMouseExit();
    // }
}
