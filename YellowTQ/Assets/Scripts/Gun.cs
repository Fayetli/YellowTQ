using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private int _damage = 3;
    [SerializeField] private GameObject _bulletPrefab;
    private void Start()
    {
        GameObject.FindGameObjectWithTag("AttackButton").GetComponent<Button>().onClick.AddListener(Attack);
    }


    private void Attack()
    {
        GameObject bulletObj = Instantiate(_bulletPrefab);
        bulletObj.transform.position = transform.position + transform.forward;
        bulletObj.transform.rotation = transform.rotation;
        bulletObj.GetComponent<Bullet>().SetDamage(_damage);
    }
}
