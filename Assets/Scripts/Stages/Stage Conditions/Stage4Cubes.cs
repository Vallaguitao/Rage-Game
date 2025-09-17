using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Cubes : MonoBehaviour
{

    public float xSpacing;
    public GameObject lightning;
    public int numberOfLightning = 4;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        InvokeRepeating("SpawnCubes", 2, 2);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }


    protected virtual void SpawnCubes()
    {
        if(gameObject.activeSelf)
        {
            if (!GameManager.gameManagerScript.isPaused)
            {
                for (int column = 0; column < numberOfLightning; column++)
                {
                    Vector3 lightningPosition;

                    if (column < numberOfLightning / 2)
                    {
                        lightningPosition = new Vector3(transform.position.x + -(column + 1) * xSpacing, transform.position.y);
                    }
                    else
                    {
                        lightningPosition = new Vector3(transform.position.x + (column - 1) * xSpacing, transform.position.y);
                    }

                    Instantiate(lightning, lightningPosition, transform.rotation);
                }
            }
        }
        
    }
}
