using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSSwitcher : SwitcherBase, ISwitcherEvent
{
    public void OnSwitcherClicked()
    {
       base.OnSwitcherClicked();
        Debug.LogError("[DSSwitcher]" + " OnSwitcherClicked");
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
