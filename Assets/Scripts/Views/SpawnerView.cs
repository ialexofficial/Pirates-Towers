using Models;
using ScriptableObjects;
using UnityEngine;
using StateMachine;

namespace Views
{
    public class SpawnerView : MonoBehaviour
    {
        [SerializeField] private float startSleepingTime = 0f;
        [SerializeField] private float spawnFrequency = 1.5f;
        [SerializeField] private GameObject[] spawningEnemies;
        [SerializeField] private AnimationCurve randomSpawningCurve;
        [Tooltip("The first state in array will be applied at start")]
        [SerializeField] private State[] states;

        private SpawnerModel _model;
        
        private void Awake()
        {
            _model = new SpawnerModel(startSleepingTime, spawnFrequency, spawningEnemies, randomSpawningCurve, this);
            _model.OnSpawn += Spawned;
        }

        private void Spawned()
        {
            EnemyView spawnedEnemy = Instantiate(_model.SpawningEnemy, transform.position, transform.rotation).GetComponent<EnemyView>();
            
            spawnedEnemy.OnDie.AddListener((int money) => _model.RestartSpawner());
            spawnedEnemy.SetStateArray(states);
        }

        private void OnDestroy()
        {
            _model.OnSpawn -= Spawned;
            _model.Dispose();
        }
    }
}