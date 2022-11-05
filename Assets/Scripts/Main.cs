using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{


    public Transform dialogParent;
    public Transform circuitBoardParent;

    public PracticeSession practiceSession;

    private DialogScroll cbSelectionDialog, cbPracticesDialog;
    void Start()
    {
        ShowCBSelectionDialog();
    }
    void ShowCBSelectionDialog()
    {
        var dialogPrefab = ResourceManager.instance.GetDialogByType(EDialogType.DialogScroll);
        cbSelectionDialog = Instantiate(dialogPrefab, dialogParent).GetComponent<DialogScroll>();
        cbSelectionDialog.Init("Select your Circuit Board",
        (object data) =>
        {
            JCircuitBoardItem item = (JCircuitBoardItem)data;
            cbSelectionDialog.Hide(() =>
            {

                ShowPracticeCases(item);
            });
        },
        null);
        foreach (JCircuitBoardItem item in ResourceManager.instance.circuitDatabase.CircuitBoards)
        {
            ButtonBase button = cbSelectionDialog.AddButton();
            button.SetText(item.name);
            button.SetData(item);
        };
        cbSelectionDialog.Show();
    }
    void ShowPracticeCases(JCircuitBoardItem board)
    {
        JCircuitBoardPractice[] practices = board.practices;
        var dialogPrefab = ResourceManager.instance.GetDialogByType(EDialogType.DialogScroll);
        cbPracticesDialog = Instantiate(dialogPrefab, dialogParent).GetComponent<DialogScroll>();
        cbPracticesDialog.Init("Select your practice",
        (object data) =>
        {
            JPracticeHolder convert = (JPracticeHolder)data;
            cbPracticesDialog.Hide(() =>
            {
                InstantiatePracticeSession(convert);
            });

        },
        (object data) =>
        {
            ShowCBSelectionDialog(); // back to circuit selection dialog
        });
        foreach (JCircuitBoardPractice item in practices)
        {
            ButtonBase button = cbPracticesDialog.AddButton();
            button.SetText(item.name);
            button.SetData(new JPracticeHolder(board, item.id));
        };
        cbPracticesDialog.Show();
    }
    void InstantiatePracticeSession(JPracticeHolder holder)
    {
        practiceSession.InitSession(holder);
        practiceSession.OnPracticeEnd += OnPracticeEnd;
    }
    private void OnPracticeEnd(bool success)
    {
        var dialogPrefab = ResourceManager.instance.GetDialogByType(EDialogType.DialogNotice);
        var dialogComp = Instantiate(dialogPrefab, dialogParent).GetComponent<DialogNotice>();
        dialogComp.Init("Notice", (object data) =>
        {

        },
        (object data) =>
        {

        }
        );
        dialogComp.AddContent(success?"Congratulation! You has passed this practice":"Damn! You has killed the system");
        dialogComp.Show();
    }
}