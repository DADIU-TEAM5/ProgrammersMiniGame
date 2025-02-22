﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Space(5)]
    [Header("Scpritable Objects")]
    public FloatVariable moveSpeed;
    [Space(5)]
    public FloatVariable rotationSpeed;
    [Space(5)]
    public Vector3Variable playerPosition;
    public ParticleSystem particle;

    [HideInInspector]
    public float movespeed;
    [HideInInspector]
    public float rotationspeed;


    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(movespeed);
        moveSpeed.Value = movespeed;
        rotationSpeed.Value = movespeed;
    }

    // Update is called once per frame
    private void Update()
    {
        playerPosition.vector = transform.position;

        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed.Value);
        transform.Rotate(Vector3.up, Time.deltaTime * Input.GetAxis("Horizontal") * rotationSpeed.Value*4);



        anim.SetBool("Moving",(Input.GetAxisRaw("Horizontal") != 0)|| (Input.GetAxisRaw("Vertical") != 0));


        if (Input.GetButtonDown("Fire1"))
            particle.Emit(1);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        SceneManager.LoadScene("DeadScene");
    }



}
