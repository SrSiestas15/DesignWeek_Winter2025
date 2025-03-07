using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static GameObject terminalUIStatic;
    public GameObject turnOnUIButton;
    public static GameObject turnOnUIButtonStatic;
    public GameObject selfDestructButton;
    public static GameObject selfDestructButtonStatic;

    public GameObject terminalUI;

    // Start is called before the first frame update
    void Start()
    {
        turnOnUIButtonStatic = turnOnUIButton;
        selfDestructButton.SetActive(false);
        selfDestructButtonStatic = selfDestructButton;

        //Transform[] childrenTransforms = GetComponentInParent<Canvas>().gameObject.GetComponentsInChildren<Transform>();
        //foreach (Transform t in childrenTransforms)
        //{
        //    if (t.gameObject.name == "Terminal UI")
        //    {
        //        terminalUI = t.gameObject;
        //        terminalUI.SetActive(false);
        //    }
        //}

        terminalUIStatic = terminalUI;
        terminalUI.SetActive(false);
    }

    public static void TurnOnOff(bool on)
    {
        terminalUIStatic.SetActive(on);
        turnOnUIButtonStatic.SetActive(!on);

        CombinationReader.Reset();
    }
}
