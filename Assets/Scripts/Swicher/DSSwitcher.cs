using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DSSwitcher : SwitcherBase, ISwitcherEvent
{
    ISwitcherEvent onSwitch;

 
    public void OnSwitcherClicked()
    {
       base.OnSwitcherClicked();
        ChangeStatus(status);
    }
    void ChangeStatus(ESwitcherStatus status) {
        switch(status) {
            case ESwitcherStatus.ON:
            case ESwitcherStatus.OFF:
            animator.SetTrigger(status.ToString());
            break;
        }
        //Quy
    }
}
