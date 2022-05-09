using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour // attached to barrel prefab
{
    private GameManager GM;

    private GameObject buildingArea;
    public GameObject[] explosionColours;
    public AudioClip destroySound;

    public bool isRogue = true;

    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        buildingArea = GameObject.Find("Building Area");
    }

    void Update()
    {
        DestroyRogueObstacles(); // TODO: only run when timer runs out
    }

    private void OnCollisionStay(Collision collision) //TODO: change to is trigger
    {
        if (collision.gameObject == buildingArea)
        {
            isRogue = false;
        }

        else {isRogue = true; }
    }


    void DestroyRogueObstacles()
    {
        if (isRogue == true && GM.destroyRogueObstacles == true)
        {
            DestoyObstacles();
        }
    }

    public void DestoyObstacles()
    {
        int explosionIndex = Random.Range(0, explosionColours.Length); // when the barrels explode they have a random explosion colour
        Instantiate(explosionColours[explosionIndex], transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(destroySound, transform.position);
        Destroy(gameObject);
    }

}

