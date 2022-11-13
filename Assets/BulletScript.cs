using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damageValue;

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
        //print(this.gameObject.transform.parent.name);
        if (collision.gameObject)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerHealthController player = collision.gameObject.GetComponent<PlayerHealthController>();
                player.DamagePlayer(damageValue);
            }
            Destroy(this.gameObject);
        }
        
    }

}
