
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler ,IEndDragHandler
{
    private UnityEngine.UI.Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public int draggableID;

    public void OnBeginDrag(PointerEventData eventData)
    {
        image = GetComponent<UnityEngine.UI.Image>();

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

}
