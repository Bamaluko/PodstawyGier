using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public GameObject continueButton;
  
    public PlayerAbillityTracker player;

    public GameObject creditsScreen;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMainMenuMusic();
        if (PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueButton.SetActive(true);
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("PosX", 2);
        PlayerPrefs.SetFloat("PosY", 22.5f);

        PlayerPrefs.SetInt("canDoubleJump", 0);
        SceneManager.LoadScene(newGameScene);
        PlayerPrefs.SetString("enemies_killed", "0");
        PlayerPrefs.SetString("player_deaths", "0");
        PlayerPrefs.SetString("none", "0");
    }

    public void Continue()
    {
        player.gameObject.SetActive(true);
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), 0);

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

    public void showCredits ()
    {
        creditsScreen.SetActive(true);
    }

    public void hideCredits()
    {
        creditsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Game quit!");
    }
}
