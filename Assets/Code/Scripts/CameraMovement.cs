using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float height = 2;
    public float depth = -10;
    float yPos;
    float xPos;
    private void LateUpdate() {
        xPos = target.position.x;
        yPos = target.position.y;

        if (xPos < -6) xPos = -6;
        if (xPos > 6) xPos = 6;
        if (yPos < 0) yPos = 0;
        if (yPos > 32) yPos = 32;

        transform.position = new Vector3(xPos, yPos, depth);
    }
}
