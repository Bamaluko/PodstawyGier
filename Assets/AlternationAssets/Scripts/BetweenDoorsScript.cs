using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenDoorsScript : MonoBehaviour
{
    public string required1;

    public string required2;

    public string excluding1;

    public string excluding2;
    
    // Start is called before the first frame update
    void Start()
    {
        if ((!PlayerPrefs.HasKey(required1) && !PlayerPrefs.HasKey(required2)) || PlayerPrefs.HasKey(excluding1) ||
            PlayerPrefs.HasKey(excluding2))
        {
            Destroy(gameObject);
        }
    }
}
