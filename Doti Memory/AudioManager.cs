using UnityEngine;

public class AudioManager : MonoBehaviour // attached to empty game object
{
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioSource soundAudio;

    public AudioClip failureSound;
    public AudioClip lifeSound;
    public AudioClip successSound;
    public AudioClip newCardSound;
    public AudioClip newThemeSound;
    public AudioClip endGameSound;
    public AudioClip matchSound;
    public AudioClip turnCardSound;
    public AudioClip selectThemeSound;
    public AudioClip buttonSound;

    public bool musicIsOn { get; private set; }
    public bool soundIsOn { get; private set; }

    // MY METHODS START HERE

    public void turnMusicOn()
    {
        musicAudio.Play();
        musicIsOn = true;
    }

    public void turnMusicOff()
    {
        musicAudio.Stop();
        musicIsOn = false;
    }

    public void turnSoundOn()
    {
        soundAudio.mute = false; 
        soundIsOn = true;
    }

    public void turnSoundOff()
    {
        soundAudio.mute = true;
        soundIsOn = false;
    }

    public void PlaySound(AudioClip clip)
    {
        soundAudio.PlayOneShot(clip);
    }
   
}
