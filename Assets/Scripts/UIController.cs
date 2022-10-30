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
    * healthSlider - slider that presents how much health we currently have.
    * 
    * healthText - gives us exact number of current health.
    */

    public static UIController instance;

    public Slider healthSlider;

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
        //Dealing with text presentation of player health
        StringBuilder sb = new StringBuilder();
        sb.Append(PlayerHealthController.instance.currentHealth);
        sb.Append("/");
        sb.Append(PlayerHealthController.instance.maxHealth);
        healthText.text = sb.ToString();

        //ONLY FOR TESTING IF INTERFACE WORKS. COMMENT IF UNNEEDED!
        UpdateHealth(PlayerHealthController.instance.currentHealth, PlayerHealthController.instance.maxHealth);
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}

