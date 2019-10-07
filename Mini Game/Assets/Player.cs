using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public FloatVariable moveSpeed;
    public FloatVariable rotationSpeed;
    public Vector3Variable playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        playerPosition.vector = transform.position;

        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed.Value);
        transform.Rotate(Vector3.up, Time.deltaTime * Input.GetAxis("Horizontal") * rotationSpeed.Value);


    }
    
}
