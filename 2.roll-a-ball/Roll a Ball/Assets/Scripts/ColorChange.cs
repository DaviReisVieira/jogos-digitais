using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Material Background;

    void Update()
    {
        Background.color = Color.Lerp(Background.color, new Color(Random.value, Random.value, Random.value), Time.deltaTime * 0.5f);

    }
}
