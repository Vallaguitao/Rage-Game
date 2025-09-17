using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BossState
{
    Idle,
    Attack,
    Move
}

public enum AttackPattern
{
    Attack1,
    Attack2,
    Attack3,
    Attack4,
    Attack5
}

public enum DirectionFacing
{
    Left,
    Right
}

public class Stage1Boss : MonoBehaviour
{
    [Header("Boss Enums")]
    [SerializeField] private BossState state;
    [SerializeField] private BossState statePrevious;
    [SerializeField] private AttackPattern attackPattern;
    [SerializeField] private DirectionFacing directionFacing;

    [Header("Boss Components")]
    [SerializeField] private SpriteRenderer bossRenderer;
    [SerializeField] private Rigidbody2D bossRb;

    [Header("Boss Stats")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float stateInterval;
    [SerializeField] private bool checker;
    [SerializeField] private bool toStart = false;
    [SerializeField] private bool toPause = false;
    [SerializeField] private int bossLives = 5;
    [SerializeField] private Slider bossLivesSlider;
    [SerializeField] private GameObject blackScreen;
    public bool ToStart { get { return toStart; } set { toStart = value; } }

    [Header("Barrier")]
    [SerializeField] private GameObject barrier;
    [SerializeField] private bool activateBarrier;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gunFire;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletObject;

    [Header("Attack Pattern 1")]
    [SerializeField] private int randomNumber;
    [SerializeField] private float newPosition;
    [SerializeField] private float positionOffset = 3f;
    [SerializeField] private float randomOffset;

    [Header("Player Information")]
    [SerializeField] private float playerDistance;
    [SerializeField] private Vector3 playerSize;
    [SerializeField] private float playerWidth;

    [Header("Attack Pattern 2")]
    [SerializeField] private GameObject van;
    [SerializeField] private SpriteRenderer vanRenderer;
    [SerializeField] private Van vanScript;
    [SerializeField] private float vanHeight;

    [Header("Attack Pattern 3")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float timeShots = 0f;

    [Header("Attack Pattern 4")]
    [SerializeField] private GameObject grenade;
    [SerializeField] private GameObject meteor;
    [SerializeField] private Rigidbody2D grenadeRigid;
    [SerializeField] private float throwForce  = 10f;

    void Start()
    {
        bossRenderer = GetComponent<SpriteRenderer>();
        bossRb = GetComponent<Rigidbody2D>();

        playerSize = GameManager.gameManagerScript.playerRenderer.bounds.size;
        playerWidth = playerSize.x;

        vanRenderer = van.GetComponent<SpriteRenderer>();
        vanHeight = vanRenderer.bounds.size.y;
        van.SetActive(false);

        grenadeRigid = grenade.GetComponent<Rigidbody2D>();

        checker = false;
        bossLivesSlider.maxValue = bossLives;
        bossLivesSlider.value = bossLives;
    }

    // Update is called once per frame
    void Update()
    {

        if(toStart && !toPause)
        {
            playerDistance = transform.InverseTransformPoint(GameManager.gameManagerScript.player.transform.position).x;

            //activatingBarrier(barrierRNG(0, 2));

            if (state == BossState.Attack)
            {
                switch (attackPattern)
                {
                    case AttackPattern.Attack1: //teleport then shoot

                        AttackPattern1();
                        break;

                    case AttackPattern.Attack2: //summon van

                        AttackPattern2();
                        break;

                    case AttackPattern.Attack3: //boss jump then fire gun

                        AttackPattern3();
                        break;

                    case AttackPattern.Attack4: //summon meteors

                        AttackPattern4();
                        break;

                    case AttackPattern.Attack5: //no more attack 5

                        AttackPattern1();
                        break;

                    default:
                        break;
                }
            }
            else if (state == BossState.Move)
            {
                Move();
            }
            else
            {
                Idle();
            }

            if (activateBarrier)
            {
                barrier.SetActive(true);
            }
            else
            {
                barrier.SetActive(false);
            }
        }
        else
        {
            return;
        }

    }

    #region Move and Idle

    private void Move()
    {
        if(!checker)
        {
            RandomDirectionFacing();
            checker = true;

            if (directionFacing == DirectionFacing.Right)
            {
                bossRenderer.flipX = false;
                speed = Mathf.Abs(speed);
            }
            else
            {
                bossRenderer.flipX = true;
                speed *= -1f;
            }
        }

        bossRb.velocity = new Vector2(speed, bossRb.velocity.y);

        stateInterval = 2f;

        StartCoroutine(MoveInterval());

    }

    IEnumerator MoveInterval()
    {
        
        yield return new WaitForSeconds(stateInterval);
        checker = false;

        statePrevious = BossState.Move;
        state = BossState.Idle;
    
    }

    private void Idle()
    {

        bossRb.velocity = Vector3.zero;
        CheckPlayerPosition();

        timeShots += Time.deltaTime;

        if (timeShots > stateInterval)
        {
            StartCoroutine(IdleInterval());
            timeShots = 0;
        }

        
        
    }

    IEnumerator IdleInterval()
    {

        yield return new WaitForSeconds(stateInterval);

        //to avoid changing the attackPattern when the boss just move and idle
        if (statePrevious == BossState.Move)
        {
            //do nothing
        }
        else
        {
            attackPattern = (AttackPattern)Random.Range(0, System.Enum.GetValues(typeof(AttackPattern)).Length);
        }
        

        
        print("Idle Interval");
        ChangeState();

        while (state == BossState.Idle)
        {
            ChangeState();
        }

        if((statePrevious == BossState.Move) && (state == BossState.Move))
        {
            print("Double Move");
            state = BossState.Attack;
        }

        statePrevious = BossState.Idle;

        yield return new WaitForSeconds(stateInterval);
    }

    private void ChangeState()
    {
        state = (BossState)Random.Range(0, System.Enum.GetValues(typeof(BossState)).Length);
    }

    #endregion

    #region Attack Patterns 
    private void AttackPattern1()
    {
        //directionFacing = (DirectionFacing)Random.Range(0, System.Enum.GetValues(typeof(DirectionFacing)).Length);

        RandomDirectionFacing();

        if (directionFacing == DirectionFacing.Right)
        {

            randomOffset = GameManager.gameManagerScript.player.transform.position.x - playerWidth - positionOffset;
            newPosition = Random.Range(randomOffset, randomOffset - positionOffset);
        }
        else
        {
            randomOffset = GameManager.gameManagerScript.player.transform.position.x + playerWidth + positionOffset;
            newPosition = Random.Range(randomOffset, randomOffset + positionOffset);
        }

        transform.position = new Vector2(newPosition, transform.position.y);

        //state = BossState.Idle;

        StartCoroutine(FireGun(directionFacing));

        state = BossState.Idle;
    }

    IEnumerator FireGun(DirectionFacing direction)
    {
        if (direction == DirectionFacing.Left)
        {
            bossRenderer.flipX = true;
            bulletObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            bossRenderer.flipX = false;
            bulletObject.transform.rotation = Quaternion.Euler(0, 0, 270);

        }

        yield return new WaitForSeconds(0.3f);
        Instantiate(bulletObject, transform.position, bulletObject.transform.rotation);

        audioSource.PlayOneShot(gunFire);

        stateInterval = 2f;
    }

    private void AttackPattern2()
    {
        //RandomDirectionFacing();

        directionFacing = (playerDistance < 0) ? DirectionFacing.Left : DirectionFacing.Right;

        transform.position = new Vector2(transform.position.x, transform.position.y + vanHeight + 0.10f);
        van.transform.position = new Vector2(transform.position.x, transform.position.y - vanHeight - 0.10f);

        van.SetActive(true);

        if (directionFacing == DirectionFacing.Right)
        {
            vanRenderer.flipX = false;
            vanScript.Speed = Mathf.Abs(vanScript.Speed);
        }
        else
        {
            vanRenderer.flipX = true;
            vanScript.Speed *= -1f;
        }

        state = BossState.Idle;
    }

    private void AttackPattern3()
    {

        Vector2 jumpDirection = Vector2.zero;

        if (playerDistance < 0)
        {
            jumpDirection = new Vector2(-1f, 1f);
        }
        else
        {
            jumpDirection = new Vector2(1f, 1f);
        }

        bossRb.constraints = RigidbodyConstraints2D.None;
        
        //jump left or right
        bossRb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);


        //rotate boss
        float rotationForce = (playerDistance < 0) ? 180f : -180f;

        transform.Rotate(0, 0, rotationForce * rotationSpeed * Time.deltaTime);

        //instantiate bullet

        timeShots += Time.deltaTime;

        if(timeShots > 0.025f)
        {
            
            var newBullet = Instantiate(bulletObject, transform.position, bulletObject.transform.rotation);
            
            if(playerDistance > 0)
            {
                newBullet.transform.Rotate(0, 0, 180f);
                print("new bullet if");
            }
            
            timeShots = 0;

        }

        StartCoroutine(FireGunRotating());

        
    }

    IEnumerator FireGunRotating()
    {
        yield return new WaitForSeconds(0.3f);

        bossRb.constraints = RigidbodyConstraints2D.FreezeRotation;

        transform.rotation = Quaternion.Euler(0, 0, 0);

        state = BossState.Idle;
    }

    private void AttackPattern4()
    {
        //instantiate/set actibve
        grenade.SetActive(true);

        Vector2 throwDirection = Vector2.zero;

        //add force
        if(playerDistance < 0)
        {
            throwDirection = new Vector2(-1f, 1f);
        }
        else
        {
            throwDirection = new Vector2(1f, 1f);
        }

        grenadeRigid.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

        //wait for seconds
        StartCoroutine(GrenadeDisappear());

        //disappear
        state = BossState.Idle;
    }

    IEnumerator GrenadeDisappear()
    {

        yield return new WaitForSeconds(1f);

        Instantiate(meteor, grenade.transform.position, meteor.transform.rotation);

        grenade.SetActive(false);
        grenade.transform.position = new Vector2(transform.position.x, transform.position.y + bossRenderer.bounds.size.y);
    }

    private void AttackPattern5()
    {
        //wait for seconds
        StartCoroutine(GrenadeDisappear());

        //disappear
        state = BossState.Idle;
    }

    #endregion

    private void RandomDirectionFacing()
    {
        directionFacing = (DirectionFacing)Random.Range(0, System.Enum.GetValues(typeof(DirectionFacing)).Length);
    }

    

    private void CheckPlayerPosition()
    {
        if(playerDistance > 0)
        {
            bossRenderer.flipX = false;
        }
        else
        {
            bossRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.gameManagerScript.PlayerDied();
        }
        else if(collision.gameObject.CompareTag("BarrierPowerUp"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BarrierPowerUp"))
        {
            if(toStart)
            {
                bossLives -= 1;
                bossLivesSlider.value = bossLives;
            }

            if(bossLives < 1)
            {
                blackScreen.gameObject.SetActive(true);
                blackScreen.GetComponent<Animator>().SetBool("IsFinish", true);
                Destroy(gameObject);
            }

            collision.gameObject.SetActive(false);
        }
    }

    public void bossPause()
    {
        toPause = !toPause;
    }
}


