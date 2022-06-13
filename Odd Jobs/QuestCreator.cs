using System.Collections.Generic;
using UnityEngine;

public class QuestCreator : MonoBehaviour // attached to game manager
{
    [SerializeField] QuestBoard board;

    [SerializeField] private List<Customer> allCustomers; // remains constant | used to reset customerList
    [SerializeField] private List<Item> itemList; // quest items

    private List<QuestGoal> goalList = new List<QuestGoal>(); // vacant goal instances added at start | remains constant, only goal info is updated during game play
    private List<Customer> customerList; // removed from when setting up daily quests | to prevent adding same customer twice


    private void Start()
    {
        CreateVacantGoal();  
    }


    // ***** EVENTS *****
    private void OnEnable()
    {
        Home.NewDayEvent += CreateQuest;
    }

    private void OnDisable()
    {
        Home.NewDayEvent -= CreateQuest;
    }



    private void CreateVacantGoal() // creates goal instance + adds it to a list to be accessed later
    {
        for (int i = 0; i < board.tabs.Length; i++)
        {
            QuestGoal goal = new QuestGoal();
            goalList.Add(goal);
        }
    }

    private void AssignGoalInfo(QuestGoal goal) 
    {
        int randomCustomer = Random.Range(0, customerList.Count);
        int randomItem = Random.Range(0, itemList.Count);
        int randomTip = Random.Range(1, 5);

        goal.customer = customerList[randomCustomer];
        goal.item = itemList[randomItem];
        goal.reward = (itemList[randomItem].load + goal.customer.difficulty) + randomTip;
        goal.energyCost = (itemList[randomItem].load + goal.customer.difficulty);

        customerList[randomCustomer].questGoal = goal; 
        customerList.RemoveAt(randomCustomer); // don't add customer already in dict
    }

    private void CreateQuest()
    {
        int amount = Random.Range(2, board.tabs.Length); // the amount of quests for this day
        customerList = new List<Customer>(allCustomers);

        for (int i = 0; i < amount; i++)
        {
            QuestGoal goal = goalList[i]; 
            QuestBoardTab tab = board.tabs[i];

            AssignGoalInfo(goal);
            tab.DisplayTab(goal);
        }
    }



}
