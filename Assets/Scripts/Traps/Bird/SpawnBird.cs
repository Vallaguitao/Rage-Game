using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBird : MonoBehaviour
{

    public GameObject Bird;
    public float interval = 5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBirdies", interval, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnBirdies()
    {
        Instantiate(Bird, transform.position, transform.rotation);
    }
}
