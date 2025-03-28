using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform moveLocation;

    [Header("Associated with layers set in inspector")]
    public LayerMask MovementBlocker;

    void Start()
    {
        moveLocation.parent = null;
    }

    void Update()
    {
        // update the transform pos
        transform.position = Vector3.MoveTowards(transform.position, moveLocation.position, moveSpeed * Time.deltaTime);

        // if the distance between the person and the next tile is lessthan or equal to 0.05
        if (Vector3.Distance(transform.position, moveLocation.position) <= 0.05f)
        {
            // handle directional input 
            if (Mathf.Abs(Input.GetAxisRaw("HumanHorizontal")) == 1f)
            {
                // check if the next move location is not on something that would block the player
                if (!Physics2D.OverlapCircle(moveLocation.position + new Vector3(Input.GetAxisRaw("HumanHorizontal"), 0, 0), 0.2f, MovementBlocker))
                {
                    moveLocation.position += new Vector3(Input.GetAxisRaw("HumanHorizontal"), 0, 0);
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("HumanVertical")) == 1f)
            {
                // check if the next move location is not on something that would block the player
                if (!Physics2D.OverlapCircle(moveLocation.position + new Vector3(0, Input.GetAxisRaw("HumanVertical"), 0), 0.2f, MovementBlocker))
                {
                    moveLocation.position += new Vector3(0, Input.GetAxisRaw("HumanVertical"), 0);
                }
            }
        }
    }
}
