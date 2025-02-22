﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onParticleCollision : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);


        int i = 0;

        while (i < numCollisionEvents)
        {
            if(other.CompareTag("Enemy"))
                Destroy(other);
            i++;
        }
    }
}
