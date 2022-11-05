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
    protected ESwitcherStatus status = ESwitcherStatus.OFF;

    private int step = -1;
    void Awake()
    {
        if (stepParent)
        {
            stepParent.SetActive(false);
        }

    }
    public void OnSwitcherClicked()
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
    public void ShowItemStep()
    {
        stepParent.SetActive(true);
    }

}
