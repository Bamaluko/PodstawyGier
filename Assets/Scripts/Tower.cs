using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Damage;
    public GameObject bullet;
    public float FireRate;                  // bulets shoot on 1 sec
    public Transform shootPoint;
    public bool shotLeft = true;
    public float bulletSpeed;

    //[HideInInspector]
    public float nextTimeToFire = 0;
    // Update is called once per frame

    private void Start()
    {

    }

    void Update()
    {
        if (Time.time > nextTimeToFire) 
        {
            nextTimeToFire = Time.time + 1 / FireRate;
            shoot();
        }
    }

    void shoot()
    {
        GameObject BulletIns = Instantiate(bullet, shootPoint.position, gameObject.transform.rotation);
        BulletIns.GetComponent<BulletScript>().damageValue = Damage;
        BulletIns.GetComponent<BulletScript>().moveSpeed = bulletSpeed * -transform.localScale.x;
        BulletIns.gameObject.transform.localScale = transform.localScale;
    }
}
