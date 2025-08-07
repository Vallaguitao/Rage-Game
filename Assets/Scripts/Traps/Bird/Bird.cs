using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected SpawnBird spawnBirdScript;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        spawnBirdScript = GetComponentInParent<SpawnBird>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(transform.parent != null)
        {
            switch(spawnBirdScript.Direction)
            {
                case Direction.Left:
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                    break;
                case Direction.Right:
                    transform.Translate(Vector3.right * speed * Time.deltaTime);
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    break;
                default:
                    break;

            }
        }
        
    }
}
