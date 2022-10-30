using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* DESCRIPTION OF ALL PROPERTIES
    * 
    * theRB - Rigid body of the current object.
    * 
    * moveSpeed - Player move speed asigned in Unity.
    * 
    * jumpForce - Determines how high can player jump.
    * 
    * groundPoint - Is a Transform [x, y] defining a point on player sprite. Usef to determin if player is on ground.
    * 
    * isOnGround - Flag set to true if groundPoint is close enough to object, that has collision with the player enabled.
    * 
    * whatIsGround - Layer mask from unity. This layer covers everything that is considered to be ground.
    * 
    * anim - Animator used to deal with player animations.
    * 
    * shotToFire - Bullet Controller. Class dealing with bullets.
    * 
    * shotPoint - Transform [x, y] indicating point to spawn the shot.
    * 
    * canMove - if set to false, player cant make the character move.
    */

    public Rigidbody2D theRB;

    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;

    public Animator anim;

    public BulletController shotToFire;
    public Transform shotPoint;

    public bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        //In the future we will use it to, for example block player movement in some situations
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Time.timeScale != 0)
        {
            //If we are not dashing, we can do stuff.
            //MOVING
            //Moving sideways. GetAxisRaw checks if we are moving left or right. "Horizontal" is "a", "d" or left/right keys.
            //Without "Raw" it would take a while before player reaches full speed. Would make movement kind of slippery.
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

            //Changing direction. We basicaly rotate the Rigid Body round the x axis, if we face to the left.
            if (theRB.velocity.x < 0)
            {
                theRB.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            //If we face to the right, we just go with [1, 1, 1] vector.
            else if (theRB.velocity.x > 0)
            {
                theRB.transform.localScale = Vector3.one;
            }

            //Checking if on the ground. Basically we draw a small circle around groundPoint and se if there is ground within it.
            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .4f, whatIsGround);

            //JUMPING
            //Jumping. We can jump after pressing the space bar when we are on the ground.
            if (Input.GetButtonDown("Jump") && isOnGround)
            {
                //And the jump itself. We don't change the x axis movement, but we move up by the jumpForce.
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }

            //FIRING
            //If left mouse button pressed.
            if (Input.GetButtonDown("Fire1"))
            {
                //We create a shotToFire. Our bullet, that will go into direction we are facing. We set it's x direction based
                //on that of the player.
                Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
                //We trigger animation for firing.
                anim.SetTrigger("shotFired");
            }
        }

         //Setting variables for animator.
         anim.SetBool("isOnGround", isOnGround);
         anim.SetFloat("speed", Math.Abs(theRB.velocity.x));
    }
}
