using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static ItemManager instance;

    public void Awake() {
        instance = this;
    }
}
