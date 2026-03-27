using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    PhaserWeapon weapon;
    private void Start()
    {
        weapon = PhaserWeapon.Instance;
    }
    void Update()
    {
        transform.position += new Vector3(weapon.stats[weapon.weaponLevel].speed * Time.deltaTime, 0f);
        if (transform.position.x > 9)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid) asteroid.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
            gameObject.SetActive(false);
        } else if (collision.gameObject.CompareTag("Critter"))
        {
            gameObject.SetActive(false);
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy) enemy.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
            gameObject.SetActive(false);
        }
    }
}
