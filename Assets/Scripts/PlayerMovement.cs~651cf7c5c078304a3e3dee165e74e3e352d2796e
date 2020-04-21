//using directives
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
        
    }
    
    float GetSpeed()
    {
        GameObject[] enemies =  GameObject.FindGameObjectsWithTag("Enemy");
        float leastDistance = 999999999999;
        foreach(GameObject enemy in enemies)
        {
            Vector3 otherpos = enemy.transform.position;
            Vector3 mypos = this.m_Rigidbody.position;
            float dist = Vector3.Distance(otherpos, mypos);
            
            if(dist < leastDistance)
            {
                leastDistance = dist;
            }
        }
        
        float spd = GetSpeed(leastDistance);
        
        //print("speed " + spd + " least dist " + leastDistance);
        return spd;
        
    }
    
    float GetSpeed(float distFromEnemy)
    {
        float max = 1.4f;//fastest possible speed
        float min = .4f;//slowest possible speed
        
        float maxDistance = 8;//distance (from closest enemy) at which u move the fastest speed)
        float minDistance = 1;//distance (from closest enemy) at which u move the slowest speed
        
        
        if (distFromEnemy > maxDistance) distFromEnemy = maxDistance;
        if (distFromEnemy < minDistance) distFromEnemy = minDistance;
        
        float zeroToOne = (distFromEnemy-minDistance)/(maxDistance-minDistance);
        
        return zeroToOne*(max-min)+min;
        
    }
    
    
    
    
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float spd = GetSpeed();
        
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");

        m_Movement.Set (horizontal, 0f, vertical); // initializes vector
        m_Movement.Normalize (); // changes vector magnitude to 1 by normalizing
        
        m_Movement *= spd;
        
        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f); 
        bool hasVerticalInput = !Mathf.Approximately (vertical,0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput ;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play ();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
}
