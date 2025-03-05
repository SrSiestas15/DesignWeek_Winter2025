using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    private bool isMoving;
    private Vector3 originalPos, newPos;
    public float moveTime = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !isMoving) StartCoroutine(Move(Vector3.up));

        if (Input.GetKey(KeyCode.A) && !isMoving) StartCoroutine(Move(Vector3.left));

        if (Input.GetKey(KeyCode.S) && !isMoving) StartCoroutine(Move(Vector3.down));

        if (Input.GetKey(KeyCode.D) && !isMoving) StartCoroutine(Move(Vector3.right));
    }

    // movement coroutine instance
    public IEnumerator Move(Vector3 direction)
    {
        // make it so you cant move while already moving
        isMoving = true;
        float elapsedTime = 0;

        // store positions
        originalPos = transform.position;
        newPos = originalPos + direction;

        // move coroutine
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(originalPos, newPos, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // set new position
        transform.position = newPos;

        isMoving = false;
    }
}
