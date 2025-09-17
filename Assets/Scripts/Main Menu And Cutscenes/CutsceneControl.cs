using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneControl : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    //public Dialogue dialogue;
    public GameObject dialogueParent;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void StartTimeline()
    {
        director.time = director.time;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
    public void StopTimeline()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        
    }

    public void ResumeTimeline()
    {
        
    }
}
