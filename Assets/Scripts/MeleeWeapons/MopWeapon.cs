﻿using UnityEngine;
using System.Collections;

public class MopWeapon : MonoBehaviour
{

    public float damage;
    // how often player can press attack
    public float attackRate;
    // Delay before damage is dealt
    public float attackDelay;
    // how long the hitbox is active for.
    public float attackDuration;
    //the hitbox for the melee weapon
    public BoxCollider2D hitBox;

    // the time the last swing took place
    private float m_previousAttackTime;
    private float m_attackDelay;
    private float m_attackDuration;
    private bool m_attacking = false;
    private Vector3 m_startingPosition;

    void OnTriggerEnter2D(Collider2D col)
    {
        // Have to add all enemies here
        if (col.tag == "StationaryEnemy")
        {
            col.gameObject.GetComponent<StationaryEnemy>().TakeDamage(damage);
        }
        else if (col.tag == "MovingEnemy")
        {
            col.gameObject.GetComponent<MovingEnemy>().TakeDamage(damage);
            if (!col.gameObject.GetComponent<MovingEnemy>().GetPushedBack())
            {
                col.gameObject.GetComponent<MovingEnemy>().SetPushedBack();
            }
        }
        else if (col.tag == "InternEnemy")
        {
            col.gameObject.GetComponent<InternEnemy>().TakeDamage(damage);
            if (!col.gameObject.GetComponent<InternEnemy>().GetPushedBack())
            {
                col.gameObject.GetComponent<InternEnemy>().SetPushedBack();
            }
        }
        else if (col.tag == "RatEnemy")
        {
            //TODO: Maybe add pushback to rat
            col.gameObject.GetComponent<RatEnemy>().TakeDamage(damage);
        }
        else if (col.tag == "MailmanEnenmy")
        {
            col.gameObject.GetComponent<MailmanEnemy>().TakeDamage(damage);
            if (!col.gameObject.GetComponent<MailmanEnemy>().GetPushedBack())
            {
                col.gameObject.GetComponent<MailmanEnemy>().SetPushedBack();
            }
        }
        else if (col.tag == "MailmanMiniBoss")
        {
            col.gameObject.GetComponent<MailmanMiniBoss>().TakeDamage(damage);
        }
    }

    void Start()
    {
        m_startingPosition = hitBox.transform.position;
        m_attackDuration = attackDuration;
        m_attackDelay = attackDelay;
        m_previousAttackTime = Time.time;
        hitBox.enabled = false;

    }
    void Update()
    {

        if (m_attacking)
        {
            // if we're attacking start the timer for the attack delay
            m_attackDelay -= Time.deltaTime;
            if (m_attackDelay <= 0)
            {
                // when the delay has passed, activate the hit box
                hitBox.enabled = true;
                //moves the hitbox around so that the OnTriggerEntered2D event is called. 
                Vector3 endPos = new Vector3(1f, .1f, 0);
                this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, endPos, 3f * Time.deltaTime);


                m_attackDuration -= Time.deltaTime;
                if (m_attackDuration <= 0)
                {
                    // after the attack duration reset values for the next attack turn off hitbox.
                    this.transform.localPosition = new Vector3(.35f, .1f, 0);
                    m_attacking = false;
                    m_attackDelay = attackDelay;
                    m_attackDuration = attackDuration;
                    hitBox.enabled = false;
                }
            }
        }
    }
    // if successful return true, if not return false
    public bool Swing(float currentTime)
    {
        if (currentTime - m_previousAttackTime > attackRate + attackDelay)
        {
            m_attacking = true;
            m_previousAttackTime = Time.time;
            return true;
        }
        else { return false; }

    }
}
