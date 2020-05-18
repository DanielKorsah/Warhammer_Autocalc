using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class RollButton : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField ballistic;
    [SerializeField]
    private TMP_InputField strength;
    [SerializeField]
    private TMP_InputField toughness;
    [SerializeField]
    private TMP_InputField shotNumber;

    public void ActivateCalculator()
    {
        if (ballistic.text == "")
        {
            Debug.Log("Null Input");
            return;
        }
        if (strength.text == "")
        {
            Debug.Log("Null Input");
            return;
        }
        if (toughness.text == "")
        {
            Debug.Log("Null Input");
            return;
        }
        if (shotNumber.text == "")
        {
            Debug.Log("Null Input");
            return;
        }

        int b = Convert.ToInt32(ballistic.text);
        int s = Convert.ToInt32(strength.text);
        int t = Convert.ToInt32(toughness.text);
        int sh = Convert.ToInt32(shotNumber.text);
        Calculator.Instance.Calculate(b, s, t, sh);
    }
}