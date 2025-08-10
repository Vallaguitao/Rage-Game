using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Idle,
    Attack,
    Move
}

public enum AttackPattern
{
    Attack1,
    Attack2,
    Attack3,
    Attack4,
    Attack5
}

public class Stage1Boss : MonoBehaviour
{

    [SerializeField] private GameObject barrier;
    [SerializeField] private bool activateBarrier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        activatingBarrier(barrierRNG(0, 2));
    }

    private bool barrierRNG(int minimum, int maximum)
    {
        int randomNumber = Random.Range(minimum, maximum);

        if(randomNumber == 0)
        {
            activateBarrier = false;
        }
        else
        {
            activateBarrier = true;    
        }

        return activateBarrier;
    }

    private void activatingBarrier(bool lucky)
    {
        if(lucky)
        {
            barrier.SetActive(true);
        }
        else
        {
            barrier.SetActive(false);
        }
    }
}
