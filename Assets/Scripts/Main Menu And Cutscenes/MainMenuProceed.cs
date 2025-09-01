using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuProceed : MonoBehaviour
{

    public UnityEvent onPress;
    [SerializeField] private PlayerInputHandler playerInputHandlerScript;


    private void Awake()
    {
        playerInputHandlerScript = GameObject.Find("Player Controller").GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInputHandlerScript != null)
        {
            playerInputHandlerScript.cancelInput.performed += context => Cancel();
        }
        else
        {
            print("NO CANCELLLL BUTTONNNN");
        }

    }

    public void InMaineMenu()
    {
        GameManager.gameManagerScript.LoadNextLevel();
        SceneManager.LoadScene("Main Menu");
    }

    public void Cancel()
    {
        onPress.Invoke();
    }

}
