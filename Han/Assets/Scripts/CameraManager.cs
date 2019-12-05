using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject player;
    private GameObject mainCamera;
    public float xMin, xMax, yMin, yMax;
    public bool lockPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        xMin = -9.0f;
        xMax = 9.0f;
        yMin = -3.5f;
        yMax = -1.5f;

        lockPosition = true;
    }
    private void FixedUpdate()
    {

    }

    // getter
    public float GetXMin() { return xMin; }
    public float GetXMax() { return xMax; }
    public float GetYMin() { return yMin; }
    public float GetYMax() { return yMax; }
}
