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
    [SerializeField] private FakeGroundTrigger cannonMoveTrigger;
    [SerializeField] private GameObject nextPosition;

    public SpriteRenderer canonRenderer;

    // Start is called before the first frame update
    void Start()
    {
        canonRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        Attack();

        if(cannonMoveTrigger.IsTriggered)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition.transform.position, speed * Time.deltaTime);
            delayPerBullet = 0.5f;
            cannonMoveTrigger.gameObject.SetActive(false);

            if (!canonRenderer.isVisible && transform.position == nextPosition.transform.position)
            {
                canonRenderer.flipX = true;
            }
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
