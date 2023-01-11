using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{

    /* DESCRIPTION OF ALL PROPERTIES
    * 
    * instance - static reference to this object. Accessible everywhere.
    * 
    * respawnPoint - where should the player respawn after death.
    * 
    * waitToRespawn - how long should we wait after player's death for respawn.
    * 
    * player - reference to the player game object.
    */

    public static RespawnController instance;

    private Vector3 respawnPoint;

    public float waitToRespawn;

    private GameObject player;

    public GameObject deathEffect;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Getting player object instance.
        player = PlayerHealthController.instance.gameObject;

        //Setting first respawn point to wherever the player has started.
        respawnPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Starts up a coroutine responsible for respawning.
    public void Respawn()
    {
        FindObjectOfType<PlayerController>().jumpShake = false;
        StartCoroutine(RespawnCo());
    }

    //Sets up new respawn point.
    public void setSpawn(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    //A coroutine responsible for respawning.
    IEnumerator RespawnCo()
    {
        //We deactivate the player for the time of respawning.
        player.SetActive(false);

        if (deathEffect != null)
        {
            Instantiate(deathEffect, player.transform.position, player.transform.rotation);
        }

        //This should make the coroutine wait a while.
        yield return new WaitForSeconds(waitToRespawn);
        
        UIController.instance.StartFadeToBlack();
        
        yield return new WaitForSeconds(1f);
        
        //After some time we reload the active scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //We move player to the respawn point and activate them.
        player.transform.position = respawnPoint;
        player.SetActive(true);

        //Refilling the health bar.
        PlayerHealthController.instance.FillHealth();
        
        UIController.instance.StartFadeFromBlack();
    }
}
