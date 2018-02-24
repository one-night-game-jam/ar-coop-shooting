using System.Collections.Generic;
using Damages;
using UnityEngine;

namespace Missiles
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;

        private static readonly Quaternion _bulletRotation = Quaternion.Euler(-90, 0, 0);

        public void Launch(IEnumerable<IDamageApplicable> targets)
        {
            foreach (var target in targets)
            {
                var bullet = Instantiate(_bulletPrefab, transform.position, _bulletRotation).GetComponent<IMissile>();
                bullet.Launch(target);
            }
        }
    }
}
