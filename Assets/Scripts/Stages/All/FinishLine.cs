using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;

    [SerializeField] private UnityEvent OnFinishLine;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            NextStage();
            OnFinishLine?.Invoke();
        }
    }

    public void NextStage()
    {
        int currentIndex = gameManager.CurrentStageIndex;

        //insert clear stage text animation here

        currentIndex++;
    }
}
