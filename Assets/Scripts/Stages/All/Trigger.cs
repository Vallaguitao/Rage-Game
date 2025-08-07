using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{

    [Header("For All (Formerly for buttons in stage 5)")]
    public UnityEvent onTrigger;
    [SerializeField] private Animator anim;
    [SerializeField] private bool isTriggered;

    [Header("For Signboard Details")]
    public UnityEvent onInteract;
    [SerializeField] private GameObject instruction;
    [SerializeField] private bool inRange;
    public UnityEvent onLeaveTrigger;


    private void Start()
    {
        //anim = GetComponent<Animator>();
        isTriggered = false;
        inRange = false;
    }

    private void Update()
    {
        if (instruction != null)
        {
            if(inRange)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    onInteract.Invoke();
                }
            }
            
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        inRange = true;

        if (isTriggered)
        {
            return;
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                onTrigger.Invoke();

            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onLeaveTrigger.Invoke();
        }

        inRange = false;
    }


    public void PlayAnimTrue(string animNameState)
    {
        anim.SetBool(animNameState, true);
        isTriggered = true;
    }

    public void PlayAnimFalse(string animNameState)
    {
        anim.SetBool(animNameState, false);
        isTriggered = false;
    }
}
