using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down, 
    Left, 
    Right
}

public enum TrapActivate
{
    Yes,
    No
}
public class SpawnBird : MonoBehaviour
{

    [SerializeField] private GameObject bird;
    [SerializeField] private GameObject birdTrap;
    [SerializeField] private GameObject birdie;

    [SerializeField] private float highest;
    [SerializeField] private float lowest;
    [SerializeField] private float interval = 5f;

    [SerializeField] private Direction direction;
    [SerializeField] private TrapActivate trapActivate;
    [SerializeField] private int randomNumber;

    public Direction Direction { get { return direction; } }

    // Start is called before the first frame update
    void Start()
    {
        //Random enum state : direction = (Direction)Random.Range(0, 3);
        InvokeRepeating("SpawnBirdies", interval, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnBirdies()
    {
        if(!GameManager.gameManagerScript.isPaused)
        {
            if(trapActivate == TrapActivate.Yes)
            {

                randomNumber = RandomNumber();

                switch(randomNumber)
                {
                    case 0:
                        birdie = Instantiate(bird, RandomPosition(), transform.rotation);
                        break;
                    case 1:
                        birdie = Instantiate(birdTrap, RandomPosition(), transform.rotation);
                        break;
                    default:
                        break;
                }

                birdie.transform.parent = transform;
            }
            else
            {
                birdie = Instantiate(bird, RandomPosition(), transform.rotation);
                birdie.transform.parent = transform;
            }
        }
    }

    private int RandomNumber()
    {
        randomNumber = Random.Range(0, 2);
        return randomNumber;
    }
    private Vector2 RandomPosition()
    {
        Vector2 position = new Vector2(transform.position.x ,Random.Range(lowest, highest));
        return position;
    }
}
