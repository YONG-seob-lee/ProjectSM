using System;
using System.Collections.Generic;
using Systems;

namespace Managers
{
    public class SM_ManagerContainer
    {
        private readonly Dictionary<Type, ISM_ManagerBase> _managers = new();

        public void Register<T>(T manager) where T : ISM_ManagerBase
        {
            _managers[typeof(T)] = manager;
        }

        public T Get<T>() where T : class, ISM_ManagerBase
        {
            return _managers[typeof(T)] as T;
        }

        public void InitAll()
        {
            
        }

        public void DestroyAll()
        {
            foreach (var manager in _managers.Values)
            {
                manager.DestroyManager();
            }
        }
    }
}