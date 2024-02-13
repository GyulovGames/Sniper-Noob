using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zombie : MonoBehaviour
{
    public static UnityEvent ZombieHitEvent = new UnityEvent();

    [SerializeField] private AudioSource audioSource;
    [Space(15)]
    [SerializeField] private AudioClip[] zombieHitSounds;


    private bool isHite = false;


    public void Hit()
    {
        if (!isHite)
        {
            isHite = true;
            ZombieHitEvent.Invoke();

            int clipIndex = Random.Range(0, zombieHitSounds.Length);
            audioSource.clip  = zombieHitSounds[clipIndex];
            audioSource.Play();
        }
    }
}