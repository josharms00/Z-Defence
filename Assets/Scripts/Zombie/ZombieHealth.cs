﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int health = 50;

    bool deathSequence = false;

    Animator anim;

    int deathType;

    CapsuleCollider capsuleCollider;

    float sinkSpeed = 2.5f;

    void Awake()
    {
        anim = GetComponent <Animator> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
    }

    public void TakeDamage(int damageTaken, Vector3 hitPoint)
    {

        health -= damageTaken;

        // if dead and has not done this sequence before
        if(IsDead() && !deathSequence)
        {
            // choose a random death animation
            deathType = Random.Range(0, 1);

            if(deathType == 1)
            {
                anim.SetTrigger("Dead1");
            }
            else
            {
                anim.SetTrigger("Dead2");
            }

            deathSequence = true;

            // one less zombie in round
            RoundManager.zombiesInRound--;
            PointsManager.points += 100 * ComboManager.comboMultiplier;
            ComboManager.zombieKilled = true;

            // need to set as kinematic so zombie can sink through floor
            SetKinematics(true);
        }
    
    }

    private void SetKinematics(bool isKinematic)
    {
        capsuleCollider.isTrigger = isKinematic;
        capsuleCollider.attachedRigidbody.isKinematic = isKinematic;
    }

    public int CurrentHealth()
    {
        return health;
    }

    public bool IsDead()
    {
        
        return (health <= 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDead())
        {
            // when dead the zombie will gradually sink lower beneath the map until it's delelted
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
            if (transform.position.y < -5f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
