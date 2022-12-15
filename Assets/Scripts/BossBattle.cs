using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
    *
    * theCam - reference to the camera controlling script.
    * 
    * camPosition - information about the position of the camera (it's transform).
    * 
    * camSpeed - speed of cameras transiotion to it's new position.
    * 
    * treshold1, threshold2 - how much health must the boss lose for another phase to activate.
    * 
    * activeTime - how long will the boss stay in one point.
    * 
    * fadeoutTime - time for fading out of said place.
    * 
    * inactiveTime - short break before appearing somwhere else.
    * 
    * active/fade/inactiveCounter - values used for measuring how much time for corresponding action to end.
    * 
    * spawnPoints - all points in which our boss can appear.
    * 
    * targetPoint - the next target to move to.
    * 
    * moveSpeed - when our boss stops teleporting and starts moving - this is how fast he will move.
    * 
    * anim - reference to the animator of the boss.
    * 
    * theBoss - reference to the bosses transform.
    * 
    * timeBetweenShots1, timeBetweenShots2 - time between shoting at the pkayer in different phases.
    * 
    * shotCounter - current time passed since the last shot.
    * 
    * bullet - game object, the bullet that the bos shots.
    * 
    * shotPoint - where the bullet spawns.
    * 
    * winObjects - appear after the boss is defeated.
    * 
    * battleEnded - set to true, when the boss is defeated.
    * 
    * bossRef - uniqe identifier of this boss battle.
    */

    private CameraController theCam;
    public Transform camPosition;
    public float camSpeed;

    public int threshold1, threshold2;

    public float activeTime, fadeoutTime, inactiveTime;
    private float activeCounter, fadeCounter, inactiveCounter;

    public Transform[] spawnPoints;
    public Transform targetPoint;
    public float moveSpeed;

    public Animator anim;

    public Transform theBoss;

    public float timeBetweenShots1, timeBetweenShots2;
    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;

    public GameObject winObjects;

    private bool battleEnded;

    public string bossRef;
    private CameraShaker shaker;

    // Start is called before the first frame update
    void Start()
    {
        //Disabling the camera script, so that it no longer follows the player.
        theCam = FindObjectOfType<CameraController>();
        theCam.enabled = false;

        activeCounter = activeTime;

        shotCounter = timeBetweenShots1;
        shaker = FindObjectOfType<CameraShaker>();
    }

    // Update is called once per frame
    void Update()
    {
        //Slowly moving the camera to the required position.
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position,
            camSpeed * Time.deltaTime);
        if (!battleEnded)
        {
            if (BossHealthController.instance.currentHealth > threshold1)
            {
                if (activeCounter > 0)
                {
                    activeCounter -= Time.deltaTime;
                    if (activeCounter <= 0)
                    {
                        fadeCounter = fadeoutTime;
                        anim.SetTrigger("vanish");
                    }
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots1;
                        Instantiate(bullet, shotPoint.position, Quaternion.identity);
                    }
                }
                else if (fadeCounter > 0)
                {
                    fadeCounter -= Time.deltaTime;
                    if (fadeCounter <= 0)
                    {
                        theBoss.gameObject.SetActive(false);
                        inactiveCounter = inactiveTime;
                    }
                }
                else if (inactiveCounter > 0)
                {
                    inactiveCounter -= Time.deltaTime;
                    if (inactiveCounter <= 0)
                    {
                        theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                        theBoss.gameObject.SetActive(true);
                        activeCounter = activeTime;

                        shotCounter = timeBetweenShots1;
                    }
                }
            }
            else
            {
                if (targetPoint == null)
                {
                    targetPoint = theBoss;
                    fadeCounter = fadeoutTime;
                    anim.SetTrigger("vanish");
                }
                else
                {
                    if (Vector3.Distance(theBoss.position, targetPoint.position) > .02f)
                    {
                        theBoss.position = Vector3.MoveTowards(theBoss.position, targetPoint.position, moveSpeed * Time.deltaTime);

                        if (Vector3.Distance(theBoss.position, targetPoint.position) <= .02f)
                        {
                            fadeCounter = fadeoutTime;
                            anim.SetTrigger("vanish");
                        }

                        shotCounter -= Time.deltaTime;
                        if (shotCounter <= 0)
                        {
                            if (BossHealthController.instance.currentHealth > threshold2)
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }
                            Instantiate(bullet, shotPoint.position, Quaternion.identity);
                        }
                    }
                    else if (fadeCounter > 0)
                    {
                        fadeCounter -= Time.deltaTime;
                        if (fadeCounter <= 0)
                        {
                            theBoss.gameObject.SetActive(false);
                            inactiveCounter = inactiveTime;
                        }
                    }
                    else if (inactiveCounter > 0)
                    {
                        inactiveCounter -= Time.deltaTime;
                        if (inactiveCounter <= 0)
                        {
                            theBoss.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                            //Makes sure that we never end up in permanent loop and freezing Unity.
                            int whileBreaker = 0;
                            while (targetPoint.position == theBoss.position && whileBreaker < 100)
                            {
                                targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                                whileBreaker++;
                            }

                            theBoss.gameObject.SetActive(true);

                            if (BossHealthController.instance.currentHealth > threshold2)
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            fadeCounter -= Time.deltaTime;
            if(fadeCounter < 0)
            {
                if(winObjects != null)
                {
                    winObjects.SetActive(true);
                    winObjects.transform.SetParent(null);
                }

                theCam.enabled = true;
                gameObject.SetActive(false);

                //Remembering, that this boss has been bitten.
                PlayerPrefs.SetInt(bossRef, 1);
                Time.timeScale = 1f;
            }
        }
    }

    public void EndBattle()
    {
        battleEnded = true;
        Time.timeScale = .5f;
        
        fadeCounter = fadeoutTime;
        StartCoroutine(shaker.Shake(1f, 2f));
        anim.SetTrigger("vanish");
        theBoss.GetComponent<Collider2D>().enabled = false;

        BossBullet[] bullets = FindObjectsOfType<BossBullet>();
        if(bullets.Length > 0)
        {
            foreach (BossBullet bb in bullets)
            {
                Destroy(bb.gameObject);
            }
        }
        
    }
}
