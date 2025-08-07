using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainGreenRedLight : TrapsCommonality
{

    [Header("Mountain Code")]
    [SerializeField] private float duration = 5f;
    [SerializeField] private bool eyesOpen = true; //True: Green Light, False: Red Light
    [SerializeField] private Animator eyesAnim;

    [Header("Player Code")]
    [SerializeField] private float playerInput;
    
    protected override void Start()
    {

        base.Start();

        InvokeRepeating("ChangeEyeStatus", duration, duration);

        eyesAnim = GetComponent<Animator>();

        eyesAnim.SetBool("IsOpenEyes", true);
    }

    void Update()
    {
        playerInput = gameManagerScript.playerControllerScript.HorizontalInput;

        if (eyesOpen)
        {
            if ((playerInput == -1) || (playerInput == 1)) //do this to give margin of error but can be exploited by moving slight
            {
                base.TrapsPlayerDied();
            }

        }
    }

    private void ChangeEyeStatus()
    {
        eyesOpen = !eyesOpen;
        eyesAnim.SetBool("IsOpenEyes", eyesOpen);
        print(eyesOpen);
    }
}
