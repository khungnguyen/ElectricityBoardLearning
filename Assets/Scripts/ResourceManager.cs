using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public List<GameObject> electricItems = new List<GameObject>();
    public TextAsset circuitDatabaseAsset;
    public TextAsset electricItemDatabaseAsset;
    public static ResourceManager instance;
    public JCircuitBoardDatabase circuitDatabase;
    public JElectricItemDatabase electricItemDatabase;
    void Awake()
    {
        instance = this;
        circuitDatabase = JsonUtility.FromJson<JCircuitBoardDatabase>(circuitDatabaseAsset.text);
        electricItemDatabase = JsonUtility.FromJson<JElectricItemDatabase>(electricItemDatabaseAsset.text);
    }

    public GameObject GetElectricItemByType(EElectricItem type)
    {
        return electricItems.Find((e => e.name == type.ToString()));
    }

}
