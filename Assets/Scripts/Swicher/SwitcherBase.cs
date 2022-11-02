using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitcherBase : ElectricItemBase, IPointerDownHandler
{

    public delegate void OnSwitcherChange(ESwitcherStatus status, SwitcherBase instance, bool isRightMouse);
    public event OnSwitcherChange OnChange;
    public Animator animator;

    protected ESwitcherStatus status = ESwitcherStatus.OFF;
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

}
