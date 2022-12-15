using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
    *
    * bossToActivate - reference to the game object, that needs to be activated to start the encounter.
    *
    * bossRef - reference to this boss battle.
    * 
    * platform - an object, that should be active, when the boss is beaten already.
    */

    public GameObject bossToActivate;

    public string bossRef;

    public GameObject platform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (PlayerPrefs.HasKey(bossRef))
            {
                if(PlayerPrefs.GetInt(bossRef) != 1)
                {
                    //If player enter the required area and the battle against this boss wasn't beaten yet
                    //we activate the boss and deactivate this object (it's unnecesseary now).
                    bossToActivate.SetActive(true);
                    this.gameObject.SetActive(false);
                }
                else
                {
                    if (platform != null)
                    {
                        platform.transform.SetParent(null);
                        platform.SetActive(true);
                    }
                }
            }
            else
            {
                //If player enter the required area and the battle against this boss never happened
                //we activate the boss and deactivate this object (it's unnecesseary now).
                bossToActivate.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
    }
}