using System.Collections.Generic;
using Damages;
using UnityEngine;
using UnityEngine.Networking;

namespace Missiles
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform launchPad;

        private static readonly Quaternion _bulletRotation = Quaternion.Euler(-90, 0, 0);

        public void Launch(IEnumerable<IDamageApplicable> targets)
        {
            foreach (var target in targets)
            {
                var go = Instantiate(_bulletPrefab, launchPad.position, launchPad.rotation * _bulletRotation);
                NetworkServer.Spawn(go);

                var missile = go.GetComponent<IMissile>();
                missile.Launch(target);
            }
        }
    }
}
