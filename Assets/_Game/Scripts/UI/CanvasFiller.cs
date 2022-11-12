using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Collections.Generic;

public class CanvasFiller : MonoBehaviour
    {
        [SerializeField]protected Transform visualsTransform;
        [SerializeField]protected GameObject canvasPrefab;
        [SerializeField] protected float timeToShow;
        protected Coroutine showCanvas;
        protected GameObject currCanvas;
        protected Image fillImage;

        protected virtual void ClearLifeBar()
        {
            showCanvas = null;
            StopAllCoroutines();
            fillImage.enabled = false;
        }

        protected virtual void FillLifeBar()
        {
            fillImage.enabled = true;

        }
        protected virtual void OnDestroy()
        {
            StopAllCoroutines();
        }
        protected virtual IEnumerator ShowCanvasForTime()
        {
            FillLifeBar();
            yield return new WaitForSeconds(timeToShow);
            ClearLifeBar();
            showCanvas = null;
        }
        protected virtual void UpdateCanvas(float currentValue, float maxValue)
        {
            fillImage.fillAmount = currentValue / maxValue;
            if (showCanvas == null)
            {
                showCanvas = StartCoroutine(ShowCanvasForTime());
            }
        }
    }