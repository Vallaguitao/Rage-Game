using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAppearStage2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var fakeCoins = Object.FindAnyObjectByType<Coins>();

        if (fakeCoins == null)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }
}
