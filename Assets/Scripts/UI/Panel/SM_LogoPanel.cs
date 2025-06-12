using System.Runtime.CompilerServices;
using Managers;
using Systems;
using Table;
using UI.Base;
using UnityEngine;

namespace UI
{
    public class SM_LogoPanel : SM_PanelBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string GetName() { return "Logo"; }

        public void Start()
        {
            Animator animator = GetComponent<Animator>();
            animator.Play("FadeInAnim");
        }
        public void OnLogoFadeOutEnd()
        {
            Animator animator = GetComponent<Animator>();
            animator.enabled = false;
            gameObject.SetActive(false);
            
            SM_TableManager tableManager = (SM_TableManager)SM_GameManager.Instance.GetManager(ESM_Manager.TableManager);
            if (!tableManager)
            {
                SM_Log.ASSERT(false, "[TableManager] is not exist!!!!!");
                return;
            }
            var command = new SM_SceneCommand(
                next: SM_LobbyPanel.GetName(),
                loadingUIType: ESM_LoadingUIType.Default,
                fadeDuration: tableManager ? tableManager.GetParameter<float>(ESM_CommonType.FADE_DURATION_TIME) : 1f,
                onComplete: () =>
                {
                    SM_Log.INFO("메인 Lobby 이동 완료");
                },
                transitionStyle: ESM_TransitionType.OnlyFadeIn,
                clearPolicy: ESM_UIClearPolicy.ClearAllExceptTop);

            SM_SceneManager sceneManager = (SM_SceneManager)SM_GameManager.Instance.GetManager(ESM_Manager.SceneManager);
            sceneManager.RequestSceneChange(command);
        }
    }
}