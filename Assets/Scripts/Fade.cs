using DG.Tweening;
using UnityEngine;

namespace AnimaRes
{
    public class Fade : BaseTransition
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private float fadeInDuration = 1;
        [SerializeField] private float fadeOutDuration = 1;

        private void OnEnable()
        {
            if (renderer == null)
            {
                return;
            }

            var color = renderer.material.color;

            beginTween = DOTween.Sequence()
                .OnStart(() => color.a = 0)
                .Append(DOTween.To(() => color.a, a => color.a = a, 1, Mathf.Abs(fadeInDuration))
                               .OnUpdate(() => renderer.material.color = color)).Pause();

            endTween = DOTween.Sequence()
                .Append(DOTween.To(() => color.a, a => color.a = a, 0, Mathf.Abs(fadeOutDuration))
                               .OnUpdate(() => renderer.material.color = color)).Pause();

            EnterTransition();
        }
    }
}