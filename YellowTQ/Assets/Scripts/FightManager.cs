using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FightManager : MonoBehaviour
{
    [SerializeField] List<StageEnemy> _stages;
    private int _stageNumber = 0;


    private void Start()
    {
        SpawnNewEnemy();
    }

    public void SpawnNewEnemy()
    {
        if(_stageNumber < _stages.Count)
        {
            _stages[_stageNumber]._manager = this;
            _stages[_stageNumber++].Spawn();
        }
    }

    
}
