using System;
using UniRx;
using UnityEngine;

public class Reaper : MonoBehaviour
{
    [SerializeField] private float lifetimeSeconds;

    void Start()
    {
        TimeOfDeathAsObservable()
            .Subscribe(_ => Destroy(gameObject))
            .AddTo(this);
    }

    private IObservable<long> TimeOfDeathAsObservable()
    {
        return Observable.Interval(TimeSpan.FromSeconds(lifetimeSeconds));
    }
}
