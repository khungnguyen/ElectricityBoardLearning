using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricItemBase : MonoBehaviour
{
   public EElectricItem type = EElectricItem.DS;
   
   public string defaultName;
   public string GetName() {
      return defaultName!=null && defaultName.Length>0?defaultName:gameObject.name;
   }
}
