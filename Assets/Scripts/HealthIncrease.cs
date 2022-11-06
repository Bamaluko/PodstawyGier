using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : MonoBehaviour
{

    public int points;
    // Start is called before the first frame update
    void Start()

    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //PlayerHealthController player = other.GetComponentInParent<PlayerHealthController>(); ;
            //player.MaxHealtIncrease(points);
            //Destroy(gameObject);
        }
    }
}

