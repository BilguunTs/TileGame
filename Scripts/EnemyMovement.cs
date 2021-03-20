using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed = 0.5f;
    Rigidbody2D myRigidBody;
    SpriteRenderer mySprite;
    bool shouldFlip=false;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (shouldFlip)
        {
            mySprite.flipX = true;
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
        else
        {
            mySprite.flipX =false;
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("EnemyFliper"))
        {
            shouldFlip = shouldFlip == true ? false : true;
        }
    }
   
}
