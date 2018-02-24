using System;
using System.Collections.Generic;
using System.Linq;
using Missiles;
using Damages;
using Enemies;
using UniRx;
using UnityEngine;
using Zenject;

namespace Players
{
    public class FireControlSystem : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Launcher _launcher;
        [SerializeField] private float _coolDownTimeSeconds;

        private EnemyPooler _enemyPooler;

        private Dictionary<IDamageApplicable, float> _lockon = new Dictionary<IDamageApplicable, float>();
        private readonly ISubject<IEnumerable<Vector3>> _aimingScreenPositionSubject = new Subject<IEnumerable<Vector3>>();

        private readonly ReactiveProperty<float> _coolDownTimeLastSeconds = new ReactiveProperty<float>();

        [Inject]
        private void Initialize(EnemyPooler enemyPooler)
        {
            _enemyPooler = enemyPooler;
        }

        void Update()
        {
            _coolDownTimeLastSeconds.Value -= Time.deltaTime;

            if (_coolDownTimeLastSeconds.Value <= 0)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Launch();
                    _coolDownTimeLastSeconds.Value = _coolDownTimeSeconds;
                }
                else if (Input.GetMouseButton(0))
                {
                    Lockon();
                }
            }

        }

        private void Lockon()
        {
            if (_enemyPooler == null) return;
            var visibles = _enemyPooler.DamageApplicables()
                .Select(x => Tuple.Create(x, _camera.WorldToScreenPoint(x.Transform.position)))
                .Where(x => 0 < x.Item2.x && x.Item2.x < _camera.pixelWidth && 0 < x.Item2.y && x.Item2.y < _camera.pixelHeight);
            _lockon = visibles.ToDictionary(x => x.Item1, _ => Time.deltaTime).Concat(_lockon.Where(x => x.Key != null))
                .ToLookup(x => x.Key, x => x.Value)
                .ToDictionary(x => x.Key, x => x.Sum());


            _aimingScreenPositionSubject.OnNext(visibles.Select(t => t.Item2));
        }

        private void Launch()
        {
            _launcher.Launch(_lockon.Keys);
            _lockon.Clear();
        }
    }
}
