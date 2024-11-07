using UnityEngine;
using UnityEngine.Serialization;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    public void SetSpeed(float newSpeed) => speed = newSpeed;
    private void Update() => transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    
}