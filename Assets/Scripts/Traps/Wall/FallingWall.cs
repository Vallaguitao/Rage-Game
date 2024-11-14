using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWall : MonoBehaviour
{

    private Animator animator;
    [SerializeField] FallingTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger.isTriggered)
        {
            animator.SetBool("PlayerPass", true);
        }
    }

    
}
