using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 oryginalPos = transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float y = Random.Range(-.1f, .1f) * magnitude;
            transform.position =
                new Vector3(transform.position.x,transform.position.y - y, transform.position.z);
            
            elapsed += Time.deltaTime;
            
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, oryginalPos.y, transform.position.z);
    }
}
