using System.Collections.Generic;
using Interface;
using Managers;
using Systems;
using UnityEngine;

namespace Table
{
    [CreateAssetMenu(fileName = "StageDataTable", menuName = "Tables/Stage Data Table")]
    public class SM_Stage_DataTable : ScriptableObject, ISM_DataTable
    {
        public Dictionary<int, SM_StageEntry> DataMap = new();
        public void RegisterData()
        {
            TextAsset textAsset = SM_SystemLibrary.CreateTextAsset("Tables/Stage");
            if (textAsset != null)
            {
                SM_SystemLibrary.CreateTableObject(textAsset, DataMap);
            }
        }

        public void Clear() => DataMap.Clear();

        public List<int> GetModeSequence(int stageId)
        {
            SM_StageEntry stageEntry = DataMap.GetValueOrDefault(stageId);
            if (stageEntry != null)
            {
                return stageEntry.SequenceMode;
            }
            
            return null;
        }

        public string GetHubName(int stageId)
        {
            SM_StageEntry stageEntry = DataMap.GetValueOrDefault(stageId);
            if (stageEntry != null)
            {
                return stageEntry.HudName;
            }

            return null;
        }

        public string GetTileMapPath(int stageId)
        {
            SM_StageEntry stageEntry = DataMap.GetValueOrDefault(stageId);
            if (stageEntry != null)
            {
                SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
                if (!tableManager)
                {
                    SM_Log.ASSERT(false, "[Table Manager] is not exist!");
                    return null;
                }
                SM_TileMap_DataTable tileMapDataTable = (SM_TileMap_DataTable)tableManager.GetTable(ESM_TableType.TileMap);
                if (!tileMapDataTable)
                {
                    SM_Log.ASSERT(false, "[Tile Map Table] is not exist!");
                    return null;
                }

                return tileMapDataTable.GetTileMapPath(stageEntry.TileMapKey);
            }

            return null;
        }

        public string GetBackgroundPath(int stageId)
        {
            SM_StageEntry stageEntry = DataMap.GetValueOrDefault(stageId);
            if (stageEntry != null)
            {
                SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
                if (!tableManager)
                {
                    SM_Log.ASSERT(false, "[Table Manager] is not exist!");
                    return null;
                }

                SM_Bg_DataTable bgDataTable = (SM_Bg_DataTable)tableManager.GetTable(ESM_TableType.Background);
                if (!bgDataTable)
                {
                    SM_Log.ASSERT(false, "[BG Table] is not exist!");
                    return null;
                }

                return bgDataTable.GetBgPath(stageEntry.BackgroundKey);
            }

            return null;
        }
    }

    public class SM_StageEntry : SM_Data
    {
        public int Key;
        public string HudName;
        public int StageIndex;
        public int TileMapKey;
        public int BackgroundKey;
        public List<int> SequenceMode;
    }
}