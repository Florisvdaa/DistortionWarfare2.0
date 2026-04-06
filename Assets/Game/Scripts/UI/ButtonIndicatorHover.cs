using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonIndicatorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Indicators")]
    [SerializeField] private GameObject indicators;
    [SerializeField] private TextMeshProUGUI buttonText;

    private Vector2 initialScale = new Vector2(1, 1);

    private void Awake()
    {
        indicators.SetActive(false);
        transform.localScale = initialScale;
        //buttonText.fontMaterial.SetFloat(ShaderUtilities.ID_UnderlayDilate, -0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        indicators.SetActive(true);
        transform.localScale = new Vector2(1.1f, 1.1f);
        //buttonText.fontMaterial.SetFloat(ShaderUtilities.ID_UnderlayDilate, 1f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        indicators.SetActive(false);
        transform.localScale = initialScale;
        //buttonText.fontMaterial.SetFloat(ShaderUtilities.ID_UnderlayDilate, -0.2f);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = new Vector2(0.9f, 0.9f);
    }

}
