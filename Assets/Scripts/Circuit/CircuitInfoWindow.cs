using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class CircuitInfoWindow : MonoBehaviour
{
    [SerializeField]
    private Transform contentView;

    [SerializeField]
    private GameObject buttonInfoPrefab;

    public void AddScrollContent(string buttonName,object data,ButtonBase.ClickAction action)
    {
        var go = Instantiate(buttonInfoPrefab,contentView);
        var button = go.GetComponent<ButtonBase>();
        button.SetText(buttonName);
        button.SetData(data);
        button.OnClicked+=action;
    }
}
