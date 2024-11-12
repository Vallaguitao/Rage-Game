using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    
    [SerializeField] private float delayPerBullet;
    [SerializeField] private GameObject cannonBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireCanon", 2f, delayPerBullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FireCanon()
    {
        Instantiate(cannonBulletPrefab, transform.position, transform.rotation);
    }

}
