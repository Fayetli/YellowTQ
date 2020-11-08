using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _scoreText;

    private void Start()
    {
        GameObject player = GameObject.FindObjectOfType<CharacterMovement>().gameObject;

        PlayerStat stat = player.GetComponent<PlayerStat>();
        _slider.minValue = 0;
        _slider.maxValue = stat.GetMaxHp();
        _slider.value = _slider.maxValue;
        stat.OnHpChanged += UpdatePlayerHp;

        GameManager.Instance.OnScoreChanged += UpdateScore;
        UpdateScore(GameManager.Instance._score);
    }

    private void UpdatePlayerHp(int hpValue)
    {
        _slider.value = hpValue;
    }

    private void UpdateScore(int value)
    {
        _scoreText.text = "Score: " + value;
    }
}
