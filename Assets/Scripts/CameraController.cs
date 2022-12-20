using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
     * 
     * player - This is a PlayerController. Special class created to deal with the player things.
     * 
     * boundsBox - Typical 2D box colider. Marks boundaries of the camera.
     * 
     * halfHeight - Half of camera (its boundaries) height.
     * 
     * halfWidth - Half of camera (its boundaries) width.
     */

    private PlayerController player;
    public BoxCollider2D boundsBox;

    private float halfHeight;
    private float halfWidth;

    public Transform vitalPoint;
    private bool vitalActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //We find any instance of Player (should be only one) and assign it to player variable.
        player = FindObjectOfType<PlayerController>();
        //Taht's how we get height of the camera.
        halfHeight = Camera.main.orthographicSize;
        //Cameras aspect is it's ratio width/height. aspect * halfHeight = halfWidth.
        halfWidth = halfHeight * Camera.main.aspect;

        if (!PlayerPrefs.HasKey("Boss1") && SceneManager.GetActiveScene().name == "Boss1")
        {
            AudioManager.instance.StopMusic();
        } else
        {
            AudioManager.instance.PlayLevelMusic();
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && vitalPoint != null)
        {
            transform.position = vitalPoint.position;
            vitalActive = true;
        }
        //If there is a player we do stuff.
        else if(!vitalActive)
        {
            if (player != null)
            {
                //Big maths. Setting position of the camera. We want the camera to follow the player, but we also want it to
                //get stuck on level boundaries. Mathf.Clamp returns minimum or maximu value given, if the actual one is lower
                //than minimum or higher tham maximum.

                transform.position = new Vector3(
                    Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth),
                    Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight),
                    transform.position.z);
                    
            }
            else
            {
                player = FindObjectOfType<PlayerController>();
            }
        }

        if(Input.GetKeyUp(KeyCode.V))
        {
            vitalActive = false;
        }
    }
    
}

