using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFightTrigger : MonoBehaviour
{
    [SerializeField] Stage1Boss bossScript;
    [SerializeField] Transform playerTransform;

    private void Update()
    {
        if(bossScript.ToStart)
        {
            if (playerTransform.position.x < transform.position.x)
            {
                playerTransform.position = new Vector3(transform.position.x, playerTransform.position.y, playerTransform.position.z);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(playerTransform.position.x > transform.position.x)
            {
                bossScript.ToStart = true;
            }
        }
    }

}
