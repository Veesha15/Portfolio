using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour // attached to empty game object
{
    // TODO: too much on one script | move result popups to other scripts | move home/game screen to other script

    // DEPENDENCIES
    [SerializeField] private AudioManager AM;
    [SerializeField] private Animator anim;


    // ACCESSED BY OTHER SCRIPTS
    [HideInInspector] public List<CardController> cardScripts = new List<CardController>(); // allows access to all cards instantiated at start
    [HideInInspector] public List<Button> revealedCards = new List<Button>(); // allows access to cards currently turned face up
    [HideInInspector] public List<Sprite> currentDeck = new List<Sprite>(); // assigned when a theme is selected, has two of each unlocked face
    [HideInInspector] public ThemeButtons currentThemeScript; // allows access to selected theme
    [HideInInspector] public Sprite cardBack; // used for turning card face down
    [HideInInspector] public bool revealMaxReached = false; // indicates when two cards are turned face up | resets continiously during a round


    // ASSIGN IN INSPECTOR
    [SerializeField] private ThemeButtons[] themeScripts; // index is used to save and load the active theme | drag into Inspector because need to control order
    [SerializeField] private GameObject[] bannerTexts; // used to loop thru and set all inactive, then only make relevent ones active
    [SerializeField] private Sprite[] easyPopups, hardPopups; // all result possibilities | LOSE | WIN new card, new theme, endless
    [SerializeField] private GameObject gameScreen, homeScreen, resultPopup; // toggle canvases on/off
    [SerializeField] private GameObject signBoard; // becomes child of currently active theme
    [SerializeField] private Image background, resultMain, resultFrame; // images where the sprites need to be updated
    [SerializeField] private Sprite cardBackRevealed, gardenBG, oceanBG, mountainBG;
    [SerializeField] private Text signBoardText, bannerText, gamesWonText, gamesLostText, livesText;
    [SerializeField] private CanvasGroup blockCardContainer; // prevent accidentally clicking on cards in the background when result is being shown
    [SerializeField] private Transform cardContainer; // used for resizing cards
    
    
    // PRIVATE
    private Sprite[] currentThemePopups; // set to either easy or hard result popups
    private int matchedSets = 0; // keeps track of when a round is won
    private int defaultUnlockedFaces = 3; // starting amount of cards for each theme
    private int maxUnlockedFaces = 12; // max amount of faces availible per theme
    private int gamesWon, gamesLost; // keeps track of scores in endless mode
    private int maxLives = 1; // the amount each new round starts with | increases at certain intervals
    private int currentLives; // keeps track of how many you have left
    private float radialSegment = 0.111f; // ?? rather calculate in start
    private float cardContainerWidth, cardContainerHeight; // used for resizing cards
    private GridLayoutGroup cardContainerGrid; // used for resizing cards
    private WaitForSeconds shortDelay = new WaitForSeconds(0.5f);
    private WaitForSeconds longDelay = new WaitForSeconds(1.0f);

    private void Awake()
    {
        cardContainerWidth = cardContainer.GetComponent<RectTransform>().rect.width;
        cardContainerHeight = cardContainer.GetComponent<RectTransform>().rect.height;
        cardContainerGrid = cardContainer.GetComponent<GridLayoutGroup>();
    }

    private void Start()
    {
        ResetLives();
        LoadThemeProgress();
        StartCoroutine(RunGame());
    }


    // MY METHODS START HERE
    IEnumerator RunGame()
    {
        while (true)
        {
           yield return new WaitUntil(() => revealedCards.Count == 2); // wait untill two cards are face up

            if (FacesMatch() == false)
            {
                if (revealMaxReached == false)
                {
                    yield return shortDelay;
                    TurnFaceDown();
                }

                else if (revealMaxReached == true)
                {
                    if (currentLives > 0)
                    { 
                        yield return shortDelay;
                        TurnFaceDown();
                        UseLife();
                    }

                    else
                    {
                        yield return longDelay;
                        DisplayFailureScreen();
                    }
                }
            }

            else if (FacesMatch() == true)
            {
                revealMaxReached = false; // reset

                yield return shortDelay;
                AM.PlaySound(AM.matchSound);
                revealedCards[0].interactable = false;
                revealedCards[1].interactable = false;
                matchedSets++;

                if (matchedSets == currentDeck.Count / 2) // if all cards have been matched | game is won
                {
                    yield return longDelay;
                    DisplaySuccessScreen();
                }    
            }

            revealedCards.Clear();

        }
    }

    private void LoadThemeProgress()
    {
        // setup all completed themes
        for (int i = 0; i < PlayerPrefs.GetInt("SAVED_position", 0); i++) 
        {
            themeScripts[i].button.interactable = true;
            themeScripts[i].iconImage.sprite = themeScripts[i].colourSprite;
            themeScripts[i].totalUnlockedFaces = maxUnlockedFaces;
            themeScripts[i].themeCompleted = true;
            themeScripts[i+1].chain.SetActive(false);
            CheckForAddLife(themeScripts[i]);
        }

        // setup currently active theme
        if (PlayerPrefs.GetInt("SAVED_position") != themeScripts.Length) // if not all the themes have been completed
        {
            ThemeButtons activeTheme = themeScripts[PlayerPrefs.GetInt("SAVED_position", 0)];
            activeTheme.button.interactable = true;
            activeTheme.iconImage.sprite = activeTheme.greyScaleSprite;
            activeTheme.totalUnlockedFaces = PlayerPrefs.GetInt("SAVED_unlockedFaces", defaultUnlockedFaces);
            activeTheme.themeCompleted = false;
            activeTheme.radialBarFill.fillAmount = radialSegment * progressAmount(activeTheme);
            activeTheme.radialBar.SetActive(true); // radial bar ring | activeTheme.transform.GetChild(0).gameObject.SetActive(true)
            signBoard.transform.SetParent(activeTheme.transform, false); // moves signboard to active theme
            signBoardText.text = progressText(activeTheme);
            CheckForAddLife(activeTheme);
        }

        else  // if all the themes have been completed 
        {
            signBoard.SetActive(false);
            maxLives = 4;
        }

        gamesLost = PlayerPrefs.GetInt("SAVED_gamesLost", 0);
        gamesWon = PlayerPrefs.GetInt("SAVED_gamesWon", 0);
    }

    public void DealDeck()
    {
        revealedCards.Clear();
        revealMaxReached = false;
        matchedSets = 0;
        resultFrame.gameObject.SetActive(false);
        resultPopup.SetActive(false);

        foreach (GameObject text in bannerTexts)
        {
            text.SetActive(false);
        }

        for (int i = 0; i < currentDeck.Count; i++)
        {
            cardScripts[i].timesRevealed = 0;
        }

        SetCardSize();
        ShuffleDeck(currentDeck);
        AssignFaces();
        UpdateBannerText();
        UpdateScoreText();
        ResetLives();

        blockCardContainer.alpha = 1;
        blockCardContainer.interactable = true;
    } // accessed by other scripts

    private void SetCardSize()
    {
        float yPadding = 10f * (currentThemeScript.totalUnlockedFaces - 1);
        float xPadding = 10f * 3;
        float newSize;

        if (currentThemeScript.totalUnlockedFaces > 5)
        {
            newSize = (cardContainerWidth - xPadding) / 4;
            cardContainerGrid.constraintCount = 4;
        }

        else if (currentThemeScript.totalUnlockedFaces == 3)
        {
            newSize = (cardContainerWidth - 10) / 2;
            cardContainerGrid.constraintCount = 2;
        }

        else
        {
            newSize = (cardContainerHeight - yPadding) / currentThemeScript.totalUnlockedFaces;
            cardContainerGrid.constraintCount = 2;
        }

        Vector2 buttonSize = new Vector2(newSize, newSize);
        cardContainerGrid.cellSize = buttonSize;
    }

    private void ShuffleDeck(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void AssignFaces()
    {
        // Assign a face to each card on the screen
        for (int i = 0; i < currentDeck.Count; i++)
        {
            cardScripts[i].button.image.sprite = cardBack;
            cardScripts[i].face = currentDeck[i];
            cardScripts[i].button.interactable = true;
            cardScripts[i].gameObject.SetActive(true);
        }
    }

    private void UpdateBannerText()
    {
        if (!currentThemeScript.themeCompleted)
        {
            bannerText.text = progressText(currentThemeScript);
            bannerTexts[0].SetActive(true); // default mode

            if (currentThemeScript.totalUnlockedFaces != maxUnlockedFaces)
            {
                bannerTexts[1].SetActive(true); // info unlock more cards
            }

            else
            {
                bannerTexts[2].SetActive(true); // info final round
            }
        }

        else // endless mode
        {
            bannerTexts[3].SetActive(true);
        }
    }

    private void UpdateScoreText()
    {
        gamesLostText.text = gamesLost.ToString();
        gamesWonText.text = gamesWon.ToString();
    }

    private bool FacesMatch()
    {
        if (revealedCards.Count == 2 && revealedCards[0].image.sprite == revealedCards[1].image.sprite)
        { return true; }

        else
        { return false; }
    }

    private void TurnFaceDown()
    {
        revealedCards[0].image.sprite = cardBackRevealed;
        revealedCards[1].image.sprite = cardBackRevealed;
    }

    private int progressAmount(ThemeButtons theme)
    {
        return theme.totalUnlockedFaces - defaultUnlockedFaces;
    }

    private string progressText(ThemeButtons theme)
    {
        return ($"{theme.totalUnlockedFaces - defaultUnlockedFaces}/9");
    }

    public void ContinueButton()
    {
        if (currentThemeScript.continueGoesHome == false) // theme active or completed
        {
            DealDeck(); 
        }

        else if (currentThemeScript.continueGoesHome == true) 
        {
            signBoardText.text = "0/9";
            DisplayHomeScreen();
            currentThemeScript.continueGoesHome = false;
        }
    } // attached to button

    private void DisplayFailureScreen()
    {
        AM.PlaySound(AM.failureSound);
        blockCardContainer.alpha = 0; // hides cards so result popup looks nicer
        blockCardContainer.interactable = false; // prevent accidentally clicking on cards in the background that are not disbaled
 
        if (currentThemeScript.themeCompleted) // endless mode
        {
            gamesLost++;
            PlayerPrefs.SetInt("SAVED_gamesLost", gamesLost);
        }

        resultMain.sprite = currentThemePopups[0];
        resultPopup.SetActive(true);
    }

    private void DisplaySuccessScreen()
    {
        blockCardContainer.alpha = 0; // hides cards so result popup looks nicer (all cards disabled, no need for interactable block)

        if (currentThemeScript.totalUnlockedFaces != maxUnlockedFaces) // new card
        {
            AM.PlaySound(AM.newCardSound);
            AddNewCard();
            resultMain.sprite = currentThemePopups[2];
        }

        else if (currentThemeScript.themeCompleted) // endless mode
        {
            AM.PlaySound(AM.successSound);
            gamesWon++;
            PlayerPrefs.SetInt("SAVED_gamesWon", gamesWon);
            resultMain.sprite = currentThemePopups[1];
        }


        else if (currentThemeScript.themeIndex != 11) // new theme
        {
            AM.PlaySound(AM.newThemeSound);
            ThemeButtons nextthemeScript = themeScripts[currentThemeScript.themeIndex +1].GetComponent<ThemeButtons>();

            CompleteTheme(currentThemeScript);
            UnlockTheme(nextthemeScript);
            resultMain.sprite = currentThemePopups[3];
            resultFrame.sprite = nextthemeScript.newThemeSprite;
            resultFrame.gameObject.SetActive(true);
        }


        else if (currentThemeScript.themeIndex == 11) // final thank you popup
        {
            AM.PlaySound(AM.endGameSound);
            resultMain.sprite = currentThemePopups[4];
            CompleteTheme(currentThemeScript);
            signBoard.SetActive(false);
            PlayerPrefs.SetInt("SAVED_position", themeScripts.Length);
        }
        
        resultPopup.SetActive(true);
    }

    private void AddNewCard()
    {
        currentDeck.Add(currentThemeScript.themeDeck[currentThemeScript.totalUnlockedFaces]);
        currentDeck.Add(currentThemeScript.themeDeck[currentThemeScript.totalUnlockedFaces]);

        resultFrame.sprite = currentThemeScript.themeDeck[currentThemeScript.totalUnlockedFaces]; // because index starts at 0 works out to same no. as unlockedFaces
        resultFrame.gameObject.SetActive(true);
        currentThemeScript.totalUnlockedFaces++;
        PlayerPrefs.SetInt("SAVED_unlockedFaces", currentThemeScript.totalUnlockedFaces);
    }

    private void CompleteTheme(ThemeButtons theme)
    {
        theme.iconImage.sprite = theme.colourSprite;
        theme.radialBar.SetActive(false);
        theme.themeCompleted = true;
        theme.continueGoesHome = true;
    }

    private void UnlockTheme(ThemeButtons theme)
    {
        theme.iconImage.sprite = theme.greyScaleSprite;
        theme.radialBar.SetActive(true);
        theme.button.interactable = true;
        theme.totalUnlockedFaces = defaultUnlockedFaces;
        theme.chain.SetActive(false);
        signBoard.transform.SetParent(theme.transform, false);
        CheckForAddLife(theme);

        PlayerPrefs.SetInt("SAVED_position", theme.themeIndex);
        PlayerPrefs.SetInt("SAVED_unlockedFaces", defaultUnlockedFaces); // although value is set in load | if line removed "SAVED_unlockedFaces" = the last save = 12
    }

    private void ResetLives()
    {
        currentLives = maxLives;
        UpdateLifeText();
    }

    private void CheckForAddLife(ThemeButtons theme)
    {
        if (theme.life != null) // if theme has a life bonus
        {
            theme.life.SetActive(false); // remove life bonus image from home screen
            maxLives++;
            UpdateLifeText();
        }
    }

    private void UseLife()
    {
        revealMaxReached = false;
        anim.SetTrigger("Start");
        currentLives--;
        UpdateLifeText();
    }

    private void UpdateLifeText()
    {
        livesText.text = currentLives.ToString();
    }

    public void DisplayGameScreen()
    {
        if (currentThemeScript.themeIndex <= 5) // easy difficulty
        {
            background.sprite = oceanBG;
            currentThemePopups = easyPopups;
        }


        else if (currentThemeScript.themeIndex >= 5) // hard difficulty
        {
            background.sprite = gardenBG;
            currentThemePopups = hardPopups;
        }

        homeScreen.SetActive(false);
        gameScreen.SetActive(true);
    } // attached to button

    private void DisplayHomeScreen()
    {
        AM.PlaySound(AM.buttonSound);

        for (int i = 0; i < currentDeck.Count; i++)
        {
            cardScripts[i].gameObject.SetActive(false);
        }

        currentDeck.Clear();
        resultPopup.SetActive(false);

        if (currentThemeScript.themeCompleted == false)
        {
            currentThemeScript.radialBarFill.fillAmount = radialSegment * (progressAmount(currentThemeScript));
            signBoardText.text = progressText(currentThemeScript);
        }

        background.sprite = mountainBG;
        gameScreen.SetActive(false);
        homeScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    } // attached to button

}




