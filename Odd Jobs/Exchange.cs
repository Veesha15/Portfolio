using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Exchange : Interactable // attached to signboard
{
    private GameManager GM;

    [SerializeField] private TextMeshProUGUI sliderText;
    [SerializeField] private Slider slider;
    [SerializeField] private Toggle heartToggle, coinToggle;
    [SerializeField] private Button exhangeButton;

    private TokenType tokenType;

    private enum TokenType
    {
        Heart,
        Coin
    }


    protected override void Awake()
    {
        base.Awake();
        GM = FindObjectOfType<GameManager>();
        slider.onValueChanged.AddListener((v) => { sliderText.text = v.ToString(); });
        coinToggle.onValueChanged.AddListener(delegate { ExchangeCoin(coinToggle); });
        heartToggle.onValueChanged.AddListener(delegate { ExchangeHeart(heartToggle); });
        exhangeButton.onClick.AddListener(CompleteExchange);
    }


 
    private void ExchangeCoin(Toggle toggle)
    {
        tokenType = TokenType.Coin;
        slider.maxValue = GM.currentCoin;

        if (!toggle.isOn) // reset slider if no toggle is selected
        {
            slider.maxValue = 0;
            slider.value = 0;
        }
    }


    private void ExchangeHeart(Toggle toggle)
    {
        tokenType = TokenType.Heart;
        slider.maxValue = GM.currentHeart;

        if (!toggle.isOn) // reset slider if no toggle is selected
        {
            slider.maxValue = 0;
            slider.value = 0;
        }
    }


    private void CompleteExchange()
    {
        switch (tokenType)
        {
            case TokenType.Coin:
                GM.RemoveFromStats(Stat.Coin, slider.value);
                GM.AddToStats(Stat.Heart, slider.value);
                break;

            case TokenType.Heart:
                GM.RemoveFromStats(Stat.Heart, slider.value);
                GM.AddToStats(Stat.Coin, slider.value);
                break;
        }

        ResetSlider();
        ClosePopup();
    }


    private void ResetSlider()
    {
        slider.maxValue = 0;
        slider.value = 0;
        sliderText.text = slider.value.ToString();
        heartToggle.isOn = false;
        coinToggle.isOn = false;
    }


}
