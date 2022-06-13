using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour // attached to player
{
    [SerializeField] GameManager GM;
    [SerializeField] Inventory inventory;

    public List<QuestGoal> activeQuests = new List<QuestGoal>(); // keeps track of completed quests
    public QuestLogTab[] tabs; // set amount | max amount of quests that can be accepted at a time



    private void OnEnable()
    {
        Home.ResetEvent += ResetQuestLog;
    }

    private void OnDisable()
    {
        Home.ResetEvent -= ResetQuestLog;
    }



    public void CheckQuest(Customer customer)
    {
        if (activeQuests.Count > 0)
        {
            for (int i = 0; i < activeQuests.Count; i++)

                if (activeQuests[i] == customer.questGoal) // if customer is on player's list
                {
                    if (inventory.inventoryDict.TryGetValue(customer.questGoal.item, out int stackSize)) // if player has item
                    {
                        if (GM.currentEnergy >= customer.questGoal.energyCost) // if player has enough energy
                        {
                            customer.DisplayMessage(Message.ThankYou);
                            CompleteQuest(customer.questGoal);
                        }

                        else
                        {
                            customer.DisplayMessage(Message.NoEnergy);
                        }
                    }

                    else
                    {
                        customer.DisplayMessage(Message.NoItem, customer.questGoal.item.icon);
                    }
                }

                else
                {
                    customer.DisplayMessage(Message.NoQuest);
                }

        }

        else
        {
            customer.DisplayMessage(Message.NoQuest);
        }
    }


    private void CompleteQuest(QuestGoal goal) // called from Check Quest
    {
        GM.AddToStats(Stat.Coin, goal.reward);
        GM.RemoveFromStats(Stat.Energy, goal.energyCost);
        inventory.RemoveFromInventory(goal.item);
        activeQuests.Remove(goal);

        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i].tab.goal == goal)
            {
                tabs[i].ClearTab();
            }
        }
    }


    public void ResetQuestLog()
    {
        activeQuests.Clear();

        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].ClearTab();
        }
    }


}
