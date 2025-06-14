using Interface;
using Units;
using UnityEngine;

namespace Weapons
{
    public class SM_WeaponBase : MonoBehaviour, ISM_Weapon
    {
        protected SM_UnitBase _owner;
        
        public void Equip(SM_UnitBase owner)
        {
            _owner = owner;
            transform.parent = owner.transform;
            transform.localPosition = Vector3.zero;
        }

        public virtual void Fire()
        {
            
        }

        public void Reload()
        {
            
        }
    }
}