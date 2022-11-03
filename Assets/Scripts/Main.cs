using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public DialogScroll boardSelectionDialog;
   void Start() {
      boardSelectionDialog.Init(ResourceManager.instance.circuitDatabase.CircuitBoards);
      boardSelectionDialog.Show();
   }
}
