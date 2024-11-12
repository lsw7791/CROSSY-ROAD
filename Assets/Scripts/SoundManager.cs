using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audioSource;

    public AudioClip jumpSound;
    public AudioClip collisionSound;
    public AudioClip backgroundMusic;
    public AudioClip cherrySound;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; 
            audioSource.Play(); 
        }
    }

    public void PlayJumpSound()
    {
            audioSource.PlayOneShot(jumpSound);
    }

    public void PlayCollisionSound()
    {
            audioSource.PlayOneShot(collisionSound);
    }

    public void PlayCherrySound()
    {
        audioSource.PlayOneShot(cherrySound);
    }
}
