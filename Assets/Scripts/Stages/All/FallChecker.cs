using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallChecker : TrapsCommonality
{

    [SerializeField] AudioClip fallSound;
    [SerializeField] protected Vector3 boxSize = new Vector3(3f, 3f, 3f);


    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < transform.position.y)
        {
            PlayerDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {

        TrapsPlayerDied();
        audioManager.PlaySFX(fallSound);

    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(0, 1, 0, 1f);
        Gizmos.DrawCube(transform.position, boxSize);
    }

}
