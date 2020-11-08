using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private uint _maxHp;

    public int _hp { get; protected set; }

    private void Awake()
    {
        _hp = (int)_maxHp;
    }

    public int GetMaxHp()
    {
        return (int)_maxHp;
    }
}
