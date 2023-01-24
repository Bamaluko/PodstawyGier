using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint;

    public float moveSpeed, waitAtPoint;
    private float waitCounter;

    public float jumpForce;

    public Rigidbody2D theRB;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;

        foreach(Transform pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
        if (PlayerPrefs.HasKey("walker_damage_increase"))
        {
            GetComponentInParent<DamagePlayer>().damageAmount += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f)
        {
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (transform.position.y < patrolPoints[currentPoint].position.y -.5f && theRB.velocity.y < .1f)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
        }
        else
        {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoint;
                currentPoint ++;

                if (currentPoint >= patrolPoints.Length)
                {
                    currentPoint = 0;
                }
            }
        }

        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
    }
}

