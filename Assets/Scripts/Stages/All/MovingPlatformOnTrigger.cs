using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformOnTrigger : MovingPlatform
{

    protected bool moveIt;
    protected bool playerRiding;
    protected float startingYPosition;

    protected override void Start()
    {
        base.Start();
        moveIt = false;
        playerRiding = false;
        startingYPosition = transform.position.y;
        speed = 10f;
    }

    protected override void FixedUpdate()
    {
        if (moveIt)
        {
            base.FixedUpdate();

            if(!playerRiding)
            {
                if(Vector2.Distance(transform.position, pointB.transform.position) < 1f)
                {
                    moveIt = false;
                    transform.position = new Vector2(transform.position.x, startingYPosition);
                }
            }
        }
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (attachable == Attachable.Yes)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //collision.gameObject.transform.SetParent(transform, false);
                collision.transform.parent = this.transform;
                moveIt = true;
                playerRiding = true;
            }
        }

        
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        if (attachable == Attachable.Yes)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.parent = null;
                //collision.gameObject.transform.SetParent(null, false);
                currentPoint = pointB.transform;

                playerRiding = false;
            }
        }

    }

}
