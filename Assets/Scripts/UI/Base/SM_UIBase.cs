using Systems;
using UnityEngine;

namespace UI.Base
{
    public class SM_UIBase : MonoBehaviour
    {

        protected T FindChild<T>(string name) where T : Component
        {
            var target = transform.Find(name);
            if (!target)
            {
                SM_Log.WARNING($"[UIBase] Cannot find Child : {name}");
                return null;
            }
            
            return target.GetComponent<T>();
        }
    }
}