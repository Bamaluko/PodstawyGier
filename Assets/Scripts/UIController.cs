using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text;

public class UIController : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
    * 
    * instance - static reference to this UIController. Accessible everywhere.
    * 
    * fadeSpeed - the speed that the screen will be covered/uncovered in.
    * 
    * fadingToBlack - true if we are trying to cover the screen.
    * 
    * fadingFromBlack - true if we are restoring visibility.
    * 
    * mainMenuSceene - name of the scene that represents the main menu of the game.
    * 
    * pauseScreen - objects to be activated, when the game is paused.
    */

    public static UIController instance;

    public Slider healthSlider;

    public Image fadeScreen;

    public float fadeSpeed = 2f;

    private bool fadingToBlack, fadingFromBlack;

    public string mainMenuScene;

    public GameObject pauseScreen;

    public TMP_Text healthText;

    private void Awake()
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
        //UpdateHealth(PlayerHealthController.instance.currentHealth, PlayerHealthController.instance.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(PlayerHealthController.instance.currentHealth);
        sb.Append("/");
        sb.Append(PlayerHealthController.instance.maxHealth);
        healthText.text = sb.ToString();

        if (fadingToBlack)
        {
            //Will move the alpha value towards 100% in given time.
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            //When we are done, we stop changing color.
            if (fadeScreen.color.a == 1f)
            {
                fadingToBlack = false;
            }
        }
        else if (fadingFromBlack)
        {
            //Will move the alpha value towards 100% in given time.
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            //When we are done, we stop changing color.
            if (fadeScreen.color.a == 0f)
            {
                fadingFromBlack = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void StartFadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }

    public void StartFadeFromBlack()
    {
        fadingToBlack = false;
        fadingFromBlack = true;
    }

    public void PauseUnpause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);

            //Time within the game won't flow anymore.
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.SetActive(false);

            //Time within the game will resume.
            Time.timeScale = 1;
        }
    }

    //Sends us to the main menu.
    public void GoToMainMenu()
    {
        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        instance = null;
        Destroy(gameObject);

        Time.timeScale = 1;

        SceneManager.LoadScene(mainMenuScene);
    }
}
