﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backScene : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            SceneManager.LoadScene("GameScen");
    }
}
