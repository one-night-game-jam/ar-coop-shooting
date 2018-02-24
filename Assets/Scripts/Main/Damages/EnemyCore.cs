using UnityEngine;

namespace Damages
{
    public class EnemyCore : MonoBehaviour, IDamageApplicable
    {
        public Vector3 Position => transform.position;
    }
}
