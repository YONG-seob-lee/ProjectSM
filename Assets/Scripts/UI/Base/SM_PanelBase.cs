using Systems;
using UnityEngine;

namespace UI.Base
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(SM_SceneFader))]
    public abstract class SM_PanelBase : MonoBehaviour
    {
        protected CanvasGroup canvasGroup;
        protected SM_SceneFader screenFader;

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            screenFader = GetComponent<SM_SceneFader>();

            if (screenFader != null)
            {
                screenFader.BindCanvasGroup(canvasGroup);
            }
            else
            {
                SM_Log.ERROR($"[PanelBase] {name}에 SM_ScreenFader가 없습니다.");
            }
        }
    }
}