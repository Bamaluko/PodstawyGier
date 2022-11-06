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
            gameObject.SetActive(false);
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
            }

            if (unlockBecomeBall)
            {
                player.canBecomeBall = true;
            }

            if (unlockDropBomb)
            {
                player.canDropBomb = true;
            }

            PlayerPrefs.SetString(ID, ID);
            Destroy(gameObject);
        }
    }
}
