using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DSSwitcher : SwitcherBase, ISwitcherEvent
{
    void Start()
    {
        if(status ==  ESwitcherStatus.ON) {
           ChangeStatus(status); 
        }
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

}
