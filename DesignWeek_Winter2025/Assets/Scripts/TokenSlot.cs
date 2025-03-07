using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TokenSlot : MonoBehaviour, IDropHandler
{
    public int slotID;

    public void OnDrop(PointerEventData eventData)
    {
        if(GetComponentInChildren<DraggableItem>() == null)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }
}
