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

    public static bool unlocked2;
    public static bool unlocked3;
    public static bool unlocked4;
    public static bool unlocked5;
    public static bool unlocked6;
    public static bool unlocked7;
    public static bool unlocked8;

    public List<RectTransform> UISlots;
    public static List<RectTransform> UISlotsStatic;
    public List<RectTransform> UIChips;
    public static List<RectTransform> UIChipsStatic;

    public static List<GameObject> tempEmpties = new List<GameObject>();

    public static CombinationReader publicReaderStatic;

    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<TokenSlot>();
        chipsTempStatic = chipsTemp;
        slotsTempStatic = slotsTemp;

        UISlotsStatic = UISlots;
        UIChipsStatic = UIChips;

        publicReaderStatic = GetComponent<CombinationReader>();
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
            //Debug.Log($"chip {chipsTempStatic[(int)slotValues[i].y].GetComponent<SimulatorPiece>().type} at slot {slotsTempStatic[(int)slotValues[i].x].name}");
        }
        publicReaderStatic.spawnRobot();
    }

    void CheckChips()
    {
        for (int i = 0; i < slotValues.Count; i++)
        {
            currentCode += slotValues[i].x;
            currentCode += slotValues[i].y;
        }
    }

    void spawnRobot()
    {
        bool success = true;
        float numOfImportantChips = 0;
        for (int i = 0; i < slotValues.Count; i++)
        {
            if(slotValues[i].y > 0 && UIChipsStatic[(int)slotValues[i].y].GetComponent<SimulatorPiece>().isAllowed == false)
            {
                success = false;
                Debug.Log("failure");
                return;
            }
            else
            {
                //Debug.Log(roomResetters[roomChosen]);
                if (roomResetters[5] != null)
                {
                    roomResetters[roomChosen].ResetPositions();
                }

                CameraController.roomNum = roomChosen;
                CameraController.moving = true;

                UIController.TurnOnOff(false);
                UIController.turnOnUIButtonStatic.SetActive(false);
                UIController.selfDestructButtonStatic.SetActive(true);

                Debug.Log(i);
                Instantiate(possibleRobots[0], possibleSpawners[roomChosen]);
            }
        }
        Debug.Log(numOfImportantChips);
    }

    public static void Reset()
    {
        UIChipsStatic[1].SetParent(UISlotsStatic[7]);
        Debug.Log($"move {UIChipsStatic[1].name} to {UISlotsStatic[7].name}");


        if (unlocked2)
        {
            UIChipsStatic[2].SetParent(UISlotsStatic[8]);
        }
        if (unlocked3)
        {
            UIChipsStatic[3].transform.SetParent(UISlotsStatic[1].transform);
        }
        if (unlocked4)
        {
            UIChipsStatic[4].transform.SetParent(UISlotsStatic[2].transform);

        }
        if (unlocked5)
        {
            UIChipsStatic[5].transform.SetParent(UISlotsStatic[3].transform);

        }
        if (unlocked6)
        {
            UIChipsStatic[6].transform.position = UISlotsStatic[4].transform.position;

        }
        if (unlocked7)
        {
            UIChipsStatic[7].transform.position = UISlotsStatic[5].transform.position;
        }
        if (unlocked8)
        {
            UIChipsStatic[8].transform.position = UISlotsStatic[8].transform.position;
        }
    }
}
