using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private BoundInAndOut boundAnimation;

    [SerializeField]
    private TMP_Text tmpTitle;

    protected Action<object> okFunc;
    protected Action<object> cancelFunc;
    public virtual void Hide(Action onComplete = null)
    {
        boundAnimation.PlayBoundOutEffect(()=>{
            if(onComplete!=null) {
                onComplete();
            }
        });
    }

    public virtual Dialog Init(string title,Action<object> ok,Action<object> cancel) {
        if(tmpTitle!= null) {
            tmpTitle.SetText(title);
        }
        okFunc = ok;
        cancelFunc = cancel;
        return this;
    }

    public virtual void Show(Action onComplete = null)
    {
        boundAnimation.PlayBoundEffect(()=>{
             if(onComplete!=null) {
                onComplete();
            }
        });
    }
}
