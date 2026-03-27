using System.Collections.Generic;
using UnityEngine;

public class Locustmorph : Enemy
{
    [SerializeField] private List<Frames> frames;
    private int enemyVariant;
    private bool charging;

    protected override void OnEnable()
    {
        base.OnEnable();
        enemyVariant = Random.Range(0, frames.Count);
        EnterIdle();
    }

    private void Start()
    {
        destroyEffectPool = GameObject.Find("LocustPopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.locustHit;
        destroySound = AudioManager.Instance.locustDestroy;
    }

    protected override void Update()
    {
        base.Update();
        if (transform.position.y > 5 || transform.position.y < -5)
        {
            speedY *= -1;
        }
    }

    private void EnterIdle()
    {
        charging = false;
        spriteRenderer.sprite = frames[enemyVariant].sprites[0];
        speedX = Random.Range(0.1f, 0.5f);
        speedY = Random.Range(-0.9f, 0.9f);
    }
    private void EnterCharge()
    {
        if (!charging)
        {
            charging = true;
            spriteRenderer.sprite = frames[enemyVariant].sprites[1];
            AudioManager.Instance.PlaySound(AudioManager.Instance.locustCharge);
            speedX = -5f;
            speedY = 0;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (lives <= maxLives * 0.5)
        {
            EnterCharge();
        }
    }

    [System.Serializable]
    private class Frames
    {
        public Sprite[] sprites;
    }
}