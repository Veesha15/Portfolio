using UnityEngine;
using UnityEngine.UI;

public class QuestBoardTab : MonoBehaviour // attached to prefab
{
    private QuestLog questLog;
    
    [SerializeField] private Button tickBox;
    [SerializeField] private GameObject tickMark;

    public QuestTab tab;
    public QuestLogTab linkedTab; // links the quest log tab with the quest board tab

    
    private void Awake()
    {
        questLog = FindObjectOfType<QuestLog>();
        tickBox.onClick.AddListener(ToggleDelivery);
    }



    private void ToggleDelivery() // added to tickbox button via listener
    {
        if (!tickMark.activeSelf) // if unticked
        {
            for (int i = 0; i < questLog.tabs.Length; i++) // loop thru quest log tabs
            {
                QuestLogTab questLogTab = questLog.tabs[i]; 

                if (!questLogTab.gameObject.activeSelf) // if tab is empty
                {
                    questLogTab.DisplayTab(this.tab.goal);
                    linkedTab = questLogTab;
                    tickMark.SetActive(true);
                    return;
                }
            }
        }

        else // if ticked
        {
            linkedTab.ClearTab();
            linkedTab = null;
            tickMark.SetActive(false);
        }
    }


    public void DisplayTab(QuestGoal goal)
    {
        tab.goal = goal;
        tab.DisplayInfo();
        gameObject.SetActive(true);
    }


    public void ClearTab()
    {
        tab.goal = null;
        linkedTab = null;
        tickMark.SetActive(false);
        gameObject.SetActive(false);
    }


}








