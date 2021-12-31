using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchupBar : MonoBehaviour
{

    public Slider CatchupBarUI;
    public Image FillImage;
    public float FillSpeed = 10;
    private Slider currentSlider;
    public Color StartColour;
    public Color EndColour;

    private void Start()
    {
        currentSlider = GetComponent<Slider>();
    }

    void Update()
    {
        if(CatchupBarUI.value > currentSlider.value - 0.01f && CatchupBarUI.value < currentSlider.value + 0.01f)
        {
            FillImage.color = EndColour;
            return;
        }
        var valueDif = Mathf.Abs(CatchupBarUI.value - currentSlider.value);
        if (CatchupBarUI.value < currentSlider.value)
        {
            CatchupBarUI.value += Time.deltaTime * FillSpeed * valueDif;
        }
        else if (CatchupBarUI.value > currentSlider.value)
        {
            CatchupBarUI.value -= Time.deltaTime * FillSpeed * valueDif;
        }
        FillImage.color = Color.Lerp(StartColour, EndColour, 1f - Mathf.Abs(CatchupBarUI.value - currentSlider.value));

    }
}
