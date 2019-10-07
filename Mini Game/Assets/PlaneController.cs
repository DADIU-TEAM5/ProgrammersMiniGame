using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public Vector2Variable planeScale;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 size = new Vector3(planeScale.Value.x* 0.1F,100 , planeScale.Value.y * 0.1F);
        transform.localScale = size;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
