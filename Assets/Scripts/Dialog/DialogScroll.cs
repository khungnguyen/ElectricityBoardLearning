using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScroll : Dialog, IDialog, IButtonEvent
{
    [SerializeField]
    private Transform scrollContent;

    [SerializeField]
    private GameObject baseButton;
    public override void Hide()
    {
        base.Hide();
    }
    public override void Show()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        base.Show();
    }
    public override void Init(params object[] arg)
    {
        foreach (var i in arg)
        {
            JCircuitBoardItem board = (JCircuitBoardItem)i;
            var button = Instantiate(baseButton);
            button.transform.SetParent(scrollContent);
            var buttonComp = button.GetComponent<ButtonBase>();
            if (buttonComp != null)
            {
                buttonComp.SetText(board.name);
                buttonComp.SetData(board);
                buttonComp.OnClicked += OnClicked;
            }
        }

    }

    public void OnClicked(object action)
    {

    }

}
