using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{

    [Header("Barrier")]
    [SerializeField] private GameObject playerBarrier;
    [SerializeField] private bool hasBarrier;
    [SerializeField] private float barrierDuration;
    [SerializeField] private float time;

    // Start is called before the first frame update
    void Start()
    {
        playerBarrier.SetActive(false);
        time = 0f;

        barrierDuration = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;

        //BarrierPowerUp();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BarrierPowerUp"))
        {
            hasBarrier = true;
            Destroy(collision.gameObject);
        }
    }

    public void BarrierPowerUp()
    {
        if (hasBarrier)
        {
            playerBarrier.SetActive(true);
            hasBarrier = false;

            StartCoroutine(BarrierDurationEnd());
            //time = 0;
            
        }
        else
        {
            //print("Does not have power up");
        }

        /*
        if(time >= barrierDuration)
        {
            playerBarrier.SetActive(false);
        }
        */
    }
    
    IEnumerator BarrierDurationEnd()
    {
        yield return new WaitForSeconds(barrierDuration);
        playerBarrier.SetActive(false);
    }
}
