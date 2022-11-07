using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnPref : MonoBehaviour
{
    // Start is called before the first frame update
    public string prefRequired;
    public float rotationSpeed;

    void Start()
    {
        if (PlayerPrefs.HasKey(prefRequired))
        {
            transform.Rotate(0, 0, rotationSpeed * Random.Range(0, 160));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey(prefRequired))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}
