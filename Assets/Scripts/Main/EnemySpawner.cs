using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.XR.iOS;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private ARPlaneGenerator planeGenerator;

    [Inject]
    void Initialize(ARPlaneGenerator planeGenerator)
    {
        this.planeGenerator = planeGenerator;
    }

    void Start()
    {
        SpawnTimerAsObservable()
            .SkipWhile(_ => planeGenerator == null)
            .Select(_ => planeGenerator.GetCurrentPlaneAnchors())
            .Where(x => x.Any())
            .Select(x => GetRandomPoint(x.Sample()))
            .Subscribe(t => SpawnEnemy(t.Item1, t.Item2))
            .AddTo(this);
    }

    private IObservable<long> SpawnTimerAsObservable()
    {
        return Observable.Interval(TimeSpan.FromSeconds(3));
    }

    public static Tuple<Vector3, Quaternion> GetRandomPoint(ARPlaneAnchorGameObject planeAnchorGameObject)
    {
        var position = planeAnchorGameObject.gameObject.transform.position + planeAnchorGameObject.planeAnchor.planeGeometry.vertices.Sample();
        var rotation = planeAnchorGameObject.gameObject.transform.rotation;
        return Tuple.Create(position, rotation);
    }

    private void SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        var enemy = Instantiate(enemyPrefab);
        enemy.transform.SetPositionAndRotation(position, rotation);
        //return enemy;
    }
}
