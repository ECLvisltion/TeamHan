using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleFog : MonoBehaviour
{
    public bool fogEnable;
    public float red, green, blue;
    private void Start()
    {
        fogEnable = true;
        red = 1.0f;
        green = 1.0f;
        blue = 1.0f;
    }
    private void Update()
    {
        RenderSettings.fog = fogEnable;
        RenderSettings.fogColor = new Color(red, green, blue);
    }
}
