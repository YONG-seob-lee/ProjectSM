using System;
using Managers;

namespace UI
{
    public enum ESM_LoadingUIType
    {
        Default = 0,
    }
    public class SM_SceneCommand
    {
        public string NextSceneName; // 다음 씬 이름
        public ESM_LoadingUIType LoadingType; // 로딩 UI 타입
        public int StageId; // 스테이지 진입시 필요한 데이터
        public float FadeDuration; // 페이드 지속 시간
        public Action OnTransitionComplete; // 씬 전환 완료 시 콜백
        public ESM_TransitionType TransitionStyle; // 전환 스타일
        public ESM_UIClearPolicy ClearPolicy; // 씬 관리 정책
        
        public SM_SceneCommand(string next, ESM_LoadingUIType loadingUIType, float fadeDuration,
            Action onComplete, ESM_TransitionType transitionStyle, ESM_UIClearPolicy clearPolicy)
        {
            NextSceneName = next;
            LoadingType = loadingUIType;
            FadeDuration = fadeDuration;
            OnTransitionComplete = onComplete;
            TransitionStyle = transitionStyle;
            ClearPolicy = clearPolicy;
        }
        
        public SM_SceneCommand(string next, ESM_LoadingUIType loadingUIType, int stageId, float fadeDuration,
            Action onComplete, ESM_TransitionType transitionStyle, ESM_UIClearPolicy clearPolicy)
        {
            NextSceneName = next;
            LoadingType = loadingUIType;
            StageId = stageId;
            FadeDuration = fadeDuration;
            OnTransitionComplete = onComplete;
            TransitionStyle = transitionStyle;
            ClearPolicy = clearPolicy;
        }

        public SM_SceneCommand(string next, ESM_TransitionType transitionStyle, ESM_UIClearPolicy clearPolicy)
        {
            NextSceneName = next;
            LoadingType = ESM_LoadingUIType.Default;
            FadeDuration = 0;
            OnTransitionComplete = null;
            TransitionStyle = transitionStyle;
            ClearPolicy = clearPolicy;
        }
    }
    
    public enum ESM_TransitionType
    {
        Direct,
        Fade,
        OnlyFadeIn,
        SlideLeft,
        SlideRight,
    }
}