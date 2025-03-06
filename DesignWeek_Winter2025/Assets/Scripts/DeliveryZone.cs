using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] private GameObject desiredDelivery;
    [SerializeField] private GameObject keyPickUp;

    public Transform dropOffZone;

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
        yield return new WaitForSeconds(1);
        if(deliveryGO.tag == desiredDelivery.tag)
        {
            CameraController.ResetToRoom();
            Instantiate(keyPickUp, dropOffZone.transform.position, Quaternion.identity);
        }
    }
}
