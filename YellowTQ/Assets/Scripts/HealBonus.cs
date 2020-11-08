using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBonus : MonoBehaviour
{
    [SerializeField] private uint _healValue;
    [SerializeField] private float _lifeDuration = 2f;
    private float _lifeTimer;

    private void Start()
    {
        _lifeTimer = _lifeDuration;
    }
    private void Update()
    {
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.GetComponent<PlayerStat>() != null)
        {
            collision.GetComponent<PlayerStat>().Heal(_healValue);
            Destroy(gameObject);
        }
    }
}
