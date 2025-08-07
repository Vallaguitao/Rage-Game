using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Attachable
{
    Yes,
    No
}

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] protected GameObject pointA;
    [SerializeField] protected GameObject pointB;

    [SerializeField] protected Rigidbody2D rigidBody;
    [SerializeField] protected Transform currentPoint;
    [SerializeField] protected float speed = 6.5f;
    [SerializeField] protected Attachable attachable;
    [SerializeField] protected Direction direction;

    // Start is called before the first frame update
    protected virtual void Start()
    {

        currentPoint = pointA.transform;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Move();

    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        //rigidBody.velocity is removed becaused using Rigidbody.velocity to move the platform? Because if both the parent and the child have Rigidbodies, the child's RB is not affected when the parent's RB is moved via it's velocity or forces
        
        
        if(GameManager.gameManagerScript.isPaused)
        {

        }
        else
        {
            if ((direction == Direction.Left) || (direction == Direction.Right))
            {
                if (currentPoint == pointB.transform)
                {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                    //rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
                }
                else
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                    //rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
                }
            }

            else if ((direction == Direction.Up) || (direction == Direction.Down))
            {
                if (currentPoint == pointB.transform)
                {
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.down * speed * Time.deltaTime);
                }
            }
        }

        if ((Vector2.Distance(transform.position, pointA.transform.position) < 1f) && (currentPoint == pointB.transform))
        {
            currentPoint = pointA.transform;
        }
        else if ((Vector2.Distance(transform.position, pointB.transform.position) < 1f) && (currentPoint == pointA.transform))
        {

            currentPoint = pointB.transform;

        }

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (attachable == Attachable.Yes)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //collision.gameObject.transform.SetParent(transform, false);
                collision.transform.parent = this.transform;
            }
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (attachable == Attachable.Yes)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.parent = null;
                //collision.gameObject.transform.SetParent(null, false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 1f);
        Gizmos.DrawWireSphere(pointB.transform.position, 1f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

}
