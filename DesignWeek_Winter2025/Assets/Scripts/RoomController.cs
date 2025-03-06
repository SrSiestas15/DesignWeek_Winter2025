using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    Transform[] childrenTransforms;
    List<Vector3> savedPositions;

    [SerializeField] private GameObject deliveryZone;
    [SerializeField] private GameObject chipBox;
    
    void Start()
    {
        childrenTransforms = GetComponentsInChildren<Transform>();
        savedPositions = new List<Vector3>();

        for (int i = 0; i < childrenTransforms.Length; i++)
        {
            savedPositions.Add(childrenTransforms[i].position);

            if (childrenTransforms[i].gameObject.GetComponent<DeliveryZone>())
            {
                deliveryZone = childrenTransforms[i].gameObject;
            }

            if (childrenTransforms[i].gameObject.GetComponent<BoxController>() != null && childrenTransforms[i].gameObject.GetComponent<BoxController>().hasChip)
            {
                chipBox = childrenTransforms[i].gameObject;
            }
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
            if(deliveryZone.GetComponent<DeliveryZone>().delivered == true)
            {
                Destroy(chipBox);
            }
            if (childrenTransforms[i].gameObject.GetComponentInChildren<Robot1Controller>())
            {
                Destroy(childrenTransforms[i].gameObject.GetComponentInChildren<Robot1Controller>().gameObject);
            };
            childrenTransforms[i].position = savedPositions[i];
        }
    }
}
