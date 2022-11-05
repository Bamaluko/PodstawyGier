using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbillityUnlock : MonoBehaviour
{
    public bool unlockDoubleJump, unlockDash, unlockBecomeBall, unlockDropBomb;
    public GameObject pickupEffect;

    public string unlockMessage;
    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerAbillityTracker player = other.GetComponentInParent<PlayerAbillityTracker>();

            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;
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

            if (player.canDoubleJump && player.canDash && player.canBecomeBall && player.canDropBomb) unlockMessage = "All abillities unlocked!";
            else if (player.canDoubleJump && player.canDash && player.canBecomeBall) unlockMessage = "Double jump, Dash and change to ball unlocked!";
            else if (player.canDoubleJump && player.canDash && player.canDropBomb) unlockMessage = "Double jump, Dash and Drop bomb unlocked!";
            else if (player.canDoubleJump && player.canDropBomb && player.canBecomeBall) unlockMessage = "Double jump, Change to ball and Drop bomb unlocked!";
            else if (player.canDash && player.canDropBomb && player.canBecomeBall) unlockMessage = "Dash, Change to ball and Drop bomb unlocked!";
            else if (player.canDoubleJump && player.canDash) unlockMessage = "Double jump and Dash unlocked!";
            else if (player.canDoubleJump && player.canBecomeBall) unlockMessage = "Double jump and Change to ball unlocked!";
            else if (player.canDoubleJump && player.canDropBomb) unlockMessage = "Double jump and Drop bomb unlocked!";
            else if (player.canDash && player.canBecomeBall) unlockMessage = "Dash and Change to ball unlocked!";
            else if (player.canDash && player.canDropBomb) unlockMessage = "Dash and Drop bomb unlocked!";
            else if (player.canBecomeBall && player.canDropBomb) unlockMessage = "Change to ball and Drop bomb unlocked!";
            else if (player.canDoubleJump) unlockMessage = "Double Jump unlocked!";
            else if (player.canDash) unlockMessage = "Dash unlocked!";
            else if (player.canBecomeBall) unlockMessage = "Change to ball unlocked!";
            else if (player.canDropBomb) unlockMessage = "Drop bomb unlocked!";

            Instantiate(pickupEffect, transform.position, transform.rotation);

            unlockText.transform.parent.SetParent(null);
            unlockText.transform.parent.position = transform.position;

            unlockText.text = unlockMessage;
            unlockText.gameObject.SetActive(true);

            Destroy(unlockText.transform.parent.gameObject, 5f);
            Destroy(gameObject);
        }
    }
}
