using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{
    public enum chipTypes {water, electric}
    public chipTypes obstacleType;

    // Start is called before the first frame update
    void Start()
    {
        if(obstacleType == chipTypes.water && RobotController.waterChip || obstacleType == chipTypes.water && RobotController.waterChip)
        {
            GetComponent<Collider2D>().enabled = false;
        } 
        else
        {
            GetComponent<Collider2D>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
