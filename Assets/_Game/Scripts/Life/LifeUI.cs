using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : CanvasFiller
{
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
}
