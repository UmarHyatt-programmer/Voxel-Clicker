using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueShifter : MonoBehaviour
{
    public enum ShifterType
    {
        None, Light, Color
    }
    public ShifterType shifterType;
    public float miniValue, maxValue, speed;
    public Light fireLight;
    public Outline image;
    bool isMax;
    void Start()
    {
        //objectOutline.effectColor.a=(field) 20f;
        fireLight = GetComponent<Light>();
    }
    void Update()
    {
        if (shifterType == ShifterType.Light)
        {
            FireLight();
        }
        else if (shifterType == ShifterType.Color)
        {
            ColorShifter();
        }
    }
    public void FireLight()
    {
        if (fireLight.intensity >= maxValue)
        {
            isMax = true;
        }
        else if (fireLight.intensity <= miniValue)
        {
            isMax = false;
        }
        if (isMax)
        {
            fireLight.intensity -= speed;
        }
        else
        {
            fireLight.intensity += speed;
        }

    }
    public void ColorShifter()
    {
        print(isMax);
        if (image.effectColor.a >= maxValue)
        {
            isMax = true;
        }
        else if (image.effectColor.a <= miniValue)
        {
            isMax = false;
        }
        if (isMax)
        {
            image.effectColor -= new Color(0, 0, 0, speed);
           // image.effectColor=new Color32(0,0,0,(byte) speed);
        }
        else
        {
            image.effectColor += new Color(0, 0, 0, speed);
        }

    }
}
