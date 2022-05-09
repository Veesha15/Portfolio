using UnityEngine;
using UnityEngine.UI;

public class DeckCreator : MonoBehaviour // attached to empty game object
{
    [SerializeField] private GameManager GM;

    [SerializeField] private Transform cardContainer; // canvas that will contain and display the cards
    [SerializeField] private GameObject cardPrefab;

    private int deckSize = 24; // max number of cards to ever be displayed on screen


    private void Awake()
    {
        GM.cardBack = cardPrefab.GetComponent<Image>().sprite;
    }

    void Start()
    {
        CreateCardDeck();
    }

    public void CreateCardDeck() // create the max number of cards that will ever be displayed on screen
    {
        for (int i = 0; i < deckSize; i++)
        {
            GameObject createdCard = Instantiate(cardPrefab); // temporary var to get access to the instantiated card
            createdCard.transform.SetParent(cardContainer, false);
            createdCard.SetActive(false); 

            CardController cardScript = createdCard.GetComponent<CardController>(); // CardController script attached to prefab
            GM.cardScripts.Add(cardScript); // allows GM to access required fields
        }
    }


}

