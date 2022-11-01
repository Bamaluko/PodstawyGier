using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public GameObject continueButton;
  
    //public PlayerAbilityTracker player;

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
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(newGameScene);
    }

    public void Continue()
    {
        

        SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Game quit!");
    }
}
