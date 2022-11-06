using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PracticeSession : MonoBehaviour
{
    

    public static PracticeSession instance;


    public Action<bool> OnPracticeEnd;

    private Transform curCircuitBoard;


    private JCircuitBoardItem board;
    private JCircuitBoardPractice practice;
    private ItemHandler itemHandler;
    private List<ElectricItemBase> GetAllElectricItem()
    {
        return new List<ElectricItemBase>(curCircuitBoard.GetComponentsInChildren<ElectricItemBase>());
    }

    private void ShowItemPreview(EElectricItem type)
    {
        String desc = "Thông tin chi tiết về " + type.ToString(); 
        itemHandler
        .Init(type.ToString(), desc, type)
        .Show();

    }
    public void InitSession(JPracticeHolder holder, ItemHandler handler)
    {
        itemHandler = handler;
        curCircuitBoard = transform;
        board = holder.GetBoard();
        practice = board.GetPracticeById(holder.GetPracticeId());
        string modelName = board.model;
        foreach (var item in practice.Steps)
        {
            resultSteps.Add(item.type, item.value);
        }
        SettingUpElecitricItems();
        userSteps.Clear();
    }
    private void SettingUpElecitricItems()
    {
        var listElectricItem = GetAllElectricItem();
        listElectricItem.ForEach(e =>
        {
            if (e is SwitcherBase)
            {
                SwitcherBase switcher = (SwitcherBase)e;
                switcher.OnChange += OnSwitcherChange;
                int step = FindStepIndexBySwitcherName(switcher.GetName());
                if (step != -1)
                {
                    switcher.SetStepText(step + 1);
                }


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
        if (userSteps.Count <= resultSteps.Count)
        {
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
        else
        {
            OnCompleted(false);
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
        ShowPracticeCorrectSteps(true);
        if (OnPracticeEnd != null)
        {
            OnPracticeEnd(success);
        }
    }

    private void ShowPracticeCorrectSteps(bool show)
    {
        var listElectricItem = GetAllElectricItem();
        listElectricItem.ForEach(e =>
        {
            if (e is SwitcherBase)
            {
                SwitcherBase switcher = (SwitcherBase)e;
                if (switcher.HasUsed())
                {
                    switcher.ShowStepInstruciton(show);
                }

            }
        });
    }
    private int FindStepIndexBySwitcherName(string name)
    {
        for (int i = 0; i < resultSteps.Count; i++)
        {
            var ele = resultSteps.ElementAt(i);
            if (ele.Key == name)
            {
                return i;
            }
        }
        return -1;
    }
    private Dictionary<string, string> resultSteps = new Dictionary<string, string>();
    private Dictionary<string, string> userSteps = new Dictionary<string, string>();
    public void Reset()
    {
        userSteps.Clear();
        ShowPracticeCorrectSteps(false);
        GetAllElectricItem().ForEach(electric =>
        {
            if (electric != null)
            {
                var switcher = (SwitcherBase)electric;
                if (switcher != null)
                {
                    switcher.Reset();

                }
            }
        });



    }
    public void EndPractice()
    {
        Destroy(gameObject);
    }
}
