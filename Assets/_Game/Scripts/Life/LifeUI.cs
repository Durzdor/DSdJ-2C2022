using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : CanvasFiller
{
<<<<<<< Updated upstream
    [SerializeField] private float timeToShow;
    [SerializeField]private GameObject lifeCanvasPrefab;
    private Image lifeBar;
    [SerializeField]private Transform visualsTransform;
    private GameObject currCanvas;
    private Coroutine showLife;
    

    private void ClearLifeBar()
    {
        lifeBar.enabled = false;
    }

    private void FillLifeBar()
    {
        lifeBar.enabled = true;

    }
=======
>>>>>>> Stashed changes
    public void Initialize(LifeController controller)
    {
        SuscribeEvents(controller);
        currCanvas = Instantiate(canvasPrefab, visualsTransform);
        fillImage = currCanvas.GetComponentInChildren<Image>();
        ClearLifeBar();
    }

    private void SuscribeEvents(LifeController controller)
    {
        controller.OnTakeDamage += UpdateCanvas;
        controller.OnDie += ClearLifeBar;
    }
<<<<<<< Updated upstream
    private void UpdateCanvas(float currLife, float maxLife)
    {
        lifeBar.fillAmount = currLife / maxLife;
        print(currLife / maxLife);
        if (showLife == null)
        {
            showLife = StartCoroutine(ShowLifeCorroutine());
        }
    }

    private IEnumerator ShowLifeCorroutine()
    {
        FillLifeBar();
        yield return new WaitForSeconds(timeToShow);
        ClearLifeBar();
        showLife = null;
    }
=======
>>>>>>> Stashed changes
}
