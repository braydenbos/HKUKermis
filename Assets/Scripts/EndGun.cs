using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndGun : MonoBehaviour
{
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Vector3 bulletForce;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    
    private void OnEnable()
    {
        PlayerController.Instance.Animator.SetLayerWeight(1,0.9f);
        PlayerController.Instance.OnShoot += Shoot;
    }
    
    private void Shoot()
    {
        var clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.PlayOneShot(clip);
        var rotation = bulletSpawn.rotation;
        var newRotation = new Vector3(rotation.x-90, rotation.y, rotation.z);
        var newBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.Euler(newRotation));
        Destroy(newBullet,5f);
        var forward = bulletSpawn.forward;
        var force = new Vector3(bulletForce.x * forward.x, bulletForce.y * forward.y, bulletForce.z * forward.z);
        newBullet.AddForce(force);
    }
}
