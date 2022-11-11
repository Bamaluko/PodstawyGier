using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbillityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump, unlockDash, unlockBecomeBall, unlockDropBomb;
    public string ID;

    public void Start()
    {
        if (PlayerPrefs.HasKey(ID))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerAbillityTracker player = other.GetComponentInParent<PlayerAbillityTracker>();

            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;
                PlayerPrefs.SetInt("canDoubleJump", 1);
            }

            if (unlockDash)
            {
                player.canDash = true;
                PlayerPrefs.SetInt("canDash", 1);
            }

            if (unlockBecomeBall)
            {
                player.canBecomeBall = true;
                PlayerPrefs.SetInt("canBecomeBall", 1);
            }

            if (unlockDropBomb)
            {
                player.canDropBomb = true;
                PlayerPrefs.SetInt("canDropBomb", 1);
            }

            PlayerPrefs.SetString(ID, ID);
            Destroy(gameObject);
        }
    }
}
