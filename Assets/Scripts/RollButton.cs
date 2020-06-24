using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class RollButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField ballistic;
    [SerializeField] private TMP_InputField strength;
    [SerializeField] private TMP_InputField toughness;
    [SerializeField] private TMP_InputField shotNumber;
    [SerializeField] private TMP_InputField piercing;
    [SerializeField] private TMP_InputField armour;

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
        if (piercing.text == "")
        {
            Debug.Log("Null Input");
            return;
        }
        if (armour.text == "")
        {
            Debug.Log("Null Input");
            return;
        }

        int b = Convert.ToInt32(ballistic.text);
        int s = Convert.ToInt32(strength.text);
        int t = Convert.ToInt32(toughness.text);
        int sh = Convert.ToInt32(shotNumber.text);
        int ap = Convert.ToInt32(piercing.text);
        int a = Convert.ToInt32(armour.text);
        Calculator.Instance.Calculate(b, s, t, sh, ap, a);

        int x = 0;
        do
        {
            x++;
        } while (x < 2);
    }
}