using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour // parent class, not attached to game object
{
    private PlayerMovement playerMovement; // TODO: change to event
    private bool playerInRange;
    
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject popupWindow;


    protected virtual void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        closeButton.onClick.AddListener(ClosePopup);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInRange = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }



    private void OnMouseDown()
    {
        if (playerInRange)
        {
            popupWindow.SetActive(true);
            playerMovement.playerCanMove = false;
            InteractWith();
        }
    }

    protected virtual void InteractWith()
    {
        // if additional actions are required when player clicks on interactable
    }


    public void ClosePopup()
    {
        popupWindow.SetActive(false);
        playerMovement.playerCanMove = true;
    }


}
