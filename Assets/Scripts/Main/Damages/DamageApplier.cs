using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Damages
{
    public class DamageApplier : MonoBehaviour
    {
        private readonly ISubject<IEnumerable<IDamageApplicable>> _hitSubject = new Subject<IEnumerable<IDamageApplicable>>();

        public IObservable<IEnumerable<IDamageApplicable>> HitAsObservable()
        {
            return _hitSubject;
        }

        void OnTriggerEnter(Collider collider)
        {
            var damageApplicables = collider.GetComponents<IDamageApplicable>();
            foreach (var damageApplicablet in damageApplicables)
            {
                damageApplicablet.ApplyDamage();
            }

            if (damageApplicables.Any())
            {
                _hitSubject.OnNext(damageApplicables);
            }
        }
    }
}
