using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerUp : MonoBehaviour
{

    [Header("Barrier")]
    [SerializeField] private GameObject playerBarrier;
    [SerializeField] private bool hasBarrier;
    [SerializeField] private float barrierDuration;

    // Start is called before the first frame update
    void Start()
    {
        playerBarrier.SetActive(false);

        barrierDuration = 0.3f;
        hasBarrier = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BarrierPowerUp"))
        {
            hasBarrier = true;
            Destroy(collision.gameObject);
        }
    }

    public void BarrierPowerUp(InputAction.CallbackContext context)
    {
        if (hasBarrier)
        {
            playerBarrier.SetActive(true);
            hasBarrier = false;

            StartCoroutine(BarrierDurationEnd());
            
        }
        else
        {
            print("Does not have power up");
        }

    }
    
    IEnumerator BarrierDurationEnd()
    {
        yield return new WaitForSeconds(barrierDuration);
        playerBarrier.SetActive(false);
    }
}
