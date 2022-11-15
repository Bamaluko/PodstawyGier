using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Damage;
    public GameObject bullet;
    public float FireRate;                  // bulets shoot on 1 sec
    public float nextTimeToFire = 0;
    public Transform shootPoint;
    public float force;
    Vector2 Direction = new Vector2(-1, 0);
    // Update is called once per frame


    void Update()
    {
        if(Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / FireRate;
            shoot();
        }
    }

    void shoot()
    {
        GameObject BulletIns = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        BulletIns.GetComponent<BulletScript>().damageValue = Damage;
        if(BulletIns != null)
        {
            BulletIns.GetComponent<Rigidbody2D>().AddForce(force * Direction);
        }   
    }
}
