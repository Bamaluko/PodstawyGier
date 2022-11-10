using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public GameObject continueButton;
  
    public PlayerAbillityTracker player;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueButton.SetActive(true);
        }
        
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("canDoubleJump", 0);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(newGameScene);
        PlayerPrefs.SetInt("max_health", 6);
    }

    public void Continue()
    {
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"),
            PlayerPrefs.GetFloat("PosZ"));
        if (PlayerPrefs.GetInt("canDoubleJump") == 1)
        {
            player.canDoubleJump = true;
        }
        if (PlayerPrefs.GetInt("canDash") == 1)
        {
            player.canDash = true;
        }
        if (PlayerPrefs.GetInt("canBecomeBall") == 1)
        {
            player.canBecomeBall = true;
        }
        if (PlayerPrefs.GetInt("canDropBomb") == 1)
        {
            player.canDropBomb = true;
        }



        SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Game quit!");
    }
}
