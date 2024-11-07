using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] walk,clownWalk;
    [SerializeField] private AudioSource audioSource;
    public bool isClown;

    public void FootStep()
    {
        var clip = isClown ? clownWalk : walk;
        var playClip = clip[Random.Range(0, clip.Length)];
        audioSource.PlayOneShot(playClip);
    }
}
