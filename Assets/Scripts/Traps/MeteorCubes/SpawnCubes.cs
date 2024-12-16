using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{

    [SerializeField] private GameObject meteors;
    [SerializeField] private float interval;

    private AudioManager audioManager;
    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {

        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
        InvokeRepeating("SpawnMeteors", interval, interval);
        InvokeRepeating("PlaySound", interval, interval);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMeteors()
    {
        Instantiate(meteors, gameObject.transform);
    }

    private void PlaySound()
    {
        audioManager.PlaySFX(sound);
    }
}
