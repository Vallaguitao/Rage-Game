using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrier : MonoBehaviour
{

    [SerializeField] private GameObject capsule;
    public float xLeftLimit = -9.3f;
    public float xRightLimit = 54f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBarrierCapsule", 15f, 15f);
    }

    private void SpawnBarrierCapsule()
    {
        if(!GameManager.gameManagerScript.isPaused)
        {
            float xPosition = Random.Range(xLeftLimit, xRightLimit);

            Instantiate(capsule, new Vector2(xPosition, 0), capsule.transform.rotation);
        }
        
    }
}
