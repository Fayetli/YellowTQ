using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStat : Unit
{
    public delegate void HpChange(int value);
    public event HpChange OnHpChanged;

    public void Heal(uint value)
    {
        _hp += (int)value;
        if (_hp > GetMaxHp())
            _hp = GetMaxHp();

        OnHpChanged?.Invoke(_hp);
    }

    public void GetDamage(uint value)
    {
        _hp -= (int)value;

        if (_hp <= 0)
            Dead();

        OnHpChanged?.Invoke(_hp);
        print(_hp);
    }



    private void Dead()
    {
        GameManager.Instance.RestartLevel();
    }
}
