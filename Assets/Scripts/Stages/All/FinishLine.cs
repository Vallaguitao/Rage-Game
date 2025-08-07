using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;

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
            print("Triggered");
            NextStage();
        }
    }

    public void NextStage()
    {
        int currentIndex = gameManager.CurrentStageIndex;

        //insert clear stage text animation here

        currentIndex++;
        SceneManager.LoadScene(currentIndex);
    }
}
