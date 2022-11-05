using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnPrefs : MonoBehaviour
{
    public string prefRequired;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(prefRequired))
        {
            gameObject.SetActive(false);
        }
    }
}
