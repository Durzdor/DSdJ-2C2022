using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private float timeToShow;
    [SerializeField]private GameObject lifeCanvasPrefab;
    [SerializeField] private Image lifeBar;
    [SerializeField]private Transform visualsTransform;
    private GameObject currCanvas;

    private LifeController lifeController;
    // Start is called before the first frame update
    private void Awake()
    {
        lifeController = GetComponent<LifeController>();
    }

    private void Start()
    {
        lifeController.OnTakeDamage += UpdateCanvas;
        currCanvas = Instantiate(lifeCanvasPrefab, visualsTransform);
    }
    
    // Update is called once per frame
    private void Update()
    {
        
    }

    private void UpdateCanvas(float currLife,float maxLife)
    {
        lifeBar.fillAmount = currLife / maxLife;
        StartCoroutine(ShowLifeCorroutine());
    }

    private IEnumerator ShowLifeCorroutine()
    {
        currCanvas.SetActive(true);
        yield return new WaitForSeconds(timeToShow);
        currCanvas.SetActive(false);
        StopCoroutine(ShowLifeCorroutine());
    }
}
