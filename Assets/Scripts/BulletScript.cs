using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damageValue;
    public GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
        
    }

}
