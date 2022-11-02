using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCircuitManager : MonoBehaviour
{
    const string TAG ="ElectricCircuitManager";
    [SerializeField]
    private ItemHandler itemHandler;
    
    public static ElectricCircuitManager instance;

    public Transform electricCircuitParent;

    public void Awake()
    {
        instance = this;
    }
    public List<ElectricItemBase> GetAllElectricItem() {
        return new List<ElectricItemBase>(electricCircuitParent.GetComponentsInChildren<ElectricItemBase>());
    }
    void Start() {
        var listElectricItem = GetAllElectricItem();
        listElectricItem.ForEach(e=>{
            if(e is SwitcherBase) {
                var switcher = (SwitcherBase)e;
                switcher.OnChange+=OnSwitcherChange;
            }
        });
    }
    public void OnSwitcherChange(ESwitcherStatus s, SwitcherBase ins, bool isRightMouse){
        Utils.Log(TAG,"OnSwitcherChange", ins);
        if(ins != null && isRightMouse) {
            ShowItemPreview(ins.type);
        }
    }
    public void ShowItemPreview(EElectricItem type) {
        itemHandler.init(type.ToString(),"This is the discription which I duno",type);
        itemHandler.gameObject.SetActive(true);
    }
}
