using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGroundTrigger : MonoBehaviour
{

    [SerializeField] private bool isTriggered;

    public bool IsTriggered { get { return isTriggered; } private set { } }

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
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
        }

    }
}
