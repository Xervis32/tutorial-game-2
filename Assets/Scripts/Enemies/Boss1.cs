using UnityEngine;

public class Boss1 : Enemy
{
    private Animator animator;
    private bool charging;

    private float switchInterval;
    private float switchTimer;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EnterChargeState();
        AudioManager.Instance.PlaySound(AudioManager.Instance.bossSpawn);
    }

    protected void Start()
    {
        destroyEffectPool = GameObject.Find("Boom3Pool").GetComponent<ObjectPooler>();
        EnterChargeState();
        hitSound = AudioManager.Instance.hitArmor;
        destroySound = AudioManager.Instance.boom2;
        AudioManager.Instance.PlaySound(AudioManager.Instance.bossSpawn);
    }


    protected override void Update()
    {
        base.Update();
        float playerPosition = PlayerController.Instance.transform.position.x;

        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
        } else
        {
            if (charging && transform.position.x > playerPosition)
            {
                EnterPatrolState();
            } else
            {
                EnterChargeState();
            }
        }

        if (transform.position.y > 3 || transform.position.y < -3)
        {
            speedY *= -1;
        } else if (transform.position.x < playerPosition)
        {
            EnterChargeState();
        }

        bool boost = PlayerController.Instance.boosting;
        float moveX;
        if (boost && !charging)
        {
            moveX = GameManager.Instance.worldSpeed * Time.deltaTime * -0.5f;
        } else
        {
            moveX = speedX * Time.deltaTime;
        }

        float moveY = speedY * Time.deltaTime;
        transform.position += new Vector3(moveX, moveY);
        if (transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }

    }
    void EnterPatrolState()
    {
        speedX = 0;
        speedY = Random.Range(-1f, 1f);
        switchInterval = Random.Range(3f, 10f);
        switchTimer = switchInterval;
        charging = false;
        animator.SetBool("charging", false);
    }

    void EnterChargeState()
    {
        if (!charging)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.bossCharge);
        }
        speedX = -5f;
        speedY = 0;
        switchInterval = Random.Range(0.5f, 1.5f);
        switchTimer = switchInterval;
        charging = true;
        animator.SetBool("charging", true);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid) asteroid.TakeDamage(damage);
        }
    }

}
