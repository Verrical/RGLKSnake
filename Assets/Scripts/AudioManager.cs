using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public float normalVolume = 1f;
    public float pausedVolume = 0.2f;
    public float volumeTransitionSpeed = 3f;
    private float targetVolume;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        targetVolume = normalVolume;
        audioSource.volume = normalVolume;
    }

    void Update()
    {
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, 
            Time.unscaledDeltaTime * volumeTransitionSpeed);
    }

    public void OnPause()
    {
        targetVolume = pausedVolume;
    }

    public void OnResume()
    {
        targetVolume = normalVolume;
    }

    public void OnGameOver()
    {
        targetVolume = 0f;
    }
}