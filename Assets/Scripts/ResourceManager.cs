using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public List<GameObject> electricItems = new List<GameObject>();

    public List<GameObject> circuitBoardItems = new List<GameObject>();

    public List<GameObject> dialogItems  = new List<GameObject>();
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
     public GameObject GetElectricModelByName(string name)
    {
        return electricItems.Find((e => e.name == name));
    }
    public GameObject GetCircuitBoardByModelName(string name)
    {
        return circuitBoardItems.Find((e => e.name == name.ToString()));
    }
    public GameObject GetDialogByType(EDialogType type) {
        return dialogItems.Find((e => e.name == type.ToString()));
    }

}
