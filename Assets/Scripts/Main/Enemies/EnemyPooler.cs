using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Enemies
{
    public class EnemyPooler : MonoBehaviour
    {
        [SerializeField] private EnemySpawner spawner;

        private readonly ReactiveCollection<GameObject> enemies = new ReactiveCollection<GameObject>();

        void Start()
        {
            spawner.SpawnedEnemyAsObservable()
                .TakeUntilDestroy(spawner)
                .Subscribe(AddEnemy)
                .AddTo(this);
        }

        private void AddEnemy(GameObject enemy)
        {
            enemies.Add(enemy);
        }

        public IEnumerable<GameObject> Enemies()
        {
            return enemies.Where(x => x != null);
        }
    }
}
