using System.Collections;
using UnityEngine;

namespace UI
{
    public class SM_SceneFader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        public void BindCanvasGroup(CanvasGroup _canvasGroup)
        {
            canvasGroup = _canvasGroup;
        }

        public IEnumerator FadeIn(float duration)
        {
            yield return FadeRoutine(0f, 1f, duration);
        }

        public IEnumerator FadeOut(float duration)
        {
            yield return FadeRoutine(1f, 0f, duration);
        }

        private IEnumerator FadeRoutine(float from, float to, float duration)
        {
            float elapsed = 0f;
            canvasGroup.alpha = from;
            canvasGroup.blocksRaycasts = false;

            while (elapsed < duration)
            {
                canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = to;
            canvasGroup.blocksRaycasts = true;
        }
    }
}