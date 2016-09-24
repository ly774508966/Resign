﻿using UnityEngine;
using System.Collections;

public class RangedWeapon : MonoBehaviour
{
    // The projectile it fires
    public GameObject projectilePrefab;

    // how often if fires while holding
    public float attackRate;
    // speed of the projectile
    public float projectileSpeed;

    private float m_previousShotTime;

    void Start()
    {
        m_previousShotTime = Time.time;
    }

    // Gets the time the method was called, and the direction the character is facing
    public void Shoot(float currentTime, string direction)
    {
        if (currentTime - m_previousShotTime > attackRate)
        {
            GameObject projectile = (GameObject)Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
            if (direction == "Right")
            {
                projectile.GetComponent<Rigidbody2D>().velocity += new Vector2(projectileSpeed, 0f);
            }
            else
            {
                projectile.GetComponent<Rigidbody2D>().velocity += new Vector2(-projectileSpeed, 0f);
            }
            m_previousShotTime = Time.time;
        }
    }
}
