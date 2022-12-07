using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    
    public IEnumerator Shake(float duration, float magnitude)
    {
        // Vector3 oryginalPos = transform.position;
        // float elapsed = 0.0f;
        // while (elapsed < duration)
        // {
        //     transform.position =
        //         new Vector3(transform.position.x, transform.position.y - magnitude, transform.position.z);
        //     //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, y, transform.position.z), .3f);
        //
        //     elapsed += Time.deltaTime;
        //     yield return null;
        //     
        // }
        // while (elapsed > 0)
        // {
        //     transform.position =
        //         new Vector3(transform.position.x, transform.position.y + magnitude, transform.position.z);
        //     //transform.position = Vector3.MoveTowards(transform.position, new Vector3(oryginalPos.x, oryginalPos.y, transform.position.z), .3f);
        //
        //     elapsed -= Time.deltaTime;
        //     yield return null;
        // }
        //
        // transform.position = oryginalPos;
        Vector3 oryginalPos = transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float y = Random.Range(-.1f, .1f) * magnitude;
            transform.position =
                new Vector3(transform.position.x, y - magnitude, transform.position.z);
            
            elapsed += Time.deltaTime;
            
            yield return null;
        }
        transform.position = oryginalPos;
    }
}
