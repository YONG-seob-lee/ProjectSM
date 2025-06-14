
using Units;

namespace Interface
{
    public interface ISM_Weapon
    {
        void Equip(SM_UnitBase owner);
        void Fire();
        void Reload();
    }
}