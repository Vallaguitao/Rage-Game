using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCubes : MonoBehaviour
{
    [SerializeField] private float pushForce;
    [SerializeField] private GameObject cube;
    [SerializeField] private Rigidbody cubeRb;
    // Start is called before the first frame update
    void Start()
    {
        cubeRb = GetComponent<Rigidbody>();
        StartPush();
        StartCoroutine(LatePush());
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,4f);
    }
    void StartPush()
    {
        cubeRb.AddForce(new Vector3(-8, -1 , 0) * pushForce, ForceMode.Impulse);
    }

    IEnumerator LatePush()
    {
        yield return new WaitForSeconds(2f);
        pushForce = 5f;
        cubeRb.AddForce(new Vector3(8, -1, 0) * pushForce, ForceMode.Impulse);
    }
}
