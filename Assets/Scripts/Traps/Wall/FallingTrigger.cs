using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour
{

    public bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isTriggered = true;
        }
        
    }
}
