using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorBoss : Stage4Cubes
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, 10f);
    }

}
