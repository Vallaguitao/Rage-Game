using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RotateGround : MonoBehaviour
{

    [SerializeField] private float interval;
    [SerializeField] private float time;
    [SerializeField] private float targetRotation;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float increment;
    [SerializeField] private float rightRotationStorage;
    [SerializeField] private Direction direction;

    void Start()
    {
        time = 0;

        //Left -> Positive
        switch (direction)
        {
            case Direction.Left:
                increment = 90f;
                targetRotation = 90f;
                break;
            case Direction.Right:
                increment = 90f;
                targetRotation = -270f;
                speed *= -1f;
                rightRotationStorage = -increment;
                transform.rotation = Quaternion.Euler(0f, 0f, 359.99f); //359.9
                break;
            default:
                print("Direction not available");
                break;

        }
        
    }

    void Update()
    {
        //print(transform.eulerAngles.z);
        time += Time.deltaTime;

        if (time >= interval)
        {

            if(direction == Direction.Left)
            {
                Left();
            }
            else
            {
                Right();
            }

        }
    }

    private void Left()
    {
        if (transform.eulerAngles.z < targetRotation)
        {

            if (transform.eulerAngles.z >= 359f)
            {
                targetRotation = 0;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            transform.Rotate(0, 0, speed * Time.deltaTime);

        }
        else
        {

            transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);

            targetRotation += increment;
            time = 0;


        }
    }

    private void Right()
    {

        if (transform.eulerAngles.z > Mathf.Abs(targetRotation)) //359 > 0
        {

            if (transform.eulerAngles.z <= 1.50f)
            {
                targetRotation = -270f;
                transform.rotation = Quaternion.Euler(0f, 0f, 359.99f);
                rightRotationStorage = -increment;
                time = 0;
            }

            transform.Rotate(0, 0, speed * Time.deltaTime);

        }
        else
        {


            transform.rotation = Quaternion.Euler(0f, 0f, rightRotationStorage);

            targetRotation += increment;
            rightRotationStorage += -increment;



            time = 0;


        }
    }
}
