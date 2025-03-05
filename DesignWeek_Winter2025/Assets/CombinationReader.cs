using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CombinationReader : MonoBehaviour
{

    static TokenSlot[] slots;
    static List<Vector2> slotValues;
    public static int roomChosen;
    public List<string> possibleCombinations;

    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<TokenSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void CheckChildren()
    {
        slotValues = new List<Vector2>();
        foreach(TokenSlot slot in slots)
        {
            if(slot.GetComponentInChildren<DraggableItem>() != null)
            {
                DraggableItem draggableItem = slot.gameObject.GetComponentInChildren<DraggableItem>();
                slotValues.Add(new Vector2(slot.slotID, draggableItem.draggableID));
            }
        }

        checkRoom();
    }

    public static void checkRoom()
    {
        for (int i = 0; i < slotValues.Count; i++)
        {
            if (slotValues[i].y == 1)
            {
                roomChosen = Mathf.RoundToInt(slotValues[i].x);
            }
        }

        Debug.Log("Chosen Room: "+roomChosen);
    }

}
