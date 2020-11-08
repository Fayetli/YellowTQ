using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealbar : MonoBehaviour
{
    [SerializeField] private Text _takenDamage;
    [SerializeField] private Slider _slider;
    [SerializeField] private Enemy _enemy;
    private void Start()
    {
        _slider.minValue = 0;
        _slider.maxValue = _enemy.GetMaxHp();
        _slider.value = _slider.maxValue;

        _enemy.OnHpChanged += UpdatePlayerHp;
    }

    private void UpdatePlayerHp(int hpValue)
    {
        StopCoroutine(DisableTakenDamageText(1.0f));
        _takenDamage.text = "-" + (_slider.value - hpValue);
        StartCoroutine(DisableTakenDamageText(1.0f));

        _slider.value = hpValue;
    }

    private IEnumerator DisableTakenDamageText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _takenDamage.text = "";
    }
}
