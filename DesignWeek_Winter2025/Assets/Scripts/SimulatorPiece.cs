using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorPiece : MonoBehaviour
{
    public enum chipTypes { room, push, water, electric}
    public chipTypes type;
    public bool nextTo;
    public SimulatorPiece gameObjectInRule;

    RaycastHit2D hitInfo;
    public static Vector3 hitDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void checkAround(GameObject currentGameObject)
    {
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

            RaycastHit2D hitInfo = Physics2D.Linecast(currentGameObject.transform.position + hitDirection, currentGameObject.transform.position + (hitDirection * .25f));
            if(hitInfo.collider != null && (hitInfo.collider.GetComponent<SimulatorPiece>().type == currentGameObject.GetComponent<SimulatorPiece>().gameObjectInRule.type) == currentGameObject.GetComponent<SimulatorPiece>().nextTo)
            {

            }
        }

    }

    public void IsAllowed()
    {
        if(type == chipTypes.water)
        {
            RobotController.waterChip = true;
        }

    }
}
