using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour // attached to card Prefab
{
    private GameManager GM;
    private AudioManager AM;

    public Button button;
    public Sprite face;
    public int timesRevealed;


    void Awake()
    // using Start gives error: Object reference not set to an instance of an object AT AssignFaces() cardScripts[i].button.image.sprite = cardBack;
    // think because card is instantiated before GetComponent<Button>() is run
    {
        GM = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        AM = GameObject.Find("AUDIO MANAGER").GetComponent<AudioManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(RevealCard); // not setup on prefab because there is no object to drag into On Click
    }

    public void RevealCard()
    {
        if (GM.revealedCards.Count < 2 && button.image.sprite != face)
        {
            AM.PlaySound(AM.turnCardSound);
            button.image.sprite = face;
            timesRevealed++;
            GM.revealedCards.Add(button);

            if (timesRevealed >= 2)
            {
                GM.revealMaxReached = true;   
            }
        }
    }
}

