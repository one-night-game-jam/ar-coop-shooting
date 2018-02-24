using UnityEngine;

namespace Damages
{
    public class EnemyCore : MonoBehaviour, IDamageApplicable
    {
        public Transform Transform => transform;

        public void ApplyDamage()
        {
            Destroy(gameObject);
        }
    }
}
