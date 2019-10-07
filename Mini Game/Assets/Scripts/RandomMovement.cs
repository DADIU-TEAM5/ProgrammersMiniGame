using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public int waitSeconds;
    private float randomNumberSpeed;
    private float randomNumberRotation;

    float currentTime;

    private void Start()
    {
        StartCoroutine(RandomSwitch(waitSeconds));
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>= waitSeconds)
        {
            randomNumberSpeed = Random.Range(1f, 5f);
            randomNumberRotation = Random.Range(-50.0f, 50.0f);
        }


        transform.Translate(Vector3.forward * Time.deltaTime * randomNumberSpeed * moveSpeed);
        transform.Rotate(Vector3.up, Time.deltaTime * randomNumberRotation * rotationSpeed);
    }

    private IEnumerator RandomSwitch(int seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
           
        }
            
    }
}
