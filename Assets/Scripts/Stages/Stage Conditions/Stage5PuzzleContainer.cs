using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stage5PuzzleContainer : MonoBehaviour
{

    [SerializeField] private int[] pressedButtonOrder = new int[5];
    [SerializeField] private int currentIndex;
    [SerializeField] private int pattern;
    [SerializeField] private string patternText;
    [SerializeField] private bool isFinished;
    [SerializeField] private bool isResetted;
    [SerializeField] private UnityEvent on231;
    [SerializeField] private UnityEvent onReset;

    [SerializeField] private GameObject firstFloor;
    [SerializeField] private GameObject secondFloor;
    [SerializeField] private GameObject secondFloorLast;
    [SerializeField] private GameObject thirdFloor;
    [SerializeField] private GameObject trapsFinishLine;

    // Start is called before the first frame update
    void Start()
    {

        currentIndex = 0;
        isFinished = false;
        isResetted = false;
        trapsFinishLine.SetActive(false);
    }

    private void Update()
    {
        if((currentIndex == 5) && (!isFinished))
        {
            for(int counter = 0; counter < 3; counter++)
            {
                if (pressedButtonOrder[counter] > 3)
                {
                    print("Boom");
                }

                patternText += pressedButtonOrder[counter].ToString();
            }

            int.TryParse(patternText, out pattern);

            print(pattern);

            switch (pattern)
            {
                case 123:
                    firstFloor.SetActive(false);
                    print("Deadend 1st flr");
                    break;
                case 132:
                    firstFloor.SetActive(false);
                    print("Deadend 1st flr");
                    break;
                case 213:
                    secondFloor.SetActive(false);
                    secondFloorLast.SetActive(false);
                    print("Correct Way");
                    break;
                case 231:
                    //secondFloor.SetActive(false);
                    on231?.Invoke();
                    print("Stuck 2nd flr");
                    break;
                case 321:
                    secondFloor.SetActive(false);
                    secondFloorLast.SetActive(false);
                    print("Correct Way with Traps");
                    trapsFinishLine.SetActive(true);
                    break;
                case 312:
                    secondFloor.SetActive(false);
                    thirdFloor.SetActive(false);
                    print("Deadend 3rd flr");
                    break;
                default:
                    print("Boom");
                    break;
            }

            isFinished = true;
        }

        PlayerDiedStageReset();

    }

    public void Button1Pressed()
    {
        pressedButtonOrder[currentIndex] = 1;
        currentIndex++;
    }

    public void Button2Pressed()
    {
        pressedButtonOrder[currentIndex] = 2;
        currentIndex++;
    }

    public void Button3Pressed()
    {
        pressedButtonOrder[currentIndex] = 3;
        currentIndex++;
    }

    public void Button4Pressed()
    {
        pressedButtonOrder[currentIndex] = 4;
        currentIndex++;
    }

    public void Button5Pressed()
    {
        pressedButtonOrder[currentIndex] = 5;
        currentIndex++;
    }

    private void PlayerDiedStageReset()
    {

        if(!isResetted)
        {
            if (GameManager.gameManagerScript.isInvincibleState)
            {
                print("reset action");

                //remove the button pressed order
                Array.Clear(pressedButtonOrder, 0, 5);
                currentIndex = 0;

                onReset?.Invoke();
                isResetted = true;
            }
        }
        else
        {
            if(!GameManager.gameManagerScript.isInvincibleState)
            {
                isResetted = false;
                isFinished = false;
                patternText = string.Empty;
                print("reset = false");
            }
        }
        
    }

}
