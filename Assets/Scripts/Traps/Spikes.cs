using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Spikes : TrapsCommonality
{

    public AnimationCurve animCurve;
    public float yPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        yPosition = transform.position.y;
        isDestructible = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position = new Vector3(transform.position.x, animCurve.Evaluate(Time.time % animCurve.length) + yPosition , transform.position.z);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            base.PlayerDied();
        }
    }
}
