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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey(prefRequired))
        {
            //Vector3 position = gameObject.GetComponent<Renderer>().bounds.center;
            //transform.RotateAround(position, new Vector3(0,0,1), rotationSpeed * Time.deltaTime);
            
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}
