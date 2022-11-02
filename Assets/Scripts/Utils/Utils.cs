
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Utils
{
    public static bool IsPointerOverUI(string tag)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        foreach (RaycastResult raysastResult in raysastResults)
        {
            if (raysastResult.gameObject.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
    public static void Log(string tag, params object[] agr)
    {

        string message = "";
        for (int i = 0; i < agr.Length; i++)
        {
            message += agr[i].ToString();
            message += " ";
        }
        Debug.Log("[" + tag.GetType() + "]" + message);
     }
}
