using DG.Tweening;
using UnityEngine;

namespace AnimaRes
{
    public class Fade : BaseTransition
    {
        [SerializeField] private new Renderer renderer;
        [SerializeField] private float fadeInDuration = 1;
        [SerializeField] private float fadeOutDuration = 1;

        private void Awake()
        {
            var color = renderer.material.color;

            beginTween = DOTween.Sequence()
                .OnStart(() => color.a = 0)
                .Append(DOTween.To(() => color.a, a => color.a = a, 1, Mathf.Abs(fadeInDuration))
                               .OnUpdate(() => renderer.material.color = color))
                .SetAutoKill(false)
                .Pause();

            endTween = DOTween.Sequence()
                .Append(DOTween.To(() => color.a, a => color.a = a, 0, Mathf.Abs(fadeInDuration))
                               .OnUpdate(()=> renderer.material.color = color))
                .SetAutoKill(false)
                .Pause();
        }

        private void Start()
        {
            EnterTransition();
        }
    }
}