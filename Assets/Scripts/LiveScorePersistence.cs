using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiveScorePersistence : MonoBehaviour
{

    public static LiveScorePersistence liveScorePersistence;
    public int persistentLives;
    public int persistentScore;

    private void Awake()
    {
        

        if(liveScorePersistence == null)
        {
            liveScorePersistence = this;
            DontDestroyOnLoad(liveScorePersistence);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
    }

    void Start()
    {
        persistentLives = 10;
        persistentScore = 0;

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene currentScene, Scene nextScene)
    {
        if(nextScene.name == "Tutorial Stage")
        {
            persistentLives = 10;
            persistentScore = 0;
        }
        else
        {
            print("Persistence here");
        }

        Debug.Log("Scenes: " + currentScene + ", " + nextScene.name);
    }
}
