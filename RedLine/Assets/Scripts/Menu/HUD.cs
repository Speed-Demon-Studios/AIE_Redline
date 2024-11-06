using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private Image energyBarFill, energyLeadingEdgeLower, energyLeadingEdgeUpper, speedBarFill;
    [SerializeField]
    private TextMeshProUGUI speedText, posText, lapText, lapTimeText;
    [Range(0f, 1f)]
    public float energyBarFillAmount, speedBarFillAmount;

    [SerializeField]
    private float leadingEdgeWidth;

    public Vector2 energyBarFillRange, energyLeadingEdgeFillRange, speedBarFillRange;

    public int kph, position, totalPositions, lap, totalLaps;

    public float[] lapTimes;
    
    // Re-maps one value range to another
    public float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    // Updates the HUD display with current values
    void UpdateHUD()
    {
        float energyFill = map(energyBarFillAmount, 0, 1, energyBarFillRange.x, energyBarFillRange.y);
        energyBarFill.fillAmount = energyFill;

        float energyLELowerFill = map(energyBarFillAmount, 0, 1, energyLeadingEdgeFillRange.x, energyLeadingEdgeFillRange.y);
        float energyLEUpperFill = map(energyBarFillAmount + leadingEdgeWidth, 0, 1, energyBarFillRange.x, energyBarFillRange.y);
        energyLeadingEdgeLower.fillAmount = energyLELowerFill;
        energyLeadingEdgeUpper.fillAmount = energyLEUpperFill;

        float speedFill = map(speedBarFillAmount, 0, 1, speedBarFillRange.x, speedBarFillRange.y);
        speedBarFill.fillAmount = speedFill;

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
