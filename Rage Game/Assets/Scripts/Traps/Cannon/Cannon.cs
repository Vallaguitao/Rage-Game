using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cannon : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private float delayPerBullet;
    [SerializeField] protected float time = 0f;

    [Header("Cannon")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject cannonBulletPrefab;

    [Header("Game Objects")]
    [SerializeField] private FakeGroundTrigger fakeGroundTrigger;
    [SerializeField] private GameObject nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("FireCanon", 2f, delayPerBullet);
    }

    // Update is called once per frame
    void Update()
    {

        Attack();

        if(fakeGroundTrigger.IsTriggered)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition.transform.position, speed * Time.deltaTime);
            delayPerBullet = 0.5f;
        }
    }

    private void FireCanon()
    {
        Instantiate(cannonBulletPrefab, transform.position, transform.rotation);
    }

    protected virtual void Attack()
    {

        time += Time.deltaTime;

        if (time > delayPerBullet)
        {

            FireCanon();

            
            time = 0;
        }

    }
}
