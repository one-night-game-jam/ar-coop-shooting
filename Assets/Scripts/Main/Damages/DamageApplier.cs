using UnityEngine;

namespace Damages
{
    public class DamageApplier : MonoBehaviour
    {
        void OnTriggerEnter(Collider collider)
        {
            foreach (var damageApplicablet in collider.GetComponents<IDamageApplicable>())
            {
                damageApplicablet.ApplyDamage();
            }
        }
    }
}
