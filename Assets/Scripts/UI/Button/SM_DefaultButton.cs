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
        Start,
        Option,
        Exit
    }
    
    public class SM_DefaultButton : SM_ButtonBase<ESM_DefaultButtonType>
    {
        public override void OnClick()
        {
            SM_Log.INFO($"[SM_DefaultButton] 버튼 클릭됨: {ButtonType}");

            switch (ButtonType)
            {
                case ESM_DefaultButtonType.Start:
                {
                    SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
                    if (!tableManager)
                    {
                        SM_Log.ASSERT(false, "[TableManager] is not exist!");
                        return;
                    }
                    
                    var command = new SM_SceneCommand(
                        next: SM_HUDPanel.GetName(),
                        loadingUIType: ESM_LoadingUIType.Default,
                        fadeDuration: tableManager ? tableManager.GetParameter<float>(ESM_CommonType.FADE_DURATION_TIME) : 1f,
                        onComplete: () =>
                        {
                            SM_Log.INFO("메인 HUD 이동 완료");
                        },
                        transitionStyle: ESM_TransitionType.Fade,
                        clearPolicy: ESM_UIClearPolicy.ClearAllExceptTop);
                    
                    SM_SceneManager sceneManager = (SM_SceneManager)SM_GameManager.Instance.GetManager(ESM_Manager.SceneManager);
                    sceneManager.RequestSceneChange(command);
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
    }
}