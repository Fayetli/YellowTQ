using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int _score { get; private set; }

    public delegate void ScoreChange(int value);
    public event ScoreChange OnScoreChanged;



    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(uint value)
    {
        _score += (int)value;
        OnScoreChanged?.Invoke(_score);
    }

    public void RestartLevel()
    {
        _score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
