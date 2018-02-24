using System.Collections.Generic;
using System.Linq;
using Damages;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Enemies
{
    public class EnemyPooler : MonoBehaviour
    {
        [SerializeField] private EnemySpawner spawner;

        private readonly IList<EnemyCore> enemies = new List<EnemyCore>();

        void Start()
        {
            spawner.SpawnedEnemyAsObservable()
                .TakeUntilDestroy(spawner)
                .Subscribe(AddEnemy)
                .AddTo(this);
        }

        private void AddEnemy(EnemyCore enemy)
        {
            enemies.Add(enemy);
        }

        public IEnumerable<IDamageApplicable> DamageApplicables()
        {
            return enemies.Where(x => x != null);
        }
    }
}
