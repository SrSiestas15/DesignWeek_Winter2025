using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform moveLocation;

    [Header("Associated with layers set in inspector")]
    public LayerMask MovementBlocker;

    public static bool roomChip;
    public static bool pushChip;
    public static bool waterChip;
    public static bool electricChip;
    //public static bool electricChip;

    RaycastHit2D hitInfo;

    void Start()
    {
        moveLocation.parent = null;   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }

        // update the transform pos
        transform.position = Vector3.MoveTowards(transform.position, moveLocation.position, moveSpeed * Time.deltaTime);

        // if the distance between the robot and the next tile is lessthan or equal to 0.05
        if (Vector3.Distance(transform.position, moveLocation.position) <= 0.05f)
        {
            // handle directional input 
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                // check if the next move location is not on something that would block the player
                if (!Physics2D.OverlapCircle(moveLocation.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), 0.2f, MovementBlocker))
                {
                    moveLocation.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                // check if the next move location is not on something that would block the player
                if (!Physics2D.OverlapCircle(moveLocation.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), 0.2f, MovementBlocker))
                {
                    moveLocation.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                }
            }
        }
    }

    private void Interact()
    {
        hitInfo = Physics2D.Linecast(transform.localPosition, transform.localPosition + (Vector3.up * 2));
        Debug.DrawLine(transform.localPosition, transform.localPosition + (Vector3.up * 2));
        if (hitInfo.transform.GetComponent<UIController>())
        {
            Debug.Log("got controller");
            UIController.TurnOnOff(true);
        }

        if (hitInfo.transform.GetComponent<PickUpController>())
        {
            Destroy(hitInfo.transform.gameObject);
            PickUpController pickUpScript = hitInfo.transform.GetComponent<PickUpController>();
            if (pickUpScript.pickUpID == 3)
            {
                CombinationReader.unlocked3 = true;
            }

            if (pickUpScript.pickUpID == 4)
            {
                CombinationReader.unlocked4 = true;
            }

            if (pickUpScript.pickUpID == 5)
            {
                CombinationReader.unlocked5 = true;
            }

            if (pickUpScript.pickUpID == 6)
            {
                CombinationReader.unlocked6 = true;
            }

            if (pickUpScript.pickUpID == 7)
            {
                CombinationReader.unlocked7 = true;
            }
        }
    }
}
