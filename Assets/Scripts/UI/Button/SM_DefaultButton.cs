using System.Collections.Generic;
using Managers;
using Systems;
using Table;
using UI.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI.Button
{
    public enum ESM_DefaultButtonType
    {
        IntoStage,
        StartStage,
        Option,
        Exit
    }
    
    public class SM_DefaultButton : SM_ButtonBase<ESM_DefaultButtonType>
    {
        private int CurrentStageId = 0;
        public override void OnClick()
        {
            SM_Log.INFO($"[SM_DefaultButton] 버튼 클릭됨: {ButtonType}");

            switch (ButtonType)
            {
                case ESM_DefaultButtonType.IntoStage:
                {
                    SM_StageManager stageManager = (SM_StageManager)SM_GameManager.Instance.GetManager(ESM_Manager.StageManager);
                    if (!stageManager)
                    {
                        SM_Log.ASSERT(false, "[Stage Manager] is not exist!");
                        return;
                    }
                    
                    stageManager.IntoStage(CurrentStageId);
                    
                    break;
                }
                case ESM_DefaultButtonType.StartStage:
                {
                    SM_StageManager stageManager = (SM_StageManager)SM_GameManager.Instance.GetManager(ESM_Manager.StageManager);
                    if (!stageManager)
                    {
                        SM_Log.ASSERT(false, "[Stage Manager] is not exist!");
                        return;
                    }
                    stageManager.StartStage();
                    break;
                }
                case ESM_DefaultButtonType.Option:
                {
                    break;
                }
                case ESM_DefaultButtonType.Exit:
                {
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        
        public void SetStageId(int stageId)
        {
            CurrentStageId = stageId; 
        }
    }
}