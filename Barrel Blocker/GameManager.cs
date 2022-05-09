using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI buildText;
    public TextMeshProUGUI hideText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI gameOverText;
    public Button startButton;
    public Button restartButton;

    public bool gameIsActive = false;
    public bool playerIsDead = false;
    public bool destroyRogueObstacles = false;


    public void StartGame()  // assigned to start button | additionally the SpawnManager script has a listener that starts the WaveSpawner coroutine
    {
        gameIsActive = true;
        startButton.gameObject.SetActive(false);
    }

    public void GameOver()  // game is over when Enemy collides with Player | called in PlayerController script
    {
        gameIsActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame() // assigned to a restart button availible at end of the game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
