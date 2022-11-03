using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject scrollDialogPrefab;

    public Transform dialogParent;

    private DialogScroll cbSelectionialog, cbPracticesDialog;
    void Start()
    {
        ShowCBSelectionDialog();
    }
    void ShowCBSelectionDialog()
    {
        cbSelectionialog = Instantiate(scrollDialogPrefab, dialogParent).GetComponent<DialogScroll>();
        cbSelectionialog.Init("Select your Circuit Board",
        (object data) =>
        {
            JCircuitBoardItem item = (JCircuitBoardItem)data;
            Utils.Log(GetType().Name, "showCBSelectionDialog", item.practices.Length);
            cbSelectionialog.Hide(() =>
            {
                ShowPracticeCases(item.practices);
                Destroy(cbSelectionialog.gameObject);
            });
        },
        null);
        foreach (var item in ResourceManager.instance.circuitDatabase.CircuitBoards)
        {
            ButtonBase button = cbSelectionialog.AddButton();
            button.SetText(item.name);
            button.SetData(item);
        };
        cbSelectionialog.Show();
    }
    void ShowPracticeCases(JCircuitBoardPractice[] practices)
    {
        cbPracticesDialog = Instantiate(scrollDialogPrefab, dialogParent).GetComponent<DialogScroll>();
        cbPracticesDialog.Init("Select your practice",
        (object data) =>
        {
            JCircuitBoardPractice convert = (JCircuitBoardPractice)data;
            Utils.Log(GetType().Name, "showPracticeCases", convert.name);
        },
        null);
        foreach (JCircuitBoardPractice item in practices)
        {
            ButtonBase button = cbPracticesDialog.AddButton();
            button.SetText(item.name);
            button.SetData(item);
        };
        cbPracticesDialog.Show();
    }
}
