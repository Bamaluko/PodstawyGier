using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
     * 
     * instance - static reference to this object for easy access.
     * 
     * bossHealthSlider - reference to the slider displaying bosses current health.
     * 
     * currentHealth - current health of the boss.
     * 
     * theBoss - reference to the boss battle controller.
     * 
    */

    public static BossHealthController instance;

    public Slider bossHealthSlider;

    public int currentHealth = 30;

    public BossBattle theBoss;

    public SpriteRenderer sr;

    private bool isChangingColor = false;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bossHealthSlider.maxValue = currentHealth;
        bossHealthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        isChangingColor = true;

        if(currentHealth <= 0)
        {
            currentHealth = 0;

            theBoss.EndBattle();
            AudioManager.instance.PlaySFX(0);
        } else
        {
            AudioManager.instance.PlaySFX(1);
        }
        bossHealthSlider.value = currentHealth;
    }

    public void Update()
    {
        if (sr.color.a > 0 && isChangingColor) sr.color = new Color(sr.color.r,
                                                  sr.color.g,
                                                  sr.color.b,
                                                  sr.color.a - 12f * Time.deltaTime);
        else if (sr.color.a <= 0)
        {
            sr.color = new Color(sr.color.r,
                                 sr.color.g,
                                 sr.color.b,
                                 1);
            isChangingColor = false;
        }
    }
}