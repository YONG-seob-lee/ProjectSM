using System;
using System.Collections.Generic;
using System.Reflection;
using Interface;
using UI;
using UnityEngine;
using Object = System.Object;

namespace Systems
{
    public static class SM_SystemLibrary
    {
        public static TextAsset CreateTextAsset(string assetPath)
        {
            var textAsset = Resources.Load<TextAsset>(assetPath);
            if (textAsset == null)
            {
                SM_Log.ERROR("UIDataTable CSV not found!!");
                return null;
            }

            return textAsset;
        }
        
        public static void CreateTableObject<TEntry>(TextAsset textAsset, Dictionary<int, TEntry> dataMap)
            where TEntry : SM_Data, new()
        {
            dataMap.Clear();
            // so 파일 로드 또는 생성
            string[] lines = textAsset.text.Split('\n');

            string[] headers = lines[0].Trim().Split(',');
            for (int i = 1; i < lines.Length; ++i)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                string[] tokens = lines[i].Trim().Split(',');
                if (tokens.Length != headers.Length)
                {
                    SM_Log.WARNING($"CSV 파싱 오류: {i}번째 줄의 열 개수가 헤더와 다릅니다");
                }
                
                TEntry entry = new TEntry();
                Type entryType = typeof(TEntry);

                for (int j = 0; j < headers.Length; ++j)
                {
                    string fieldName = headers[j].Trim();
                    string rawValue = tokens[j].Trim();
                    
                    FieldInfo field = entryType.GetField(fieldName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (field == null)
                    {
                        SM_Log.WARNING($"필드 {fieldName} 을(를) {entryType.Name}에서 찾을 수 없습니다.");
                        continue;
                    }

                    object converted = ConvertValue(field.FieldType, rawValue);
                    field.SetValue(entry, converted);
                }

                FieldInfo keyField = entryType.GetField("Key", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (keyField == null)
                {
                    SM_Log.WARNING($"{entryType.Name}에 Key 필드가 없습니다.");
                    continue;
                }

                int key = (int)keyField.GetValue(entry);
                
                if (dataMap.ContainsKey(key))
                {
                    SM_Log.WARNING($"중복된 ID({key})가 발견되었습니다. 덮어씁니다.");
                }

                dataMap[key] = entry;
            }
        }

        private static object ConvertValue(Type type, string raw)
        {
            if (raw.Length <= 0) return default;
            
            if (type == typeof(int)) return int.Parse(raw);
            if (type == typeof(float)) return float.Parse(raw);
            if (type == typeof(string)) return raw;
            if (type == typeof(bool)) return bool.Parse(raw);
            if (type == typeof(GameObject)) return Resources.Load<GameObject>(raw);

            throw new Exception($"지원되지 않는 타입 : {type.Name}");
        }

        public static GameObject GetLoadingUI(ESM_LoadingUIType commandLoadingType)
        {
            switch (commandLoadingType)
            {
                case ESM_LoadingUIType.Default:
                {
                    return UnityEngine.Object.Instantiate(Resources.Load<GameObject>(SM_LoadingDefaultPanel.GetPath()));
                }
                default:
                {
                    return UnityEngine.Object.Instantiate(Resources.Load<GameObject>(SM_LoadingDefaultPanel.GetPath()));
                }
            }
        }
    }
}