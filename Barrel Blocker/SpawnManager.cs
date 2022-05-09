using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager GM;

    public GameObject obstacle;
    public GameObject enemy;

    private float countDownTimer;
    private float timerDefaultValue = 21f;

    private int waveCount = 0;

    int[] spawnPoints = new int[] { -8, -7, -6, -5, -4, -3, 3, 4, 5, 6, 7, 8 };

    private void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        GM.startButton.onClick.AddListener(StartButtonClicked);
    }

    void SetTimer(float timeValue)
    {
        countDownTimer = timeValue;
    }

    IEnumerator RunTimer()
    {
        while (GM.gameIsActive)
        {
            if (countDownTimer > 0)
            {
                countDownTimer -= Time.deltaTime;
            }

            else
            {
                countDownTimer = 0;
            }

            GM.timerText.text = "Time: " + Mathf.FloorToInt(countDownTimer); // Round time displayed to a whole number

            yield return null;
        }
    }


    Vector3 RandomSpawnPos() // Create random spawn position excluding the building area
    {
        int spawnPointX = Random.Range(0, spawnPoints.Length);
        int spawnPointY = Random.Range(0, spawnPoints.Length);
        return new Vector3(spawnPoints[spawnPointX], 0.5f, spawnPoints[spawnPointY]);
    }

    IEnumerator SpawnBlock()
    {
        GM.buildText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        GM.buildText.gameObject.SetActive(false);

        int amountToSpawn = Random.Range(3, 6);

        for (int i = 0; i < amountToSpawn; i++)
        {
            Instantiate(obstacle, RandomSpawnPos(), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator SpawnEnemy()

    {
        GM.hideText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        GM.hideText.gameObject.SetActive(false);

        int maxSpawnAmount = FindObjectsOfType<ObstacleController>().Length; // Cannot spawn more enemies than there are barrels
        int amountToSpawn = Random.Range(1, maxSpawnAmount);

        for (int i = 0; i < amountToSpawn; i++)
        {
            Instantiate(enemy, RandomSpawnPos(), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator WaveSpawner()

    {
        SetTimer(timerDefaultValue);
        StartCoroutine(RunTimer());

        while (GM.gameIsActive)
        {
            // Spawn random amount of blocks | Wait till Timer reaches zero
            yield return StartCoroutine(SpawnBlock());
            yield return new WaitUntil(() => countDownTimer <= 0);

            // All blocks not toucing the buidling area are destroyed
            GM.destroyRogueObstacles = true;
            yield return new WaitForSeconds(2f);
            GM.destroyRogueObstacles = false;

            // Spawn random amount of enemies | Wait till all enemies are destroyed
            yield return StartCoroutine(SpawnEnemy());
            yield return new WaitUntil(() => FindObjectsOfType<EnemyController>().Length == 0);

            // Update wave info
            yield return new WaitForSeconds(3);
            waveCount++;
            GM.waveText.text = "Waves survived: " + waveCount;

            SetTimer(timerDefaultValue);
        }
    }

    void StartButtonClicked()
    {
        StartCoroutine(WaveSpawner());
    }


}






