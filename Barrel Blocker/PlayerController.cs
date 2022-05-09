using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    private GameManager GM;
    private Animator animator;
    private Transform carryPos;

    private float moveSpeed = 8;
    private float turnSpeed = 90;

    private bool hasObstacle = false;
    public LayerMask obstacleMask;
    private Color indicatorColour = new Color(0.8f, 0f, 1.0f);
    private Color defaultColour = Color.white;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        carryPos = GameObject.Find("Carry Pos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        AnimateHasObstacle();
        ObstacleInteraction();
    }

    void PlayerMovement()
    {
        if (GM.gameIsActive)
        {
            float moveWS = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * moveWS * moveSpeed * Time.deltaTime);

            float turnAD = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * turnAD * turnSpeed * Time.deltaTime);

            if (moveWS != 0)
            {
                animator.SetFloat("Speed_f", 1.0f);
            }

            else if (turnAD != 0)
            {
                animator.SetFloat("Speed_f", 0.3f);
            }

            else
            {
                animator.SetFloat("Speed_f", 0.0f);
            }
        }
    }

    void AnimateHasObstacle()
    {
        if (carryPos.childCount > 0)
        {
            hasObstacle = true;
            animator.SetInteger("WeaponType_int", 4);
        }

        else
        {
            hasObstacle = false;
            animator.SetInteger("WeaponType_int", 0);
        }
    }

    void SetChildColour(GameObject _obstacle, Color _colour)
    {
        _obstacle.transform.GetChild(0).GetComponent<Renderer>().material.color = _colour;
    }

    void SetChildActive(GameObject _obstacle, bool _bool)
    {
        _obstacle.transform.GetChild(0).gameObject.SetActive(_bool);
    }

    void SetChildParent(GameObject _obstacle, Transform _parent)
    {
        _obstacle.transform.parent = _parent;
    }

    void ObstacleInteraction() 
    {
        Vector3 rayOriginPos = new Vector3(transform.position.x, 0.5f, transform.position.z); // Default transform was located too high
        Ray rayFromPlayer = new Ray(rayOriginPos, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayFromPlayer, out hitInfo, 2f, obstacleMask))
        {
            GameObject obstacleHitByRay = hitInfo.collider.gameObject;

            if (hitInfo.distance <= 1)
            {
                SetChildActive(obstacleHitByRay, true); // Indicator (child) appears when Player is CLOSE enough to Obstacle (parent)

                if (Input.GetKeyDown(KeyCode.Space) && hasObstacle == false) // Indicator changes colour and obstacle is picked up (made a child of empty GO)
                {
                    SetChildParent(obstacleHitByRay, carryPos);
                    SetChildColour(obstacleHitByRay, indicatorColour);

                }

                else if (Input.GetKeyDown(KeyCode.Space) && hasObstacle == true) // Indicator colour changes back to default and obstacle is put done (no longer a child)
                {
                    SetChildParent(obstacleHitByRay, null);
                    SetChildColour(obstacleHitByRay, defaultColour);
                }
            }

            else if (hitInfo.distance > 1) // Indicator (child) disappears when Player is too FAR from Obstacle (parent)
            {
                SetChildActive(obstacleHitByRay, false);
            }
        }

    }

    void OnCollisionEnter(Collision collision) // Collision with Enenmy = Game Over + Death Animation
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("Death_b", true);
            GM.GameOver();
        }
    }





}
