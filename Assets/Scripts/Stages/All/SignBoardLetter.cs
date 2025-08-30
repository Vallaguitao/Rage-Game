using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class SignBoardLetter : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputReference;
    [SerializeField] private InputActionReference actionReference;
    [SerializeField] private TextMeshProUGUI buttonToPress;

    // Start is called before the first frame update
    void Start()
    {
        //InputBinding binding = actionReference.action.GetBindingIndex;
        //buttonToPress.text = actionReference.action.bindings[0].path.ToString();

        print(inputReference.controlSchemes.ToString());

        //Self made solution (only works for keyboard for now)
        //get the control scheme and the button -> output: <Keyboard>/button
        string controlScheme = buttonToPress.text = actionReference.action.bindings[0].path.ToString();
        int lastIndexToRemove = controlScheme.IndexOf('/');

        //To get the binded button, remove the first part (<Keyboard>/)
        controlScheme = controlScheme.Remove(0, lastIndexToRemove + 1);


        //.group returns the Control Scheme -> output: Keyboard
        if (actionReference.action.bindings[0].groups.ToString().ToUpper() == "Keyboard".ToUpper())
        {
            buttonToPress.text = controlScheme.ToUpper();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
