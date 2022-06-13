using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestTab : MonoBehaviour // attached to prefab
{
    [SerializeField] private Image customerImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI energyCostText;
    [SerializeField] private TextMeshProUGUI rewardText;

    public QuestGoal goal;


    // ***** METHODS *****

    public void DisplayInfo()
    {
        customerImage.sprite = goal.customer.customerIcon;
        itemImage.sprite = goal.item.icon;
        rewardText.text = goal.reward.ToString();
        energyCostText.text = goal.energyCost.ToString();
    }
}
