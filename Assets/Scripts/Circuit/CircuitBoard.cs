using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CircuitBoard : MonoBehaviour
{
    public CircuitInfoWindow infoWindow;
    public TMP_Text tmpPracticeTitle;

    public Transform electricItemParent;

    public void InitBoard(string title)
    {
        tmpPracticeTitle.SetText(title);
    }
    public List<ElectricItemBase> GetAllElectricItems()
    {
        var list = electricItemParent.GetComponentsInChildren<ElectricItemBase>();
        return new List<ElectricItemBase>(list);
    }
    public void AddElectricItemTypeInfoSection(KeyValuePair<EElectricItem, SwitcherBase> data,ButtonBase.ClickAction action)
    {
        infoWindow.AddScrollContent(data.Value.type.ToString(), data.Value,action); 
    }
}
