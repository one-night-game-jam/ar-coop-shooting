using Damages;

namespace Missiles
{
    public interface IMissile
    {
        void Launch(IDamageApplicable target);
    }
}
