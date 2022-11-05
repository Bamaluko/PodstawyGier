using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public float distanceToOpen;

    private PlayerController thePlayer;

    private bool playerExiting;

    public Transform exitPoint;

    public float movePlayerSpeed;

    public string levelToLoad;

    public string alternative1;

    public string alternative2;

    public string buttonText1;

    public string buttonText2;


    public GameObject choiceScreen;

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
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position,
                movePlayerSpeed * Time.deltaTime);
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
        if (!PlayerPrefs.HasKey(alternative1) && !PlayerPrefs.HasKey(alternative2))
        {
            ChoiceWindow();
        }

        //We slowly cover the screen.
        UIController.instance.StartFadeToBlack();

        //Wait fo 1.5.
        yield return new WaitForSeconds(1.5f);

        //We set new spawn point.
        RespawnController.instance.setSpawn(exitPoint.position);

        //Now the player can move.
        thePlayer.canMove = true;

        //We enable the animator again, so that it works as usual.
        thePlayer.anim.enabled = true;

        //We start slowly uncovering the scene.
        UIController.instance.StartFadeFromBlack();

        PlayerPrefs.SetString("ContinueLevel", levelToLoad);
        PlayerPrefs.SetFloat("PosX", exitPoint.position.x);
        PlayerPrefs.SetFloat("PosY", exitPoint.position.y);
        PlayerPrefs.SetFloat("PosZ", exitPoint.position.z);

        //New scene is loaded.
        SceneManager.LoadScene(levelToLoad);
    }

    public void ChoiceWindow()
    {
        if (!choiceScreen.activeSelf)
        {
            //We pass player prefs entries for each option and prompt for the button.
            UIController.instance.ChoiceWindow(alternative1, alternative2, buttonText1, buttonText2);
        }
    }
}
