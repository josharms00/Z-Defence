﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
     public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    ZombieHealth zHealth;
    bool playerInRange;
    float timer;

    void Awake ()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        zHealth = GetComponent<ZombieHealth>();
        anim = GetComponent <Animator> ();
    }
    void OnCollisionEnter (Collision other)
    {
        // If the entering collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    void OnCollisionExit (Collision other)
    {
        // If the exiting collider is the player...
        if(other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }

    void Update ()
    {      
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && playerInRange && zHealth.CurrentHealth() > 0)
        {
            // ... attack.
            Attack ();
        }

        // If the player has zero or less health...
        // if(playerHealth.currentHealth <= 0)
        // {
        //     // ... tell the animator the player is dead.
        //     anim.SetTrigger ("PlayerDead");
        // }
    }

    void Attack ()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if(playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            anim.SetTrigger("Attack");
            playerHealth.TakeDamage (attackDamage);
        }
    }
}