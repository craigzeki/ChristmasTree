using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public Gradient grad;
    public Image fill;

    public void SetComplete(int progress)
    {
        slider.maxValue = progress;
        slider.value = progress;
        fill.color = grad.Evaluate(1f);
    }

    public void CurrentStatus(int progress)
    {
        slider.value = progress;
        if(progress > 1)
        {
            
        }

        fill.color = grad.Evaluate(slider.normalizedValue);
    }
}