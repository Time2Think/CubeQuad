using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] 
    private AudioSource _audioSource;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _audioSource.Play();
    }
}
