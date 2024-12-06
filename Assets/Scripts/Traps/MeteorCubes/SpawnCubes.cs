using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{

    [SerializeField] private GameObject meteors;
    [SerializeField] private float interval;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnMeteors", interval, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMeteors()
    {
        Instantiate(meteors, gameObject.transform);
    }
}
