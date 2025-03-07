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
    public List<GameObject> possibleRobots;
    public List<Transform> possibleSpawners;
    public List<RoomController> roomResetters;
    public static string currentCode;

    public GameObject[] slotsTemp;
    public static GameObject[] slotsTempStatic;
    public GameObject[] chipsTemp;
    public static GameObject[] chipsTempStatic;

    public static List<GameObject> tempEmpties = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<TokenSlot>();
        chipsTempStatic = chipsTemp;
        slotsTempStatic = slotsTemp;

    }

    private void Update()
    {
    }

    public static void CheckChildren()
    {
        slotValues = new List<Vector2>();
        foreach (TokenSlot slot in slots)
        {
            if (slot.GetComponentInChildren<DraggableItem>() != null && slot.slotID > 0)
            {
                DraggableItem draggableItem = slot.gameObject.GetComponentInChildren<DraggableItem>();
                slotValues.Add(new Vector2(slot.slotID, draggableItem.draggableID));
            } else slotValues.Add(new Vector2(slot.slotID, 0));

            
        }
        CheckRoom();
        OrganizeTokens();
        //GetCode();
    }

    public static void CheckRoom()
    {
        roomChosen = 0;
        for (int i = 0; i < slotValues.Count; i++)
        {
            if (slotValues[i].y == 1)
            {
                roomChosen = Mathf.RoundToInt(slotValues[i].x);
                //slotValues.Remove(slotValues[i]);
            }
        }
    }

    public static void GetCode()
    {
        currentCode = string.Empty;

        for (int i = 0; i < slotValues.Count; i++)
        {
            currentCode += slotValues[i].x;
            currentCode += slotValues[i].y;
        }
        Debug.Log(currentCode);

    }

    public void RoomAndRobot()
    {
        for (int i = 0; i < possibleCombinations.Count; i++)
        {
            if (possibleCombinations[i] == currentCode && roomChosen != 0)
            {

                if (roomResetters[roomChosen] != null)
                {
                    roomResetters[roomChosen].ResetPositions();
                }

                CameraController.roomNum = roomChosen;
                CameraController.moving = true;

                UIController.TurnOnOff(false);
                UIController.turnOnUIButtonStatic.SetActive(false);
                UIController.selfDestructButtonStatic.SetActive(true);

                Instantiate(possibleRobots[i], possibleSpawners[roomChosen]);
                Debug.Log(i);
            }
        }
    }

    public static void OrganizeTokens()
    {
        for (int i = 0; i < tempEmpties.Count; i++)
        {
            Destroy(tempEmpties[i]);
        }
        for (int i = 0; i < slotValues.Count; i++)
        {
            if (slotValues[i].x == 0)
            {
                chipsTempStatic[(int)slotValues[i].y].gameObject.transform.position = slotsTempStatic[(int)slotValues[i].x + 6].gameObject.transform.position;
            }
            else if (slotValues[i].y == 0)
            {
                GameObject tempInstatiate = Instantiate(chipsTempStatic[0], slotsTempStatic[(int)slotValues[i].x].gameObject.transform.position, Quaternion.identity);
                tempEmpties.Add(tempInstatiate);
            }
            else
            {
                chipsTempStatic[(int)slotValues[i].y].gameObject.transform.position = slotsTempStatic[(int)slotValues[i].x].gameObject.transform.position;

            }
            Debug.Log($"chip {chipsTempStatic[(int)slotValues[i].y].GetComponent<SimulatorPiece>().type} at slot {slotsTempStatic[(int)slotValues[i].x].name}");
        }

    }

    void CheckChips()
    {
        for (int i = 0; i < slotValues.Count; i++)
        {
            currentCode += slotValues[i].x;
            currentCode += slotValues[i].y;
        }
    }
}
