using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2SignBoard : MonoBehaviour
{

    [SerializeField] private float stayForSeconds = 2f;
    [SerializeField] private float timeStayed = 0;
    [SerializeField] private bool startTimer = false;
    [SerializeField] private GameObject removeThisGround;

    // Start is called before the first frame update
    void Start()
    {
        removeThisGround = GameObject.Find("Remove Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer)
        {
            timeStayed += Time.deltaTime;
            print(timeStayed);
        }

        if(timeStayed >= stayForSeconds)
        {
            removeThisGround.SetActive(false);
            startTimer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startTimer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startTimer = false;
            timeStayed = 0f;
        }
    }
}
