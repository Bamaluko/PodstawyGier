using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEditor;

public class DoorController : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
    * 
    * anim - animator used to control animations of the door.
    * 
    * distanceToOpen - distance to the door needed to start opening.
    * 
    * thePlayer - reference to the player.
    * 
    * playerExiting - telling if the player is currently exiting the area.
    * 
    * exitPoint - where to move the player when he enters the door.
    * 
    * movePlayerSpeed - how fast to move the player towards the exit point.
    * 
    * levelToLoad - new scene to be loaded.
    * 
    * alternative1, alternative2 - alternative player prefs entries for choosing alternation mechanic.
    * 
    * buttonText1, buttonText2 - what should be written on the buttons, when choosing new mechanic.
    */

    public Animator anim;
    public bool isAlternation;

    public Transform exitPoint;
    public string levelToLoad;

    public string alternative1;
    public string alternative2;

    public string buttonText1;
    public string buttonText2;

    public VideoClip videoClip1;
    public VideoClip videoClip2;
    
    private float distanceToOpen = 7.5f;
    private float movePlayerSpeed = 8;
    private bool playerExiting;
    private PlayerController thePlayer;

    private float colorChanger = -0.004f;

    // Start is called before the first frame update
    void Start()
    {
        //Setting up the reference to the player.
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToOpen)
        {
            anim.SetBool("doorOpen", true);
        }
        else
        {
            anim.SetBool("doorOpen", false);
        }

        if (playerExiting)
        {
            //Moving player to the new area.
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position,
                new Vector3(transform.position.x - 20 * transform.localScale.x, transform.position.y,
                    transform.position.z), movePlayerSpeed * Time.deltaTime);
        }

        if (!PlayerPrefs.HasKey(alternative1) && !PlayerPrefs.HasKey(alternative2) && isAlternation)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = 
                new Color(gameObject.GetComponentInChildren<SpriteRenderer>().color.r + colorChanger, 
                gameObject.GetComponentInChildren<SpriteRenderer>().color.g + colorChanger, 
                gameObject.GetComponentInChildren<SpriteRenderer>().color.b - colorChanger,
                gameObject.GetComponentInChildren<SpriteRenderer>().color.a);

            if(gameObject.GetComponentInChildren<SpriteRenderer>().color.b >= 2 ||
                gameObject.GetComponentInChildren<SpriteRenderer>().color.b <= 1)
            {
                colorChanger = -colorChanger;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!playerExiting)
            {
                thePlayer.canMove = false;

                PlayerHealthController.instance.FillHealth();

                StartCoroutine(UseDoorCo());
            }
        }
    }

    IEnumerator UseDoorCo()
    {
        //The player is exiting the area now.
        playerExiting = true;

        //The animator should stop working now, so that we are stuck on the running animation.
        thePlayer.anim.enabled = false;

        //Trigger choice mechanic, if we are going through the door for the first time.
        if (isAlternation)
        {
            if (!PlayerPrefs.HasKey(alternative1) && !PlayerPrefs.HasKey(alternative2))
            {
                ChoiceWindow();
            }
            yield return new WaitForSeconds(0.1f);
            if (!PlayerPrefs.HasKey(alternative1) && !PlayerPrefs.HasKey(alternative2))
            {
                //The player is exiting the area now.
                playerExiting = false;

                //The animator should stop working now, so that we are stuck on the running animation.
                thePlayer.anim.enabled = true;
                thePlayer.transform.position = new Vector3(thePlayer.transform.position.x + 2 * transform.localScale.x, thePlayer.transform.position.y, 0);

                //Now the player can move.
                thePlayer.canMove = true;
                yield break;
            }
        }
        
        
        thePlayer.theRB.gravityScale = 0f;
        //We slowly cover the screen.
        UIController.instance.StartFadeToBlack();
        //yield return new WaitForSeconds(1.0f);
        
        VitalPoint point = FindObjectOfType<VitalPoint>();
        if (point != null)
        {
            Destroy(point.gameObject);
        }

        //Wait fo 1.5.
        yield return new WaitForSeconds(1.0f);

        //We set new spawn point.
        RespawnController.instance.setSpawn(exitPoint.position);

        //We enable the animator again, so that it works as usual.
        thePlayer.anim.enabled = true;

        PlayerPrefs.SetString("ContinueLevel", levelToLoad);
        PlayerPrefs.SetFloat("PosX", exitPoint.position.x);
        PlayerPrefs.SetFloat("PosY", exitPoint.position.y);

        //New scene is loaded.
        SceneManager.LoadScene(levelToLoad);

        //We start slowly uncovering the scene.
        UIController.instance.StartFadeFromBlack();

        thePlayer.transform.position = new Vector3(exitPoint.position.x, exitPoint.position.y, 0);
        
        thePlayer.theRB.gravityScale = 5f;
        //Now the player can move.
        thePlayer.canMove = true;
    }

    public void ChoiceWindow()
    {
        //We pass player prefs entries for each option and prompt for the button.
        UIController.instance.ChoiceWindow(alternative1, alternative2, buttonText1, buttonText2, videoClip1, videoClip2);
    }
}
