using Managers;
using Table;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installer
{
    public class SM_TableInstaller : MonoInstaller
    {
        [SerializeField] private SM_TableManager tableManager;
        [SerializeField] private SM_Common_DataTable commonTable;
        [SerializeField] private SM_PathDirectory_DataTable directoryTable;
        [SerializeField] private SM_PrefabFile_DataTable prefabDataTable;
        [SerializeField] private SM_UI_DataTable uiTable;
        [SerializeField] private SM_Item_DataTable itemTable;
        [SerializeField] private SM_Mode_DataTable modeTable;
        [FormerlySerializedAs("modeSequenceTable")] [SerializeField] private SM_Stage_DataTable stageTable;
        [SerializeField] private SM_EnemyUnit_DataTable enemyTable;

        // ReSharper disable Unity.PerformanceAnalysis

        public override void InstallBindings()
        {
            Container.Bind<SM_TableManager>().FromInstance(tableManager).AsSingle();
            Container.Inject(this);

            Container.Bind<SM_Common_DataTable>().FromInstance(commonTable).AsSingle();
            tableManager.RegisterTable(ESM_TableType.Common, commonTable);
            Container.Bind<SM_PathDirectory_DataTable>().FromInstance(directoryTable).AsSingle();
            tableManager.RegisterTable(ESM_TableType.Directory, directoryTable);
            Container.Bind<SM_PrefabFile_DataTable>().FromInstance(prefabDataTable).AsSingle();
            tableManager.RegisterTable(ESM_TableType.PrefabFile, prefabDataTable);
            Container.Bind<SM_UI_DataTable>().FromInstance(uiTable).AsSingle();
            tableManager.RegisterTable(ESM_TableType.UI, uiTable);
            Container.Bind<SM_Item_DataTable>().FromInstance(itemTable).AsSingle();
            tableManager.RegisterTable(ESM_TableType.Item, itemTable);
            Container.Bind<SM_Mode_DataTable>().FromInstance(modeTable).AsSingle();
            tableManager.RegisterTable(ESM_TableType.Mode, modeTable);
            Container.Bind<SM_Stage_DataTable>().FromInstance(stageTable).AsSingle();
            tableManager.RegisterTable(ESM_TableType.Stage, stageTable);
            Container.Bind<SM_EnemyUnit_DataTable>().FromInstance(enemyTable).AsSingle();
            tableManager.RegisterTable(ESM_TableType.EnemyUnit, enemyTable);
        }
    }
}