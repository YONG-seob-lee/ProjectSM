using UnityEngine;

namespace Characters
{
    public class SM_PlayerUnit : SM_UnitBase
    {
        protected override void Update()
        {
            base.Update();

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vectical"));
            
            SetMovement(input);
        }
    }
}