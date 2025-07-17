using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainGreenRedLight : TrapsCommonality
{

    [SerializeField] private float duration = 5f;
    [SerializeField] private bool eyesOpen = true; //True: Green Light, False: Red Light

    [SerializeField] private PlayerController playerInput;
    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();

        playerInput = player.GetComponent<PlayerController>();

        InvokeRepeating("ChangeEyeStatus", duration, duration);
    }

    // Update is called once per frame
    void Update()
    {
        if (eyesOpen)
        {
            if ((playerInput.HorizontalInput == -1) || (playerInput.HorizontalInput == 1)) //do this to give margin of error but can be exploited by moving slight
            {
                base.PlayerDied();
            }
            
        }
    }

    private void ChangeEyeStatus()
    {
        eyesOpen = !eyesOpen;
        print(eyesOpen);
    }
}
