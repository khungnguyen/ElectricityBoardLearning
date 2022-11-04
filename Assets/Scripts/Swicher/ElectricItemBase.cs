using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricItemBase : MonoBehaviour
{
   public EElectricItem type;
   public string name;
   public string GetName() {
      return name!=null && name.Length>0?name:gameObject.name;
   }
}
