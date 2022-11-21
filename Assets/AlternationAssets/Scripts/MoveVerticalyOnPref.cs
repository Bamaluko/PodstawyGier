using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVerticalyOnPref : MonoBehaviour
{
    // Start is called before the first frame update
    public string prefRequired;
    public float movementSpeed;
    public Transform point1;
    public Transform point2;
    public bool isOn;

    private Transform currentPoint;

    private void Start()
    {
        currentPoint = point1;
        point1.SetParent(null);
        point2.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey(prefRequired) || isOn)
        {
            if (transform.position == point1.position)
            {
                currentPoint = point2;
            }
            else if (transform.position == point2.position)
            {
                currentPoint = point1;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, movementSpeed * Time.deltaTime);
        }
    }
}

