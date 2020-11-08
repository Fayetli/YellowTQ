using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemy : MonoBehaviour
{
    [SerializeField] private float _spawnRadius;
    [SerializeField] private List<GameObject> _enemys;
    [SerializeField] public int Count { get; private set; }
    [SerializeField] public FightManager _manager { private get; set; }

    private void Awake()
    {
        Count = _enemys.Count;
    }
    public void Spawn()
    {
        float step = 360 / _enemys.Count;
        for (int i = 0; i < _enemys.Count; i++)
        {
            float angle = step * i;
            var x = Mathf.Cos(angle) * _spawnRadius;
            var z = Mathf.Sin(angle) * _spawnRadius;

            GameObject current = Instantiate(_enemys[i], transform);
            current.transform.position = new Vector3(x, transform.position.y, z);
            Enemy enemy = current.GetComponent<Enemy>();
            enemy.SetStartOptions(transform.position, _spawnRadius, angle);

            enemy.OnDead += CheckForKillAll;
        }
    }

    private void CheckForKillAll()
    {
        Count--;
        if (Count == 0)
        {
            _manager.SpawnNewEnemy();
        }
    }
}
