using UnityEngine;
using TMPro;

public enum Stat
{
    Coin,
    Energy,
    Heart
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energyText, coinText, heartText;

    private float defaultEnergy = 10; 
    public float currentEnergy { private set; get; }
    public float currentCoin { private set; get; }
    public float currentHeart { private set; get; }  // float because value needs to work with slider


    // ***** EVENTS *****
    private void OnEnable()
    {
        Home.NewDayEvent += ResetEnergy;
    }

    private void OnDisable()
    {
        Home.NewDayEvent -= ResetEnergy;
    }



    // ***** METHODS *****
    public void ResetEnergy()
    {
        currentEnergy = defaultEnergy;
        energyText.text = currentEnergy.ToString();
    }


    public void AddToStats(Stat stat, float amount)
    {
        switch (stat)
        {
            case Stat.Energy:
                currentEnergy += amount;
                energyText.text = currentEnergy.ToString();
                break;

            case Stat.Coin:
                currentCoin += amount;
                coinText.text = currentCoin.ToString();
                break;

            case Stat.Heart:
                currentHeart += amount;
                heartText.text = currentHeart.ToString();
                break;
        }
    }


    public void RemoveFromStats(Stat stat, float amount)
    {
        switch (stat)
        {
            case Stat.Energy:
                currentEnergy -= amount;
                energyText.text = currentEnergy.ToString();
                break;

            case Stat.Coin:
                currentCoin -= amount;
                coinText.text = currentCoin.ToString();
                break;

            case Stat.Heart:
                currentHeart -= amount;
                heartText.text = currentHeart.ToString();
                break;
        }
    }

}
