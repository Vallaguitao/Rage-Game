using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TimelineControls : MonoBehaviour
{

    [SerializeField] PlayableDirector timelineDirector;

    private void Awake()
    {
        timelineDirector = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        GameManager.gameManagerScript.playerControllerScript.CancelActionController.performed += cancelAnimation;
    }

    public void pauseTime()
    {
        Time.timeScale = 0f;
    }

    public void unPauseTime()
    {
        Time.timeScale = 1f;
        GameManager.gameManagerScript.playerControllerScript.CancelActionController.performed -= cancelAnimation;
    }

    private void cancelAnimation(InputAction.CallbackContext context)
    {
        double timelineDuration = timelineDirector.duration;
        timelineDirector.time = timelineDuration;

        GameManager.gameManagerScript.playerControllerScript.CancelActionController.performed -= cancelAnimation;
        print("Skip Intro and Unregister Cancel Action");
    }
}
