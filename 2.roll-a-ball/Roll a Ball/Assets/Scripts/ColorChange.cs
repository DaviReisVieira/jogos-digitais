using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Material Background;

    void Update()
    {
        // change background color between primary colors with time of 0.8 seconds
        Background.color = Color.Lerp(Color.red, Color.green, Mathf.PingPong(Time.time, 0.8f));
        

    }
}
