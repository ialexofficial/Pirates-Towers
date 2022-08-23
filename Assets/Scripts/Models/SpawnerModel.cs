using System;
using System.Collections;
using ScriptableObjects;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Models
{
    public class SpawnerModel : IDisposable
    {
        private float _startSleepingTime;
        private float _spawningFrequency;
        private GameObject[] _spawningEnemies;
        private AnimationCurve _spawningCurve;
        private Random _random;
        private bool _disposed;
        private readonly Coroutine _coroutine;
        private readonly MonoBehaviour _monoBehaviour;
        private bool _enemyAlive;

        public event Action OnSpawn;

        public GameObject SpawningEnemy { get; private set; }

        public SpawnerModel(float startSleepingTime, float spawningFrequency, GameObject[] spawningEnemies, AnimationCurve spawningCurve, MonoBehaviour monoBehaviour)
        {
            _startSleepingTime = startSleepingTime;
            _spawningFrequency = spawningFrequency;
            _spawningEnemies = spawningEnemies;
            _spawningCurve = spawningCurve;

            _random = new Random(
                (uint) DateTime.Now.Subtract(
                    new DateTime(1970, 1, 1)
                ).TotalMilliseconds);

            _monoBehaviour = monoBehaviour;
            _coroutine = _monoBehaviour.StartCoroutine(StartSpawning());
        }

        public void RestartSpawner()
        {
            _enemyAlive = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _monoBehaviour.StopCoroutine(_coroutine);
            }

            _disposed = true;
        }

        private IEnumerator StartSpawning()
        {
            yield return new WaitForSeconds(_startSleepingTime);

            while (true)
            {
                GenerateEnemy();

                yield return new WaitWhile(() => _enemyAlive);
                
                yield return new WaitForSeconds(_spawningFrequency);
            }
        }

        private void GenerateEnemy()
        {
            int spawningId = (int) _spawningCurve.Evaluate(_random.NextFloat());
            SpawningEnemy = _spawningEnemies[spawningId];
            _enemyAlive = true;
            
            OnSpawn?.Invoke();
        }

        ~SpawnerModel()
        {
            Dispose(false);
        } 
    }
}