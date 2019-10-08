using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTest : MonoBehaviour
{
    [HideInInspector]
    public Color ObjectColor;

    private void Update()
    {
        GetComponent<Renderer>().material.color = ObjectColor;
    }
}
