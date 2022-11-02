using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherBase : MonoBehaviour
{

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
    }
}
