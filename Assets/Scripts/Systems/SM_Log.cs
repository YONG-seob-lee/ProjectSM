using UnityEngine;

namespace Systems
{
    public static class SM_Log
    {
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void INFO(string message)
        {
            Debug.Log("[INFO] " + message);
        }

        public static void ERROR(string message)
        {
            Debug.Log("[ERROR] " + message);
        }

        public static void WARNING(string message)
        {
            Debug.LogWarning(message);
        }

        public static void ASSERT(bool condition, string message)
        {
            Debug.Assert(condition, message);
        }
    }   
}