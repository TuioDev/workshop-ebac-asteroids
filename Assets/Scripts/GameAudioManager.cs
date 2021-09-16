using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip laserSound;
    [SerializeField] private AudioClip asteroidExplosionSound;
    [SerializeField] private AudioClip playerExplosionSound;

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void PlaySound(string audioName)
    {
        switch (audioName)
        {
            case "laserSound":
                _audioSource.PlayOneShot(laserSound);
                break;
            case "asteroidExplosionSound":
                _audioSource.PlayOneShot(asteroidExplosionSound);
                break;
            case "playerExplosionSound":
                _audioSource.PlayOneShot(playerExplosionSound);
                break;
            default:
                break;
        }
    }
}
