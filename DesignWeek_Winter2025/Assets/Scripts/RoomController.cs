using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    Transform[] childrenTransforms;
    List<Vector3> savedPositions;
    
    void Start()
    {
        childrenTransforms = GetComponentsInChildren<Transform>();
        savedPositions = new List<Vector3>();

        for (int i = 0; i < childrenTransforms.Length; i++)
        {
            savedPositions.Add(childrenTransforms[i].position);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPositions()
    {
        for (int i = 0;i < childrenTransforms.Length;i++)
        {
            if (childrenTransforms[i].gameObject.GetComponentInChildren<Robot1Controller>())
            {
                Destroy(childrenTransforms[i].gameObject.GetComponentInChildren<Robot1Controller>().gameObject);
            };
            childrenTransforms[i].position = savedPositions[i];
        }
    }
}
