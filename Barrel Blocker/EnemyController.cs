using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager GM;
    private GameObject player;

    private AudioSource enemyAudio;
    public AudioClip walkSound;
    public AudioClip destroySound;

    public GameObject destroyParticle;

    private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        enemyAudio = GetComponent<AudioSource>();

        InvokeRepeating("PlayWalkSound", 0.25f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

        if (!GM.gameIsActive) // Destroys any leftover enemies when player is dead
        {
            DestroyEnemy();
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            DestroyEnemy();
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject == player)
        {
            DestroyEnemy();
        }
    }

    void FollowPlayer()
    {
        Vector3 playerPos = (player.transform.position - transform.position);
        transform.rotation = Quaternion.LookRotation(playerPos);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);   
    }

    void DestroyEnemy()
    {
        AudioSource.PlayClipAtPoint(destroySound, transform.position);
        Instantiate(destroyParticle, transform.position, destroyParticle.transform.rotation);
        Destroy(gameObject);
    }

    void PlayWalkSound()
    {
        enemyAudio.PlayOneShot(walkSound);
    }


}
