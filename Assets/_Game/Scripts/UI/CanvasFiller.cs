using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Collections.Generic;

public class CanvasFiller : MonoBehaviour
{
    [SerializeField] protected float timeToShow;
    [SerializeField] protected Image fillImage;
    [SerializeField] protected bool isPermanent = false;
    protected Coroutine showCanvas;
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
            print("SI");
            fillImage.fillAmount = currentValue / maxValue;
            if(isPermanent){return;}
            showCanvas ??= StartCoroutine(ShowCanvasForTime());
        }
    }