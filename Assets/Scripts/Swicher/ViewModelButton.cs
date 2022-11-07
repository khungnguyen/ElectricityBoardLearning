using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewModelButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text tmpLabel;
    [SerializeField]
    private Texture2D magnifier;
    public Action OnModelDetailClick;


    public void SetButtonName(string tx)
    {
        if (tmpLabel != null)
        {
            tmpLabel.SetText(tx);
        }
    }
    public void OnViewButtonClick()
    {
        OnModelDetailClick?.Invoke();
    }
    public void OnMouseEnter()
    {
        Cursor.SetCursor(magnifier, new Vector2(25, 25), CursorMode.ForceSoftware);
    }
    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }
}
