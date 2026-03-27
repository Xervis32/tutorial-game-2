using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private FlashWhite flashWhite;
    protected ObjectPooler destroyEffectPool;

    [SerializeField] protected int lives;
    [SerializeField] protected int maxLives;
    [SerializeField] protected int damage;
    [SerializeField] protected int experienceToGive;

    protected AudioSource hitSound;
    protected AudioSource destroySound;

    protected float speedX = 0;
    protected float speedY = 0;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();
    }

    protected virtual void OnEnable()
    {
        lives = maxLives;
    }

    protected virtual void Update()
    {
        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player) player.TakeDamage(damage);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        lives -= damage;
        AudioManager.Instance.PlayModifiedSound(hitSound);
        if (lives > 0)
        {
            flashWhite.Flash();
        } else
        {
            flashWhite.Reset();
            AudioManager.Instance.PlaySound(destroySound);
            GameObject destroyEffect = destroyEffectPool.GetPooledObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.SetActive(true);

            PlayerController.Instance.GetExperience(experienceToGive);

            gameObject.SetActive(false);
        }
    }
}
