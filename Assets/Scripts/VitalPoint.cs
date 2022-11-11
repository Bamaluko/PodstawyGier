using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalPoint : MonoBehaviour
{
    public GameObject objectOfInterest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objectOfInterest == null)
        {
            Destroy(gameObject);
        }
    }
}
