using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum Message
{
    NoQuest,
    NoItem,
    NoEnergy,
    ThankYou
}

public class Customer : Interactable // attached to NPCs
{
    private QuestLog questLog;

    [SerializeField] private TextMeshProUGUI popupMessage, popupTitle;
    [SerializeField] private Image customerImage, messageImage;
    [SerializeField] private Sprite noQuestSprite, noEnergySprite, thankYouSprite;

    [HideInInspector] public Sprite customerIcon;
    [HideInInspector] public QuestGoal questGoal;
    [HideInInspector] public int difficulty;


    protected override void Awake()
    {
        base.Awake();
        questLog = FindObjectOfType<QuestLog>();
        difficulty = Random.Range(1, 10);
        customerIcon = gameObject.GetComponent<SpriteRenderer>().sprite;
    }


    protected override void InteractWith()
    {
        base.InteractWith();
        popupTitle.text = gameObject.name;
        customerImage.sprite = customerIcon;
        questLog.CheckQuest(this);
    }



    public void DisplayMessage(Message msg, Sprite sprite = null) // optional parameter
    {
        switch (msg)
        {
            case Message.NoQuest:
                popupMessage.text = $"{this.name} is not on your list.";
                messageImage.sprite = noQuestSprite;
                break;

            case Message.NoItem:
                popupMessage.text = $"You don't have {this.name}'s item.";
                messageImage.sprite = sprite;
                break;

            case Message.NoEnergy:
                popupMessage.text = "You don't have enough energy to complete the delivery.";
                messageImage.sprite = noEnergySprite;
                break;

            case Message.ThankYou:
                popupMessage.text = "Delivery Complete!\nThank you.";
                messageImage.sprite = thankYouSprite;
                break;
        }
    }

}
