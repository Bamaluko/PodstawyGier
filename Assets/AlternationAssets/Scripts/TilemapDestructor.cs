using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapDestructor : MonoBehaviour
{
    public Transform[] points;

    public GameObject effect;

    public void Destruction()
    {
        foreach (Transform point in points )
        {
            Instantiate(effect, point.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
