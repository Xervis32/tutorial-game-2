using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource ice;
    public AudioSource fire;
    public AudioSource hit;
    public AudioSource pause;
    public AudioSource unpause;
    public AudioSource boom2;
    public AudioSource hitRock;
    public AudioSource shoot;
    public AudioSource critterZapped;
    public AudioSource critterBurned;
    public AudioSource hitArmor;
    public AudioSource bossCharge;
    public AudioSource bossSpawn;
    public AudioSource beetleHit;
    public AudioSource beetleDestroy;
    public AudioSource locustHit;
    public AudioSource locustDestroy;
    public AudioSource locustCharge;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }

    public void PlayModifiedSound(AudioSource sound)
    {
        sound.pitch = Random.Range(0.6f, 1.2f);
        sound.Stop();
        sound.Play();
    }
}