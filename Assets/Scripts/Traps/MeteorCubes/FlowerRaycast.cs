using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerRaycast : MonoBehaviour
{

    [Header("Meteor Details")]
    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private GameObject meteors;
    [SerializeField] private float interval;
    [SerializeField] protected float time = 0f;

    [Header("Sound Details")]
    [SerializeField] protected AudioManager audioManager;
    [SerializeField] protected AudioClip sound;

    [Header("RayCast")]
    [SerializeField] protected Ray2D enemyRay;
    [SerializeField] protected RaycastHit2D[] colliderHit = new RaycastHit2D[1];
    [SerializeField] protected ContactFilter2D contactFilter2D;
    [SerializeField] protected int numberOfHits;
    [SerializeField] private float circleRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int playerDetected = DetectPlayer();

        if(playerDetected > 0)
        {
            SpawnMeteors();
        }
        else
        {
            return;
        }
    }

    protected virtual void SpawnMeteors()
    {
        time += Time.deltaTime;

        if (time > interval)
        {
            Instantiate(meteors, spawnLocation.transform);
            PlaySound();
            time = 0;
        }


    }

    protected virtual int DetectPlayer()
    {
        int numberOfDetectedPlayer = Physics2D.CircleCast(transform.position, circleRadius, Vector2.zero ,contactFilter2D, colliderHit);

        return numberOfDetectedPlayer;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleRadius);


    }

    private void PlaySound()
    {
        audioManager.PlaySFX(sound);
    }
}
