using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public List<GameObject> electricItems = new List<GameObject>();
    public static ResourceManager instance;
    void Awake()
    {
        instance = this;
    }

    public GameObject GetElectricItemByType(EElectricItem type)
    {
        return electricItems.Find((e => e.name == type.ToString()));
    }
}
