using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{

    public Vector3Variable playerPos;

    public float speed =15;

    private void Update()
    {
        Follow();
    }

    void Follow()
    {

        Vector3 lookpoint = playerPos.vector;

        lookpoint.y = transform.position.y;
        transform.LookAt(lookpoint);

        transform.Translate(Vector3.forward*Time.deltaTime*speed);
    }


}
