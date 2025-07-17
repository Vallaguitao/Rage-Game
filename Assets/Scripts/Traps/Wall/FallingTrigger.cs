using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour
{

    [SerializeField] protected bool isTriggered;
    public bool fallOnce = false;
    [SerializeField] public bool IsTriggered { get { return isTriggered; } set { isTriggered = value; } }

    //just random values
    [SerializeField] protected Vector3 boxSize = new Vector3(3f, 3f, 3f);

    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        //Gizmos.DrawCube(transform.position, boxSize);
    }

    
}
