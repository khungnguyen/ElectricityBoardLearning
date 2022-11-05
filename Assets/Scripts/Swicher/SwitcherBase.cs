using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SwitcherBase : ElectricItemBase, IPointerDownHandler
{

    public delegate void OnSwitcherChange(ESwitcherStatus status, SwitcherBase instance, bool isRightMouse);
    public event OnSwitcherChange OnChange;
    public Animator animator;

    public TMP_Text tmpStep;

    public GameObject stepParent;
    public ESwitcherStatus status = ESwitcherStatus.OFF;

    private ESwitcherStatus defaultStatus;
    private int step = -1;
    void Awake()
    {
        if (stepParent)
        {
            stepParent.SetActive(false);
        }
        defaultStatus = status;
    }
    public virtual void OnSwitcherClicked()
    {
        if (status == ESwitcherStatus.OFF)
        {
            status = ESwitcherStatus.ON;
        }
        else if (status == ESwitcherStatus.ON)
        {
            status = ESwitcherStatus.OFF;
        }
        if (OnChange != null)
        {
            OnChange(status, this, false);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnChange != null && eventData.button == PointerEventData.InputButton.Right)
        {
            OnChange(status, this, true);
        }
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
    public virtual void ChangeStatus(ESwitcherStatus status)
    {
        this.status = status;
    }
    public ESwitcherStatus GetStatus()
    {
        return status;
    }
    public ESwitcherStatus GetDefaultStatus()
    {
        return defaultStatus;
    }
    void OnValidate()
    {
        var off = transform.Find("Off");
        var on = transform.Find("On");
        off.gameObject.SetActive(status == ESwitcherStatus.OFF);
        on.gameObject.SetActive(status == ESwitcherStatus.ON);
    }
    public void Reset()
    {
        ChangeStatus(defaultStatus);  
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
}
