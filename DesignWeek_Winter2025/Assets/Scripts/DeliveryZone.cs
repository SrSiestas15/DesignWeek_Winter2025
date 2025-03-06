using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] private GameObject desiredDelivery;
    [SerializeField] private GameObject keyPickUp;

    public Transform dropOffZone;
    [HideInInspector] public bool delivered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine("CheckDelivery", collision.gameObject);
    }

    IEnumerator CheckDelivery(GameObject deliveryGO)
    {
        if(deliveryGO.GetComponent<BoxController>())
        {
                Debug.Log("get box");
            if (deliveryGO.GetComponent<BoxController>().hasChip && !delivered)
            {
                CameraController.ResetToRoom();
                delivered = true;
                Instantiate(keyPickUp, dropOffZone.transform.position, Quaternion.identity);
                Debug.Log("get chip");
                yield return new WaitForSeconds(1);
            }
        }
    }
}
