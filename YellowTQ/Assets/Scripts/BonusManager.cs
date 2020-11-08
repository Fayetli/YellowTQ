using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private GameObject _bonusPrefab;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _range;

    private void Start()
    {
        StartCoroutine(SpawningBonus(_waitTime));
    }

    private IEnumerator SpawningBonus(float waitTime)
    {
        while (true)
        {
            SpawnBonus(_bonusPrefab);
            SpawnBonus(_bonusPrefab);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnBonus(GameObject bonus)
    {
        Vector3 position = new Vector3(Random.Range(-_range, _range), 0, Random.Range(-_range, _range));
        Instantiate(_bonusPrefab, transform.position + position, Quaternion.identity);
    }
}
