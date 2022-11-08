using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PracticeSession : MonoBehaviour
{


    public static PracticeSession instance;


    public Action<bool> OnPracticeEnd;

    private JCircuitBoardItem board;
    private JCircuitBoardPractice practice;
    private ItemHandler itemHandler;

    private Dictionary<string, string> resultSteps = new();
    private Dictionary<string, string> userSteps = new();

    private CircuitBoard circuitBoard;

    /**
    * Get All electric items from circuit board
    * return ElectricItemBase
    */
    private List<ElectricItemBase> GetAllElectricItemsOnBoard()
    {
        return circuitBoard.GetAllElectricItems();
    }

    /**
    *
    * When buttong click, a pop up will display to show the 3D model
    * All information will be got from database (JElectricItemDatabase)
    * @param EElectricItem type : Type of Model, which define in database 
    *
    */
    private void ShowDetailElectricModel(EElectricItem type)
    {
        // Res
        JElectricItem item = ItemManager.instance.GetElectricItemByType(type);
        if (item != null)
        {
            itemHandler
            .Init(item.title, item.description, item.model)
            .Show();
        }


    }

    /**
    *
    * Instantiate new practice session
    * Display all items, set item to default values
    * Add listener for each item button
    */
    public void InitSession(JPracticeHolder holder, ItemHandler handler)
    {
        itemHandler = handler;
        board = holder.GetBoard();
        practice = board.GetPracticeById(holder.GetPracticeId());
        circuitBoard = GetComponent<CircuitBoard>();
        foreach (var item in practice.GetCorrectSteps())
        {
            resultSteps.Add(item.type, item.value);
        }
        userSteps.Clear();
        SettingUpCircuitBoard();

    }
    private void SettingUpCircuitBoard()
    {
        var listElectricItem = GetAllElectricItemsOnBoard();
        Dictionary<EElectricItem, SwitcherBase> uniqueElectricItemType = new();
        listElectricItem.ForEach(switcher =>
        {
            if (switcher is DSSwitcher ds)
            {
                ds.OnChange += OnSwitcherChange;
                ds.SetViewButtonListener((SwitcherBase sw) =>
                {
                    ShowDetailElectricModel(sw.type);
                });
                int step = FindStepIndexBySwitcherName(switcher.GetName());
                var defaultItem = practice.GetDefaultItemStatus().Find(item => item.type == switcher.GetName());
                if (defaultItem != null)
                {
                    ds.ChangeStatus(Utils.String2Enum<ESwitcherStatus>(defaultItem.value));
                }
                if (step != -1)
                {
                    ds.SetStepText(step + 1);
                }
                if (!uniqueElectricItemType.ContainsKey(ds.type))
                {
                    uniqueElectricItemType.Add(ds.type, ds);
                }

            }
        });
        foreach (var e in uniqueElectricItemType)
        {
            circuitBoard.AddElectricItemTypeInfoSection(e, (object data) =>
            {
                if (data is SwitcherBase sw)
                {
                    ShowDetailElectricModel(sw.type);
                }

            });
        }
        circuitBoard.InitBoard(practice.name);

    }

    /**
    * When switcher is click, event would be notified to the listener for handling
    * @param s : ESwitcherStatus status of switcher : ON/OF
    * @param ins : SwitcherBase  an instance of Switcher
    */
    private void OnSwitcherChange(ESwitcherStatus s, SwitcherBase ins, bool isRightMouse)
    {
        Utils.Log(this, "OnSwitcherChange", ins);
        if (ins != null)
        {
            if (isRightMouse)
            {
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

    /**
    * Trigger when User finishes or made a wrong step
    * @param bool success :
    */
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
        var listElectricItem = GetAllElectricItemsOnBoard();
        listElectricItem.ForEach(e =>
        {
            if (e is DSSwitcher ds)
            {
                if (ds.HasUsed())
                {
                    ds.ShowStepInstruciton(show);
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
    public void Reset()
    {
        userSteps.Clear();
        ShowPracticeCorrectSteps(false);
        GetAllElectricItemsOnBoard().ForEach(electric =>
        {
            if (electric != null)
            {
                var switcher = (SwitcherBase)electric;
                var defaultItem = practice.GetDefaultItemStatus().Find(item => item.type == switcher.GetName());
                if (defaultItem != null)
                {
                    switcher.ChangeStatus(Utils.String2Enum<ESwitcherStatus>(defaultItem.value));
                }
            }
        });



    }
    public void EndPractice()
    {
        Destroy(gameObject);
    }
}
