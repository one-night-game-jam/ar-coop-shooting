using System;
using Damages;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Missiles
{
    public class Missile : MonoBehaviour, IMissile
    {
        [SerializeField] private float lifetimeSeconds;
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed;

        [SerializeField] private DamageApplier _damageApplier;

        private readonly ISubject<Unit> _destroyAsObservable = new Subject<Unit>();

        public void Launch(IDamageApplicable target)
        {
            this.UpdateAsObservable()
                .TakeUntilDestroy(target.Transform)
                .TakeUntil(_destroyAsObservable)
                .Subscribe(_ => UpdatePosition(target.Transform),
                    () => _destroyAsObservable.OnNext(Unit.Default))
                .AddTo(this);

            Observable.Interval(TimeSpan.FromSeconds(lifetimeSeconds))
                .TakeUntil(_destroyAsObservable)
                .Subscribe(_ => _destroyAsObservable.OnNext(Unit.Default))
                .AddTo(this);

            _damageApplier.HitAsObservable()
                .Subscribe(_ => _destroyAsObservable.OnNext(Unit.Default))
                .AddTo(this);

            _destroyAsObservable
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }

        private void UpdatePosition(Transform target)
        {
            var position = transform.position + transform.forward * speed * Time.deltaTime;
            var rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position), rotationSpeed * Time.deltaTime);
            transform.SetPositionAndRotation(position, rotation);
        }
    }
}
