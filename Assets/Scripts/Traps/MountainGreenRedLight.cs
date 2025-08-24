using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MountainGreenRedLight : TrapsCommonality
{

    [Header("Mountain Code")]
    [SerializeField] private float duration = 5f;
    [SerializeField] private bool eyesOpen = true; //True: Green Light, False: Red Light
    [SerializeField] private bool lowerDuration;
    [SerializeField] private bool goExecute;
    [SerializeField] private Animator eyesAnim;

    [Header("Player Code")]
    [SerializeField] private float playerInput;

    [Header("Cloud Code")]
    [SerializeField] private MovingClouds cloud;

    protected override void Start()
    {

        base.Start();

        //InvokeRepeating("ChangeEyeStatus", duration, duration);

        eyesAnim = GetComponent<Animator>();

        eyesAnim.SetBool("IsOpenEyes", true);

        cloud = GameObject.FindAnyObjectByType<MovingClouds>();
        lowerDuration = true;
        goExecute = true;
    }

    void Update()
    {
        playerInput = gameManagerScript.playerControllerScript.HorizontalInput;

        if(goExecute)
        {
            StartCoroutine(ChangeEyeStatus());
        }
        

        if (eyesOpen)
        {
            if (playerInput != 0) //former 1  -1
            {
                base.TrapsPlayerDied();
            }

        }

        if((cloud.MoveIt) && (lowerDuration))
        {
            duration -= 1f;
            print("MOVE ITTTTTTT");
            lowerDuration = false;
            
        }
    }

    IEnumerator ChangeEyeStatus()
    {
        goExecute = false;
        yield return new WaitForSeconds(duration);

        eyesOpen = !eyesOpen;
        eyesAnim.SetBool("IsOpenEyes", eyesOpen);

        print(eyesOpen);
        goExecute = true;
    }
}
