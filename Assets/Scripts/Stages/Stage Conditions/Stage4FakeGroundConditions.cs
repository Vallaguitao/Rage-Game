using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4FakeGroundConditions : MonoBehaviour
{

    [SerializeField] private GameObject FakeGround1;
    [SerializeField] private GameObject FakeGround2;
    [SerializeField] private GameObject FakeGround3;
    [SerializeField] private GameObject movingWall;

    [SerializeField] private bool isButtonPressed;
    [SerializeField] private bool isGround2Triggered;

    // Start is called before the first frame update
    void Start()
    {
        isButtonPressed = false;
        FakeGround2.SetActive(false);
        movingWall.SetActive(false);
    }

    private void Update()
    {

        if(movingWall != null) 
        {
            if (movingWall.activeSelf)
            {
                movingWall.transform.position += Vector3.right * 5f * Time.deltaTime;

                if (movingWall.transform.position.x > 85f)
                {
                    Destroy(movingWall);
                }
            }
        }
        
    }

    public void Fake1Condition()
    {
        
        FakeGround1.SetActive(false);
    }

    public void Fake2Condition()
    {
        FakeGround2.SetActive(true);
        FakeGround3.SetActive(false);

        movingWall.SetActive(true);
        
    }
}
