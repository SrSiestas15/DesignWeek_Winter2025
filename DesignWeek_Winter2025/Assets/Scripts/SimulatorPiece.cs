using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimulatorPiece : MonoBehaviour
{
    public enum chipTypes { room, push, water, electric, empty}
    public chipTypes type;
    public bool nextTo;
    public SimulatorPiece gameObjectInRule;

    RaycastHit2D hitInfo;
    public static Vector3 hitDirection;

    public bool isAllowed;

    // Start is called before the first frame update
    void Start()
    {
        checkAround();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkAround()
    {
        if(type != chipTypes.empty && type != chipTypes.room)
        {
            if (!nextTo)
            {
                IsAllowed(true);
            }
            for(int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    hitDirection = Vector3.up;
                }
                else if (i == 1)
                {
                    hitDirection = Vector3.right;
                }
                else if (i == 2)
                {
                    hitDirection = Vector3.down;
                }
                else if (i == 3)
                {
                    hitDirection = Vector3.left;
                }

                RaycastHit2D hitInfo = Physics2D.Linecast(transform.position + hitDirection, transform.position + (hitDirection * .25f));
                if(hitInfo.collider != null && (hitInfo.collider.GetComponent<SimulatorPiece>().type == GetComponent<SimulatorPiece>().gameObjectInRule.type))
                {
                    if (GetComponent<SimulatorPiece>().nextTo == true)
                    {
                        GetComponent<SimulatorPiece>().IsAllowed(true);
                    }
                    else if (GetComponent<SimulatorPiece>().nextTo == false)
                    {
                        GetComponent<SimulatorPiece>().IsAllowed(false);
                    }
                }
            }
        }
    }

    public void IsAllowed(bool whetherTrue)
    {
        if(type == chipTypes.room)
        {
            RobotController.roomChip = whetherTrue;
        }

        if (type == chipTypes.push)
        {
            RobotController.pushChip = whetherTrue;
        }

        if (type == chipTypes.water)
        {
            RobotController.waterChip = whetherTrue;
        }

        if (type == chipTypes.electric)
        {
            RobotController.electricChip = whetherTrue;
        }

    }
}
