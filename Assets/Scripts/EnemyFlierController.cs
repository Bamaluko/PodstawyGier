using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlierController : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
    * 
    * rangeToStartChasing - when player is within range - the enemy starts chasing him.
    * 
    * isChasing - active whenever currently chasing the player.
    * 
    * moveSpeed - speed of flyers movement.
    * 
    * turnSpeed - determines how fast the flyer will turn.
    * 
    * player - reference to players transform(position).
    * 
    * anim - animator to handle transforming to moving animation, when flyer is chasing the player.
    */

    public float rangeToStartChase;
    private bool isChasing;
    public float moveSpeed, turnSpeed;
    private Transform player;

    public Animator anim;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing)
        {
            if (Vector3.Distance(transform.position, player.position) < rangeToStartChase)
            {
                isChasing = true;

                anim.SetBool("isChasing", isChasing);
            }
        }
        else
        {
            if (player.gameObject.activeSelf)
            {
                Vector3 direction = transform.position - player.position;
                //Based on players position we rotate the enemy to face the player.
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //Quaternion handles rotation for unity.
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
                //Slerp allows us change from one rotation to another within a given time.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

                //transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                transform.position += -transform.right * moveSpeed * Time.deltaTime;

            }

            //if (Vector3.Distance(transform.position, player.position) > rangeToStartChase)
            //{
            //    isChasing = false;
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SquarePlatforms") && PlayerPrefs.HasKey("FlierDestroySquarePlatforms"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        else if (col.gameObject.CompareTag("RedBridge") && PlayerPrefs.HasKey("FlierDestroyRedBridges"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
