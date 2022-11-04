using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PracticeSession : MonoBehaviour
{
    private JCircuitBoardItem board;
    private JCircuitBoardPractice practice;

    [SerializeField]
    private Transform circuitBoardParent;
    [SerializeField]
    private ItemHandler itemHandler;

    public static PracticeSession instance;

    public Transform electricCircuitParent;

    public Action<bool> onPracticeEnd;
    private List<ElectricItemBase> GetAllElectricItem()
    {
        return new List<ElectricItemBase>(electricCircuitParent.GetComponentsInChildren<ElectricItemBase>());
    }
    private void ShowItemPreview(EElectricItem type)
    {
        itemHandler
        .init(type.ToString(), "This is the discription which I duno", type)
        .Show();

    }
    public void initSession(JPracticeHolder holder)
    {
        board = holder.GetBoard();
        practice = board.GetPracticeById(holder.GetPracticeId());
        string modelName = board.model;
        GameObject modleObject = Instantiate(ResourceManager.instance.GetCircuitBoardByModelName(modelName), circuitBoardParent);
        AddSwitcherListener();
        foreach (var item in practice.Steps)
        {
            resultSteps.Add(item.type, item.value);
        }
        userSteps.Clear();
    }
    private void AddSwitcherListener()
    {
        var listElectricItem = GetAllElectricItem();
        listElectricItem.ForEach(e =>
        {
            if (e is SwitcherBase)
            {
                var switcher = (SwitcherBase)e;
                switcher.OnChange += OnSwitcherChange;
            }
        });
    }
    private void OnSwitcherChange(ESwitcherStatus s, SwitcherBase ins, bool isRightMouse)
    {
        Utils.Log(this, "OnSwitcherChange", ins);
        if (ins != null)
        {
            if (isRightMouse)
            {
                ShowItemPreview(ins.type);
            }
            else
            {
                LogicHandler(s, ins);
            }
        }

    }
    /**
    * Function checks every user input to turn on or off any switcher, any user step must be same with result step 
    * Step[Name of Switcher on Board, Value = ON/OFF]
    * If all user steps matches with result step => Congratulation
    * If any step doesn't match to result Sttep, user will be failed at this practice.
    * @param ESwitcherStatus s : Status of switcher ON or OD
    * @param SwitcherBase ins : instance of Switcher, contain name, type for indicator model or name from Database practice
    */
    private void LogicHandler(ESwitcherStatus s, SwitcherBase ins)
    {
        if (!userSteps.ContainsKey(ins.GetName()))
        {
            userSteps.Add(ins.GetName(), s.ToString());
        }
        else
        {
            userSteps[ins.GetName()] = s.ToString();
        }
        bool success = userSteps.Count == resultSteps.Count;
        for (int i = 0; i < userSteps.Count; i++)
        {
            var result = resultSteps.ElementAt(i);
            var userResult = userSteps.ElementAt(i);
            if (result.Key == userResult.Key && result.Value == userResult.Value)
            {

            }
            else
            {

                success = false;
                OnCompleted(false);
                break;
            }
        }
        if (success)
        {
            OnCompleted(true);
        }


    }
    public void OnCompleted(bool success)
    {
        if (success)
        {
            Utils.LogError(this, "GoodBoy! You are amazing");
        }
        else
        {
            Utils.LogError(this, "Wrong step, you're dead");
        }
        if (onPracticeEnd != null)
        {
            onPracticeEnd(success);
        }
    }
    private bool hasSwictcherInput(string type)
    {
        return userSteps.ContainsKey(type);
    }
    private int[] correctStep;
    private Dictionary<string, string> resultSteps = new Dictionary<string, string>();
    private Dictionary<string, string> userSteps = new Dictionary<string, string>();
}
