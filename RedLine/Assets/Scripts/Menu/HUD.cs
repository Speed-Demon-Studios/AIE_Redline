using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private Image energyBarFill, energyLeadingEdgeLower, energyLeadingEdgeUpper, speedBarFill, speedbarRedFill;
    [SerializeField]
    private GameObject energyFullSegment1, energyFullSegment2, energyFullSegment3;
    [SerializeField]
    private TextMeshProUGUI speedText, posText, lapText, lapTimeText;
    [Range(0f, 1f)]
    public float energyBarFillAmount, speedBarFillAmount;

    [SerializeField]
    private float leadingEdgeWidth;

    public bool gainingRedline;

    [Range(0, 3)]
    public int currentBoostLevel;

    [SerializeField]
    private AnimationCurve energySegmentPulse;
    [SerializeField]
    private float pulseLength;

    private Color flickeringColor = Color.white, pulsingColor = new Color(0,0,0,0.5f), fullSegment1Color, fullSegment2Color, fullSegment3Color;

    public Vector2 energyBarFillRange, energyLeadingEdgeFillRange, speedBarFillRange;

    public int kph, position, totalPositions, lap, totalLaps;

    public float[] lapTimes;

    private void OnEnable()
    {
        fullSegment1Color = energyFullSegment1.GetComponent<Image>().color;
        fullSegment2Color = energyFullSegment2.GetComponent<Image>().color;
        fullSegment3Color = energyFullSegment3.GetComponent<Image>().color;
        Tween.Value(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), PulsingColor, pulseLength, 0, energySegmentPulse, Tween.LoopType.Loop);
    }

    void PulsingColor(Color color)
    {
        pulsingColor = color;
    }

    // Re-maps one value range to another
    public float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    // Updates the HUD display with current values
    void UpdateHUD()
    {
        // Energy bar
        float energyFill = map(energyBarFillAmount, 0, 1, energyBarFillRange.x, energyBarFillRange.y);
        energyBarFill.fillAmount = energyFill;

        // Energy bar full segments

        Color currentBoostColor = Color.white;

        switch (currentBoostLevel)
        {
            case 0:
                energyFullSegment1.SetActive(false);
                energyFullSegment2.SetActive(false);
                energyFullSegment3.SetActive(false);
                break;

            case 1:
                energyFullSegment1.SetActive(true);
                energyFullSegment2.SetActive(false);
                energyFullSegment3.SetActive(false);

                energyFullSegment1.GetComponent<Image>().color = fullSegment1Color - pulsingColor; 
                break;

            case 2:
                energyFullSegment1.SetActive(true);
                energyFullSegment2.SetActive(true);
                energyFullSegment3.SetActive(false);

                energyFullSegment1.GetComponent<Image>().color = fullSegment2Color - pulsingColor;
                energyFullSegment2.GetComponent<Image>().color = fullSegment2Color - pulsingColor;
                break;

            case 3:
                energyFullSegment1.SetActive(true);
                energyFullSegment2.SetActive(true);
                energyFullSegment3.SetActive(true);

                energyFullSegment1.GetComponent<Image>().color = fullSegment3Color - pulsingColor;
                energyFullSegment2.GetComponent<Image>().color = fullSegment3Color - pulsingColor;
                energyFullSegment3.GetComponent<Image>().color = fullSegment3Color - pulsingColor;
                break;

            default:
                energyFullSegment1.SetActive(false);
                energyFullSegment2.SetActive(false);
                energyFullSegment3.SetActive(false);
                break;

        }

        

        // Glowing leading edge of energy bar
        float intensity = Random.Range(0.5f, 1f);
        flickeringColor.a = intensity;

        if (gainingRedline)
        {
            float energyLELowerFill = map(energyBarFillAmount - leadingEdgeWidth, 0, 1, energyLeadingEdgeFillRange.x, energyLeadingEdgeFillRange.y);
            float energyLEUpperFill = map(energyBarFillAmount, 0, 1, energyBarFillRange.x, energyBarFillRange.y);
            energyLeadingEdgeLower.fillAmount = energyLELowerFill;
            energyLeadingEdgeUpper.fillAmount = energyLEUpperFill;

            energyLeadingEdgeUpper.color = flickeringColor;
        }
        else
        {
            float energyLELowerFill = 0;
            float energyLEUpperFill = 0;
            energyLeadingEdgeLower.fillAmount = energyLELowerFill;
            energyLeadingEdgeUpper.fillAmount = energyLEUpperFill;
        }

        // Speed bar
        float speedFill = map(speedBarFillAmount, 0, 1, speedBarFillRange.x, speedBarFillRange.y);
        speedBarFill.fillAmount = speedFill;
        speedbarRedFill.color = flickeringColor;

        speedText.text = "<b>" + kph.ToString() + "</b> Kph";

        posText.text = position.ToString() + " / " + totalPositions.ToString();

        lapText.text = lap.ToString() + " / " + totalLaps.ToString();

        string lapTimeStr = "";

        foreach (float time in lapTimes)
        {
            // Use correct time formatting here. eg 1:16:563 
            lapTimeStr += time.ToString() + "\n"; 
        }

        lapTimeText.text = lapTimeStr;
    }

    private void Update()
    {
        UpdateHUD();
    }

}
