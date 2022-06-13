using UnityEngine;

public class QuestBoard : Interactable // attached to signboard
{
    [SerializeField] private QuestLog questLog;
    public QuestBoardTab[] tabs; // set amount | max amount of quests

    

    private void OnEnable()
    {
        Home.ResetEvent += ResetQuestBoard;
    }

    private void OnDisable()
    {
        Home.ResetEvent -= ResetQuestBoard;
    }


    
    private void ResetQuestBoard()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].ClearTab();
        }
    }


    public void AcceptQuests() //attached to button
    {
        print("accept");
        for (int i = 0; i < tabs.Length; i++)
        {
            QuestLogTab linkTab = tabs[i].linkedTab;

            if (linkTab != null && !linkTab.alreadyAccepted)
            {
                questLog.activeQuests.Add(linkTab.tab.goal);
                linkTab.alreadyAccepted = true;
                tabs[i].gameObject.SetActive(false);
            }
        }
        ClosePopup();
    }


}

