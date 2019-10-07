using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BehaviourScript : ScriptableObject
{
    public enum Behaviour { Follow, Wander };
    public Behaviour behaviour;
    public Vector3Variable playerPos;

    public float speed = 1;


    float currentTime;

    public float rotationSpeed;
    public int waitSeconds;
    private float randomNumberSpeed;
    private float randomNumberRotation;


    private bool startCoRoutine;

    public void DoBehaviour(Transform trans)
    {
        if (behaviour == Behaviour.Follow)
            Follow(trans);
        if (behaviour == Behaviour.Wander) 
            Wander(trans);
    }

    void Follow(Transform trans)
    {
        Vector3 lookpoint = playerPos.vector;

        lookpoint.y = trans.position.y;
        trans.LookAt(lookpoint);

        trans.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void Wander(Transform trans)
    {
        currentTime += Time.deltaTime;
        if (currentTime >= waitSeconds)
        {
            randomNumberSpeed = Random.Range(1f, 5f);
            randomNumberRotation = Random.Range(-50.0f, 50.0f);
        }

        trans.Translate(Vector3.forward * Time.deltaTime * randomNumberSpeed * speed);
        trans.Rotate(Vector3.up, Time.deltaTime * randomNumberRotation * rotationSpeed);
    }

    

}
