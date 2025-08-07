using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneControl : MonoBehaviour
{
    private PlayableDirector director;

    public Dialogue dialogue;
    public int[] indexToPause = new int[5];
    // Start is called before the first frame update
    void Start()
    {
        indexToPause[0] = 0;
        indexToPause[1] = 2;
    }

    // Update is called once per frame
    public void Update()
    {
        if (dialogue.dialogueText.text.Equals(dialogue.dialogueStorage[0]))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartTimeline();
            }
        }
        if (dialogue.dialogueText.text.Equals(dialogue.dialogueStorage[2]))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartTimeline();
            }
        }
    }

    private void Awake()
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
