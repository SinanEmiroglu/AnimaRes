using UnityEngine;
using DG.Tweening;

namespace AnimaRes
{
    public class UIFade : BaseTransition
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeInDuration = 1;
        [SerializeField] private float fadeOutDuration = 1;

        private void OnEnable()
        {
            if (canvasGroup == null)
            {
                return;
            }

            beginTween = DOTween.Sequence()
                .OnStart(() => canvasGroup.alpha = 0)
                .Append(DOTween.To(() => canvasGroup.alpha, a => canvasGroup.alpha = a, 1, Mathf.Abs(fadeInDuration))).Pause();

            endTween = DOTween.Sequence()
                .Append(DOTween.To(() => canvasGroup.alpha, a => canvasGroup.alpha = a, 0, Mathf.Abs(fadeOutDuration))).Pause();

            EnterTransition();
        }
    }
}