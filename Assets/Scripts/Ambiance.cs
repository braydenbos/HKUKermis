using System.Collections;
using System.Collections.Generic;
using Ferr;
using UnityEngine;

public class Ambiance : Singleton<Ambiance>
{
    [SerializeField] private AudioSource ambianceSound;
    
    public void SetVolume(float volume) => ambianceSound.volume = volume;
}
