using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
     * 
     * moveSpeed - speed of bullet's movement.
     * 
     * theRB - bullet's rigid body.
     * 
     * damageAmount - how much damage should the bullet deal.
     * 
     * impact effect - special effect to be played when hitting something.
    */

    public float moveSpeed;

    public Rigidbody2D theRB;

    public int damageAmount;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        //Setting bullet direction
        Vector3 direction = transform.position - PlayerHealthController.instance.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        AudioManager.instance.PlaySFX(2);
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = -transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(damageAmount); 
        }
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
        AudioManager.instance.PlaySFXAdjusted(3);
    }
}