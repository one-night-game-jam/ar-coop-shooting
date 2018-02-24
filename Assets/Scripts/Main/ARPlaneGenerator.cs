using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.XR.iOS;


public class ARPlaneGenerator : MonoBehaviour
{
    public GameObject planePrefab;
    private UnityARAnchorManager unityARAnchorManager;

    void Start ()
    {
        unityARAnchorManager = new UnityARAnchorManager();
        UnityARUtility.InitializePlanePrefab (planePrefab);
    }

    void OnDestroy()
    {
        unityARAnchorManager.Destroy ();
    }

    public IEnumerable<ARPlaneAnchorGameObject> GetCurrentPlaneAnchors()
    {
        return unityARAnchorManager.GetCurrentPlaneAnchors();
    }
}


