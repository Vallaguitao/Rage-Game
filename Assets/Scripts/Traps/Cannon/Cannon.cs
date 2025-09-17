using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Cannon : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private float delayPerBullet;
    [SerializeField] protected float time = 0f;

    [Header("Cannon")]
    [SerializeField] private float bulletOffest = -1f;

    [Header("Game Objects")]
    //[SerializeField] private GameObject nextPosition;

    [Header("References")]
    public SpriteRenderer canonRenderer;
    public Transform player;
    Vector2 positionWithOffset;

    [Header("Object Pooling")]
    public Cannon sharedInstance; //static removed
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    [Header("RayCast")]
    [SerializeField] protected Ray2D enemyRay;
    [SerializeField] protected RaycastHit2D[] colliderHit = new RaycastHit2D[1];
    [SerializeField] protected ContactFilter2D contactFilter2D;
    [SerializeField] protected int numberOfHits;
    [SerializeField] protected float raycastDistance = 20f;

    private void Awake()
    {
        sharedInstance = this;
    }
    void Start()
    {
        canonRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Transform>();

        ObjectPooling();
    }

    // Update is called once per frame
    void Update()
    {

        numberOfHits = RaycastFunction();

        if (numberOfHits > 0)
        {
            Attack();
        }

    }

    private void FireCanon()
    {
        if (canonRenderer.flipX)
        {
            positionWithOffset = new Vector2(transform.position.x - bulletOffest, transform.position.y);
        }
        else
        {
            positionWithOffset = new Vector2(transform.position.x + bulletOffest, transform.position.y);
        }
        
        //Instantiate(cannonBulletPrefab, positionWithOffset, transform.rotation);

        GameObject bullet = sharedInstance.GetPooledObject(); //formerly Cannon.SharedInstance
        if (bullet != null)
        {
            bullet.transform.position = positionWithOffset;
            bullet.SetActive(true);
        }
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

    private void ObjectPooling()
    {
        pooledObjects = new List<GameObject>();
        GameObject cannonBullet;

        for (int counter = 0; counter < amountToPool; counter++)
        {
            cannonBullet = Instantiate(objectToPool);
            cannonBullet.SetActive(false);
            cannonBullet.transform.SetParent(this.transform);
            pooledObjects.Add(cannonBullet);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int counter = 0; counter < amountToPool; counter++)
        {
            if (!pooledObjects[counter].activeInHierarchy)
            {
                return pooledObjects[counter];
            }
        }
        return null;
    }

    private int RaycastFunction()
    {
        Vector2 direction = (canonRenderer.flipX == true) ? Vector2.right : Vector2.left;

        int numberOfDetectedPlayer = Physics2D.Raycast(transform.position, direction, contactFilter2D, colliderHit, raycastDistance);
    
        return numberOfDetectedPlayer;
    }

    
    protected virtual void OnDrawGizmos()
    {

        if(canonRenderer.flipX == true)
        {
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + raycastDistance, transform.position.y, transform.position.z));
        }
        else
        {
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - raycastDistance, transform.position.y, transform.position.z));
        }


    }
}
