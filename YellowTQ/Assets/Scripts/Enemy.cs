using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    enum Stage {
        idle,
        attacking
    }

    public delegate void HpChange(int value);
    public event HpChange OnHpChanged;

    public delegate void DeadObject();
    public event DeadObject OnDead;

    [SerializeField] protected uint _damage;
    [SerializeField] private uint _killScore;
    [SerializeField] private float _checkPlayerRadius;
    [SerializeField] LayerMask _playerMask;
    private Stage _stage = Stage.idle;
    private PlayerStat _player;

    private Vector3 _centerPosition;
    [SerializeField] private float _moveSpeed;
    public float _angle = 0;
    public float _idleMoveRadius = 0.5f;
    private bool _CanAttack = true;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<PlayerStat>();
        if(_centerPosition == null)
            _centerPosition = gameObject.transform.position;

    }

    private void Update()
    {
        CheckForPlayer();

        switch (_stage)
        {
            case Stage.idle:
                Idle();
                break;
            case Stage.attacking:
                Attacking();
                break;
        }
    }

    private void CheckForPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _checkPlayerRadius, _playerMask);
        if (hitColliders.Length == 0)
            _stage = Stage.idle;
        else
            _stage = Stage.attacking;
    }
    private void Idle()
    {
        _angle += Time.deltaTime;

        var x = Mathf.Cos(_angle * _moveSpeed) * _idleMoveRadius;
        var z = Mathf.Sin(_angle * _moveSpeed) * _idleMoveRadius;

        Vector3 target  = new Vector3(x, 0, z) + _centerPosition;

        transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);

    }

    private void Attacking()
    {
        Vector3 target = _player.gameObject.transform.position;

        if (Vector3.Distance(transform.position, target) <= 1.5f)
        {
            if (_CanAttack)
            {
                print("Attack");
                _CanAttack = false;
                _player.GetDamage(_damage);
                StartCoroutine(DoCanAttackTrue(1.5f));
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 0.05f);
        }
    }

    private IEnumerator DoCanAttackTrue(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _CanAttack = true;
    }

    public void SetStartOptions(Vector3 centerPosition, float radius, float angle)
    {
        _centerPosition = centerPosition;
        _idleMoveRadius = radius;
        _angle = angle;
    }

    public void GetDamage(int value)
    {
        _hp -= value;

        if (_hp <= 0)
            Dead();

        OnHpChanged?.Invoke(_hp);
    }

    private void Dead()
    {
        GameManager.Instance.AddScore(_killScore);
        OnDead?.Invoke();
        Destroy(gameObject);
    }
}
