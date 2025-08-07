using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenuProceed : MonoBehaviour
{

    public UnityEvent onPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.T)))
        {
            onPress.Invoke();
        }
    }

    public void InMaineMenu()
    {
        SceneManager.LoadScene("Tutorial Stage");
    }

}
