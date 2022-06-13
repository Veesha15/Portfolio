using UnityEngine;
using System;

public class Home : Interactable // attached to siagnboard
{
    [SerializeField] GameObject canvas;
    [SerializeField] Animator anim;

    public static event Action ResetEvent;
    public static event Action NewDayEvent;


    private void Start()
    {
        canvas.SetActive(true); // inactive while working because it's a big black screen
        NewDayEvent?.Invoke();
    }



    public void Sleep() // attached to button
    {
        ClosePopup();
        anim.Play("CrossFade_Start");
        ResetEvent?.Invoke();
        NewDayEvent?.Invoke();
    }


}
