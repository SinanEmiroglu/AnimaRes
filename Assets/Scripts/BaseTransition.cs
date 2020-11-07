using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;

namespace AnimaRes
{
    [DisallowMultipleComponent]
    public class BaseTransition : MonoBehaviour
    {
        public UnityEvent OnInitialBegin;
        public UnityEvent OnInitialEnd;
        public UnityEvent OnFinalBegin;
        public UnityEvent OnFinalEnd;

        protected Tween beginTween;
        protected Tween endTween;

        public void EnterTransition()
        {
            StartCoroutine(BeginCoroutine());
        }

        public void ExitTransition()
        {
            StartCoroutine(EndCoroutine());
        }

        private IEnumerator BeginCoroutine()
        {
            OnInitialBegin?.Invoke();
            yield return beginTween.Play().WaitForCompletion();
            OnInitialEnd?.Invoke();
        }

        private IEnumerator EndCoroutine()
        {
            OnFinalBegin?.Invoke();
            yield return endTween.Play().WaitForCompletion();
            OnFinalEnd?.Invoke();
        }
    }
}