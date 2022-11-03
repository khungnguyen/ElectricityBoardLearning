using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private BoundInAndOut boundAnimation;

    public virtual void Hide()
    {
        boundAnimation.PlayBoundOutEffect(()=>{

        });
    }

    public virtual void Init(params object[] arg) {

    }

    public virtual  void Show()
    {
        boundAnimation.PlayBoundEffect();
    }
}
