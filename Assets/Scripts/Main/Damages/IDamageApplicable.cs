using UnityEngine;

namespace Damages
{
    public interface IDamageApplicable
    {
        Transform Transform { get; }

        void ApplyDamage();
    }
}
