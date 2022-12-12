using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public int treshold1, treshold2;

    public float activeTime, fadeoutTime, inactiveTime;
    private float activeCounter, fadeCounter, inactiveCounter;

    public Transform[] spawnPoints;
    private Transform targetPoint;
    public float moveSpeed;

    public Animator anim;

    public Transform theBoss;

    public float timeBetweenShots1, timeBetweenShots2;
    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;

    private bool battleEnd;

    // Start is called before the first frame update
    void Start()
    {
        activeCounter = activeTime;

        shotCounter = timeBetweenShots1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!battleEnd)
        {
            if (BossHealthController.instance.currentHealth > treshold1)
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
                            if (PlayerHealthController.instance.currentHealth > treshold2)
                            {
                                shotCounter = timeBetweenShots1;
                            }
                            else
                            {
                                shotCounter = timeBetweenShots2;
                            }
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

                            targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                            int wileBreaker = 0;
                            while (targetPoint.position == theBoss.position && wileBreaker < 100)
                            {
                                targetPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                                wileBreaker++;
                            }

                            theBoss.gameObject.SetActive(true);

                            if (PlayerHealthController.instance.currentHealth > treshold2)
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

            if (fadeCounter < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void EndBattle()
    {
        battleEnd = true;
        fadeCounter = fadeoutTime;
        anim.SetTrigger("vanish");
        theBoss.GetComponent<Collider2D>().enabled = false;

        BossBullet[] bullets = FindObjectsOfType<BossBullet>();
        if (bullets.Length > 0)
        {
            foreach (BossBullet bb in bullets)
            {
                Destroy(bb.gameObject);
            }
        }

    }
}
