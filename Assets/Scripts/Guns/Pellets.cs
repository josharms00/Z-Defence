﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellets : MonoBehaviour
{
    public int damage = 50;

    void OnCollisionEnter(Collision other)
    {
        ZombieHealth zh = other.collider.gameObject.GetComponent<ZombieHealth> ();
        if(zh != null)
        {
            zh.TakeDamage(damage, Vector3.zero);
        }
    }
}