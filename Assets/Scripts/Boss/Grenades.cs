using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenades : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private bool doIt;

    public bool DoIt { get { return doIt; } set {  doIt = value; } }

    [SerializeField] private Rigidbody2D grenadeRigidBody;


    // Start is called before the first frame update
    void Start()
    {
        doIt = false;
        grenadeRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(doIt)
        {
            //grenadeRigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            //grenadeRigidBody.AddForce(transform.TransformDirection(Vector3.forward) * 1600);

            //doIt = false;
        }

    }
}
