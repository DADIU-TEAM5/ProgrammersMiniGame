using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    public FloatVariable moveSpeed;
    public FloatVariable rotationSpeed;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed.Value);
        transform.Rotate(Vector3.up, Time.deltaTime * Input.GetAxis("Horizontal") * rotationSpeed.Value);
    }
    
}
