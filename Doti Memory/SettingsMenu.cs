using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour // attached to empty game object
{
    [SerializeField] private AudioManager AM;

    [SerializeField] private GameObject settingsPopup;
    [SerializeField] private Image musicTickBox;
    [SerializeField] private Image soundTickBox;
    [SerializeField] private Sprite ticked;
    [SerializeField] private Sprite unticked;
    [SerializeField] private CanvasGroup themeContainerGroup;
    [SerializeField] private CanvasGroup cardsContainerGroup;

    void Start()
    {
        LoadMusicPrefs();
        LoadSoundPrefs();
    }

    // MY METHODS START HERE

    public void ToggleSettingsPopup() // both icon and quit button run this function that works as a toggle
    {
        AM.PlaySound(AM.buttonSound);
        settingsPopup.SetActive(!settingsPopup.activeSelf);
        themeContainerGroup.blocksRaycasts = !themeContainerGroup.blocksRaycasts;
        cardsContainerGroup.blocksRaycasts = !cardsContainerGroup.blocksRaycasts;
    }

    public void ToggleMusic() // attached to button that looks like a tickbox
    {
        if (AM.musicIsOn) 
        {
            AM.turnMusicOff();
            musicTickBox.sprite = unticked;
            PlayerPrefs.SetInt("Music", 0);
        }

        else
        {
            AM.turnMusicOn();
            musicTickBox.sprite = ticked;
            PlayerPrefs.SetInt("Music", 1);
        }
    }

    public void ToggleSound() // attached to button that looks like a tickbox
    {
        if (AM.soundIsOn)
        {
            AM.turnSoundOff();
            soundTickBox.sprite = unticked;
            PlayerPrefs.SetInt("Sound", 0);
        }

        else
        {
            AM.turnSoundOn();
            soundTickBox.sprite = ticked;
            PlayerPrefs.SetInt("Sound", 1);
        }
    }

    private void LoadMusicPrefs()
    {
        int toggle = PlayerPrefs.GetInt("Music", 1); // sets default as "on"

        if (toggle == 0)
        {
            AM.turnMusicOff();
            musicTickBox.sprite = unticked;
        }

        else if (toggle == 1)
        {
            AM.turnMusicOn();
            musicTickBox.sprite = ticked;
        }
    }

    private void LoadSoundPrefs()
    {
        int toggle = PlayerPrefs.GetInt("Sound", 1); // sets default as "on"

        if (toggle == 0) // if sound is muted
        {
            AM.turnSoundOff();
            soundTickBox.sprite = unticked;
        }

        else if (toggle == 1) // if sound is unmuted
        {
            AM.turnSoundOn();
            soundTickBox.sprite = ticked;
        }
    }

    public void OpenPrivacyPolicy() // attched to button Privacy Policy
    {
        Application.OpenURL("https://vecia.co.uk/policy");
    }



}

