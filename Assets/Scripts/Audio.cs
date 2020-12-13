using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;

    private AudioSource audioSource;

    [SerializeField] private AudioClip missSound;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip hitSound;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMissSound()
    {
        audioSource.PlayOneShot(missSound);
    }
    
    public void PlaySuccessSound()
    {
        audioSource.PlayOneShot(successSound);
    }

    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
}