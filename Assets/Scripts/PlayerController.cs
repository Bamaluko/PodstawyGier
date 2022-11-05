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

    private bool canDoubleJump;

    public float dashSpeed, dashTime;
    private float dashCounter;

    public SpriteRenderer theSR, afterImage;
    public float afterImageLifetime, timeBetweenAfterImages;
    private float afterImageCounter;
    public Color afterImageColor;

    public float waitAfterDashing;
    private float dashRechargeCounter;

    public GameObject standing, ball;
    public float waitToBall;
    private float ballCounter;
    public Animator ballAnim;

    public Transform bombPoint;
    public GameObject bomb;

    private PlayerAbillityTracker abilities;

    public bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        abilities = GetComponent<PlayerAbillityTracker>();

        //In the future we will use it to, for example block player movement in some situations
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && Time.timeScale != 0)
        {
            if (dashRechargeCounter > 0)
            {
                dashRechargeCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetButtonDown("Fire2") && standing.activeSelf && abilities.canDash)
                {
                    dashCounter = dashTime;
                    showAfterImage();
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;

                theRB.velocity = new Vector2(dashSpeed * transform.localScale.x, theRB.velocity.y);

                afterImageCounter -= Time.deltaTime;

                if (afterImageCounter <= 0)
                {
                    showAfterImage();
                }

                dashRechargeCounter = waitAfterDashing;
            }
            else
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
            }
            //Checking if on the ground. Basically we draw a small circle around groundPoint and se if there is ground within it.
            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .4f, whatIsGround);

            //JUMPING
            //Jumping. We can jump after pressing the space bar when we are on the ground.
            if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.canDoubleJump)))
            {
                if (isOnGround)
                {
                    canDoubleJump = true;
                }
                else
                {
                    canDoubleJump = false;

                    anim.SetTrigger("doubleJump");
                }

                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }

            //FIRING
            //If left mouse button pressed.
            if (Input.GetButtonDown("Fire1"))
            {
                if (standing.activeSelf)
                {
                    Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
                    anim.SetTrigger("shotFired");
                }
                else if (ball.activeSelf && abilities.canDropBomb)
                {
                    Instantiate(bomb, bombPoint.position, bombPoint.rotation);
                }
            }

            //ball mode
            if (!ball.activeSelf)
            {
                if (Input.GetAxisRaw("Vertical") < -0.9f && abilities.canBecomeBall)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(true);
                        standing.SetActive(false);
                    }
                }
                else
                {
                    ballCounter = waitToBall;
                }
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") > 0.9f)
                {
                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);
                    }
                }
                else
                {
                    ballCounter = waitToBall;
                }
            }
        }

        //Setting variables for animator.
        if (standing.activeSelf)
        {
            anim.SetBool("isOnGround", isOnGround);
            anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        }
        else if (ball.activeSelf)
        {
            ballAnim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        }
    }

    public void showAfterImage()
    {
        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = theSR.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        Destroy(image.gameObject, afterImageLifetime);

        afterImageCounter = timeBetweenAfterImages;
    }
}
