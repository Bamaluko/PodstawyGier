using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damageValue;
    public GameObject destroyEffect;
    public float moveSpeed;
    private void OnBecameInvisible()
    {
        //Destroy object.
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerHealthController player = collision.gameObject.GetComponent<PlayerHealthController>();
                player.DamagePlayer(damageValue);
            }
            if(destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
            }
            Destroy(this.gameObject);

            //AudioManager.instance.PlaySFX(3);
        }
    }
}
