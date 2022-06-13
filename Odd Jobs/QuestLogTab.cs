using UnityEngine;

public class QuestLogTab : MonoBehaviour // attached to prefab
{
    public QuestTab tab;
    public bool alreadyAccepted;
    


    public void ClearTab()
    {
        tab.goal = null;
        alreadyAccepted = false;
        gameObject.SetActive(false);
    }


    public void DisplayTab(QuestGoal goal)
    {
        tab.goal = goal;
        tab.DisplayInfo();
        gameObject.SetActive(true);
    }


}
