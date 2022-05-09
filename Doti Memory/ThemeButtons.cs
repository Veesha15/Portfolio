using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeButtons : MonoBehaviour // attached to each theme button
{
    private GameManager GM;
    private AudioManager AM;

    // TODO: create prefab and assign fields below in inspector
    [HideInInspector] public Button button; // allows onclick event to be added to each icon, otherwise would need to do it manually in the inspector
    [HideInInspector] public Image iconImage;
    [HideInInspector] public Image radialBarFill; // used to change fill amount of radial progress bar
    [HideInInspector] public GameObject radialBar;
    
    [HideInInspector] public int totalUnlockedFaces; // GM will update this as more cards are unlocked | default faces + the faces the player has unlocked
    [HideInInspector] public bool themeCompleted;
    [HideInInspector] public bool continueGoesHome; // when a theme is completed, the continue button opens the home screen
    [HideInInspector] public int themeIndex; // used for saving and loading game progress

    public Sprite[] themeDeck; // contains ALL the cards for that set
    public Sprite greyScaleSprite; 
    public Sprite colourSprite; 
    public Sprite newThemeSprite;
    public GameObject chain; // not all themes require chain to be removed | used empty game object for first and last themes
    public GameObject life; // not all themes add a life | used != null


    void Awake()
    {
        GM = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        AM = GameObject.Find("AUDIO MANAGER").GetComponent<AudioManager>();
        themeIndex = transform.GetSiblingIndex(); // returns index of game object within parent | first theme index = 0 etc.
        iconImage = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectTheme);
        radialBar = gameObject.transform.GetChild(0).gameObject;
        radialBarFill = radialBar.GetComponent<Image>();
    }

    // *** MY METHODS START HERE ***

    private void SelectTheme()
    {
        AM.PlaySound(AM.selectThemeSound);
        GM.currentThemeScript = this; // provides access to the selected theme's script
        CreateCardSetsFor(totalUnlockedFaces); // adds two of each unlocked face to GM currentDeck
        GM.DealDeck(); // "clears the table" as well
        GM.DisplayGameScreen();
    }


    private void CreateCardSetsFor(int faces) // adds two of each unlocked face to GM currentDeck 
    {
        int index = 0;

        for (int i = 0; i < faces * 2; i++)
        {
            if (index == faces)
            {
                index = 0;
            }

            GM.currentDeck.Add(themeDeck[index]);
            index++;
        }
    }


}
