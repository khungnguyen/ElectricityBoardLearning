using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static ItemManager instance;

    private List<JElectricItem> itemDatabase = null;
    void Awake() {
        instance = this;
    }
    
    private List<JElectricItem> GetAllItems() {
        if(itemDatabase == null) {
            itemDatabase = new List<JElectricItem>(ResourceManager.instance.electricItemDatabase.ElectricItems);
        }
        return itemDatabase;
    }
    public JElectricItem GetElectricItemByType(EElectricItem type) {
        return GetAllItems().Find(e=>e.type == type.ToString());
    }
}
