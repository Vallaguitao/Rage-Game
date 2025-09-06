using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackground : MonoBehaviour
{

    [SerializeField] private float startPosition;
    [SerializeField] private float backgroundWidth;
    [SerializeField] private float parallaxEffect;
    [SerializeField] private GameObject sceneMainCamera;

    // Start is called before the first frame update
    void Start()
    {
        sceneMainCamera = GameObject.Find("Main Camera");
        startPosition = transform.position.x;
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float distance = sceneMainCamera.transform.position.x * parallaxEffect; //1 = move, 0 = stay
        float movement = sceneMainCamera.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if(movement > startPosition + backgroundWidth)
        {
            startPosition += backgroundWidth;
        }
        else if(movement < startPosition - backgroundWidth)
        {
            startPosition -= backgroundWidth;
        }

    }
}
