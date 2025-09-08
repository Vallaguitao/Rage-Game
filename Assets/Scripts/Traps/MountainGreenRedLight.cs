using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MountainGreenRedLight : TrapsCommonality
{

    [Header("Mountain Code")]
    [SerializeField] private float duration = 5f;
    [SerializeField] private float lookDistance = 1.5f;
    [SerializeField] private bool eyesOpen = true; //True: Green Light, False: Red Light
    [SerializeField] private bool lowerDuration;
    [SerializeField] private bool goExecute;
    [SerializeField] private Animator eyesAnim;
    [SerializeField] private int randomPosition;

    [SerializeField] private GameObject halfway;

    [SerializeField] private SpriteRenderer lookerSpriteRenderer;
    [Header("Player Code")]
    [SerializeField] private float playerInput;

    [Header("Cloud Code")]
    [SerializeField] private MovingClouds cloud;

    [Header("Spawn References")]
    [SerializeField] private List<GameObject> spawnLocations;

    protected override void Start()
    {

        base.Start();

        //InvokeRepeating("ChangeEyeStatus", duration, duration);

       // eyesAnim = GetComponent<Animator>();

        //eyesAnim.SetBool("IsOpenEyes", true);

        //cloud = GameObject.FindAnyObjectByType<MovingClouds>();
        lookerSpriteRenderer = GetComponent<SpriteRenderer>();
        lowerDuration = true;
        goExecute = true;
    }

    void Update()
    {
        playerInput = gameManagerScript.playerControllerScript.HorizontalInput;

        if(goExecute)
        {
            StartCoroutine(ChangeEyeStatus());
        }
        

        if (eyesOpen)
        {
            switch (randomPosition)
            {
                case 0:
                    lookerSpriteRenderer.sortingLayerID = SortingLayer.NameToID("Clouds");
                    transform.Translate(Vector3.up * lookDistance * Time.deltaTime);
                    break;
                case 1:
                    lookerSpriteRenderer.sortingLayerID = SortingLayer.NameToID("MountainFarFar");
                    transform.Translate(Vector3.left * lookDistance * Time.deltaTime);
                    break;
                case 2:
                case 4:
                    lookerSpriteRenderer.sortingLayerID = SortingLayer.NameToID("MountainFarFar");
                    transform.Translate(Vector3.up * lookDistance * Time.deltaTime);
                    break;
                case 3:
                    lookerSpriteRenderer.sortingLayerID = SortingLayer.NameToID("MountainFarFar");
                    transform.Translate(Vector3.right * lookDistance * Time.deltaTime);
                    break;
                case 5:
                    lookerSpriteRenderer.sortingLayerID = SortingLayer.NameToID("Default");
                    transform.Translate(Vector3.up * lookDistance * Time.deltaTime);
                    break;
                default:
                    transform.Translate(Vector3.left * lookDistance * Time.deltaTime);
                    break;

            }

            if (playerInput != 0) //former 1  -1
            {
                base.TrapsPlayerDied();
            }

        }

        /*
        if((cloud.MoveIt) && (lowerDuration))
        {
            duration -= 1f;
            print("MOVE ITTTTTTT");
            lowerDuration = false;
            
        }
        */
    }

    IEnumerator ChangeEyeStatus()
    {

        if(transform.position.x <= halfway.transform.position.x)
        {
            randomPosition = Random.Range(0, 3);
        }
        else
        {
            randomPosition = Random.Range(3, 6);
        }

        if(eyesOpen)
        {
            
            transform.position = new Vector2(spawnLocations[randomPosition].transform.position.x, spawnLocations[randomPosition].transform.position.y);
            transform.parent = spawnLocations[randomPosition].transform;
        }
        
        goExecute = false;
        yield return new WaitForSeconds(duration);

        eyesOpen = !eyesOpen;

        transform.position = spawnLocations[randomPosition].transform.position;
        //eyesAnim.SetBool("IsOpenEyes", eyesOpen);

        print(eyesOpen);
        goExecute = true;
    }
}
