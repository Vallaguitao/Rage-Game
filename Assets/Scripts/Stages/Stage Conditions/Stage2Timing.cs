using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Timing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("beatBig", 1, 2);
        InvokeRepeating("beatSmall", 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void beatBig()
    {
        transform.localScale += new Vector3(1f, 1f, 1f);
        print("big");
    }

    private void beatSmall()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        print("small");
    }
}
