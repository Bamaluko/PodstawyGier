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
    * 
    * alternative1, alternative2 - alternative player prefs entries for choosing alternation mechanic.
    * 
    * buttonText1TMP, buttonText2TMP - reference to TMP text on both buttons.
    */

    public static UIController instance;

    public Slider healthSlider;

    public Image fadeScreen;

    public float fadeSpeed = 2f;

    private bool fadingToBlack, fadingFromBlack;

    public string mainMenuScene;

    public GameObject pauseScreen;

    public TMP_Text healthText;


    public GameObject choiceScreen;

    private string alternative1 = "";

    private string alternative2 = "";

    public TMP_Text buttonText1TMP;

    public TMP_Text buttonText2TMP;

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

    public void ChoiceWindow(string pref1, string pref2, string text1, string text2)
    {
        if (!choiceScreen.activeSelf)
        {
            //Activate the choice screen.
            choiceScreen.SetActive(true);

            //Remember both alternative player prefs entries.
            alternative1 = pref1;
            alternative2 = pref2;

            //Set corresponding prompts on the buttons.
            buttonText1TMP.text = text1;
            buttonText2TMP.text = text2;

            //Time within the game won't flow anymore.
            Time.timeScale = 0;
        }
    }

    public void ChoiceWindowAlternative1()
    {
        if (choiceScreen.activeSelf)
        {
            //Deactivate the window, after choice is made.
            choiceScreen.SetActive(false);

            //Set player pref entry corresponding to the clicked button.
            PlayerPrefs.SetString(alternative1, buttonText1TMP.text);
            //Time within the game will go on.
            Time.timeScale = 1;
        }
    }

    public void ChoiceWindowAlternative2()
    {
        if (choiceScreen.activeSelf)
        {
            //Deactivate the window, after choice is made.
            choiceScreen.SetActive(false);

            //Set player pref entry corresponding to the clicked button.
            PlayerPrefs.SetString(alternative2, buttonText2TMP.text);
            //Time within the game will go on.
            Time.timeScale = 1;
        }
    }
}
