using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuSelected : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelectedObject;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(defaultSelectedObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(defaultSelectedObject);
        }        
    }
}
