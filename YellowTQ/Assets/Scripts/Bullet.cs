using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int _damage { get; private set; }
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _lifeDuration = 2f;
    private float _lifeTimer;

    private void Start()
    {
        _lifeTimer = _lifeDuration;
    }
    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0f)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() != null)
        {
            Enemy current = other.GetComponent<Enemy>();
            current.GetDamage(_damage);
            Destroy(gameObject);
        }
    }
    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}
