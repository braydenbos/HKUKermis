using System;
using System.Collections;
using Ferr;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Speed = Animator.StringToHash("Speed");
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerSound sound;
    public Animator Animator => animator;
    public Action OnWalk;
    
    [SerializeField] private MinMax<Vector2> bounds;
    
    private GameBooth _hitBooth;
    private bool _inGame;
    
    public Action OnShoot;
    private bool _canShoot = true;
    [SerializeField] private float reloadTime;

    private bool _isClown;
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && _hitBooth && !_inGame)
        {
            animator.SetBool(IsWalking,false);
            GameManager.Instance.EnterGame(_hitBooth);
        }

        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            StartCoroutine(Reload());
            OnShoot?.Invoke();
        }
            
        
        if (_inGame) return;
        if(Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0) 
            MovePlayer(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        else
        {
            animator.SetBool(IsWalking,false);
        }

    }

    private IEnumerator Reload()
    {
        _canShoot = false;
        var time = 0f;
        while (time < reloadTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        _canShoot = true;
    }

    private void MovePlayer(float horizontal, float vertical)
    {
        OnWalk?.Invoke();
        animator.SetBool(IsWalking,true);
        
        var movement = new Vector3(horizontal,0,vertical);
        var normal = movement.normalized;
        transform.position += normal * (moveSpeed * Time.deltaTime);

        var toRotation = Quaternion.LookRotation(normal, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        
        if (transform.position.x < bounds.min.x) transform.position = new Vector3(bounds.min.x,transform.position.y,transform.position.z);
        else if (transform.position.x > bounds.max.x) transform.position = new Vector3(bounds.max.x, transform.position.y,transform.position.z);
        if (transform.position.z < bounds.min.y) transform.position = new Vector3(transform.position.x,transform.position.y,bounds.min.y);
        else if (transform.position.z > bounds.max.y) transform.position = new Vector3(transform.position.x,transform.position.y,bounds.max.y);
    }
    public void SetBooth(GameBooth booth) => _hitBooth = booth;
    public GameBooth GetBooth() => _hitBooth;
    
    public void SetCanInteract(bool canInteract) => _inGame = canInteract;

    public void Upgrade(float speedMultiplier, float reloadsSpeedMultiplier, bool clownNoise)
    {
        moveSpeed *= speedMultiplier;
        animator.SetFloat(Speed,animator.GetFloat(Speed) * speedMultiplier);
        reloadTime *= reloadsSpeedMultiplier;
        _isClown = clownNoise || _isClown;
        sound.isClown = _isClown;
    }
}
