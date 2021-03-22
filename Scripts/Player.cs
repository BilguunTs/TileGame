using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float playerSpeed = 6f;
    [SerializeField] float jumpSpeed= 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 dethkick = new Vector2(250f, 250f);
    //State
    bool isAlive = true;
 
    //Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFootCollider;
    float gravityScaleAtStart;
   
    void Start()
    {
       myRigidBody = GetComponent<Rigidbody2D>();
       myAnimator = GetComponent<Animator>();
       myBodyCollider = GetComponent<CapsuleCollider2D>();
       myFootCollider = GetComponent<BoxCollider2D>();
       gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) {
            return;
         }
        
        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Die();
       
    }
    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards"))||
            myFootCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))) {
            isAlive = false;
            myAnimator.SetTrigger("dying");
            myRigidBody.velocity = dethkick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    private void ClimbLadder()
    {
        if (!myFootCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            myAnimator.SetBool("climbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;         
        }

        float controlThrow =Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myRigidBody.gravityScale = 0f;
        myAnimator.SetBool("climbing", playerHasVerticalSpeed);
        

    }
    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow*playerSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    
    }
    private void Jump()
    {
        if (!myFootCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;  }
        
        if (Input.GetButtonDown("Jump"))
        {
       
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }
    
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("running", true);
            myAnimator.transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f);
        }else if (!playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("running", false);
        }
    }
}
