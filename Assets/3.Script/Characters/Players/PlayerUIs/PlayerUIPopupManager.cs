using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KF
{
    public class PlayerUIPopupManager : MonoBehaviour
    {
        [Header("YOU DIED")]
        [SerializeField] GameObject youDiedPopupGameObject;
        [SerializeField] TextMeshProUGUI youDiedPopupBackgroundText;
        [SerializeField] TextMeshProUGUI youDiedPopupText;
        [SerializeField] CanvasGroup youDiesPopupCanvasGroup;

        public void SendYouDiedPopup()
        {
            youDiedPopupGameObject.SetActive(true);
            youDiedPopupBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopupTextOverTime(youDiedPopupBackgroundText, 8, 10f));
            StartCoroutine(FadeInPopupOverTime(youDiesPopupCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopupOverTime(youDiesPopupCanvasGroup, 2, 5));
        }

        private IEnumerator StretchPopupTextOverTime(TextMeshProUGUI text, float duration, float stretchAmount)
        {
            if (duration > 0f)
            {
                text.characterSpacing = 0;
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer = timer + Time.deltaTime;
                    text.characterSpacing = Mathf.Lerp(text.characterSpacing, stretchAmount, duration * (Time.deltaTime / 20));

                    yield return null;
                }

            }
        }

        private IEnumerator FadeInPopupOverTime(CanvasGroup canvas, float duration)
        {
            if (duration > 0)
            {
                canvas.alpha = 0;
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer = timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration * Time.deltaTime);

                    yield return null;
                }
            }

            canvas.alpha = 1;

            yield return null;
        }

        private IEnumerator WaitThenFadeOutPopupOverTime(CanvasGroup canvas, float duration, float delay)
        {
            if (duration > 0)
            {
                while (delay > 0)
                {
                    delay = delay - Time.deltaTime;
                    yield return null;
                }

                canvas.alpha = 1;
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer = timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * Time.deltaTime);

                    yield return null;
                }
            }

            canvas.alpha = 0;

            yield return null;           
        }
    }

}
