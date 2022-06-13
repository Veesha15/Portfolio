using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform movePoint; // empty game object that moves ahead of player, player then moves towards move point
    [SerializeField] LayerMask road; // limits player to the road
    private float moveSpeed = 5; // speed at which the player moves
    private bool atMovePoint = true; // whether the player has reached the move point

    public bool playerCanMove = true; // player movement is disabled when popup windows are open


    void Start()
    {
        movePoint.parent = null;
    }


    void Update()
    {
        if (playerCanMove) 
        {
            if (atMovePoint) // if player is at the move point, the move point is able to be moved using the WASD keys
            {
                float xInput = Input.GetAxisRaw("Horizontal");
                float yInput = Input.GetAxisRaw("Vertical");

                movePoint.Translate(xInput, yInput, 0);

                if (Vector3.Distance(transform.position, movePoint.position) >= 1 && MovePointOnRoad()) // with == had bug (player stops moving) when moving diagonally 
                {
                    atMovePoint = false;
                }
            }

            if (!atMovePoint) // if the move point is a certain distance away from the player
            {
                transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, movePoint.position) == 0)
                {
                    atMovePoint = true;
                }
            }

            else
            {
                movePoint.position = transform.position; // reset move point
            }
        }
    }

    private bool MovePointOnRoad()
    {
        return Physics2D.OverlapCircle(movePoint.position, 0.2f, road);
    }

}
