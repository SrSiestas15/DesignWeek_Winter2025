using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Camera cameraGO;
    private List<Vector3> roomPos = new List<Vector3>();
    [SerializeField] AnimationCurve lerpCurve;
    private float lerpTimer;
    [SerializeField] private float transitionDuration;

    public static bool moving;
    public static int roomNum;
    

    // Start is called before the first frame update
    void Start()
    {
        cameraGO = GetComponent<Camera>();
        
        roomPos.Add(new Vector3 (0, 0, 0)); //PLACEHOLDER FOR INDEX[0]
        roomPos.Add(new Vector3 (-16, 0, 0)); //room 1
        roomPos.Add(new Vector3 (0, 0, 0)); //room 2 MAIN ENTRANCE
        roomPos.Add(new Vector3 (16, 0, 0)); //room 3
        roomPos.Add(new Vector3 (-16, -15, 0)); //room 4
        roomPos.Add(new Vector3 (0, -15, 0)); //room 5
        roomPos.Add(new Vector3 (16, -15, 0)); //room 6
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            lerpTimer += Time.deltaTime;
            float t = lerpTimer / transitionDuration;
            float curveValue = lerpCurve.Evaluate(t);

            cameraGO.transform.position = Vector3.Lerp(Vector3.zero, roomPos[roomNum], curveValue);
            Debug.Log(roomPos[roomNum]);
            if (lerpTimer > transitionDuration)
            {
                moving = false;
                lerpTimer = 0;
            }

        }
    }

    static public void ResetToRoom()
    {
        cameraGO.transform.position = Vector3.zero;
        UIController.turnOnUIButtonStatic.SetActive(true);
        UIController.selfDestructButtonStatic.SetActive(false);
    }
}
