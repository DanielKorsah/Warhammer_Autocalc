using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI HitRollText;
    public TextMeshProUGUI HitResultText;
    public TextMeshProUGUI WoundRollText;
    public TextMeshProUGUI WoundThresholdText;
    public TextMeshProUGUI WoundResultText;
    void Start()
    {
        Calculator.HitValue.AddListener(SetHitRoll);
        Calculator.HitResult.AddListener(SetHitResult);
        Calculator.WoundValue.AddListener(SetWoundRoll);
        Calculator.WoundThreshold.AddListener(SetWoundThreshold);
        Calculator.WoundResult.AddListener(SetWoundResult);
    }

    void SetHitRoll(int rollValue)
    {
        HitResultText.text = rollValue.ToString();
    }

    void SetHitResult(bool result)
    {
        if (result)
        {
            HitRollText.text = $"<color=red>Hit!</color>";
        }
        else
        {
            HitRollText.text = $"<color=yellow>Miss!</color>";
        }
    }

    void SetWoundRoll(int rollValue)
    {
        if (rollValue == 0)
        {
            WoundRollText.text = "~";
        }
        else
        {
            WoundRollText.text = rollValue.ToString();
        }

    }

    void SetWoundThreshold(int threshold)
    {
        if (threshold == 0)
        {
            WoundThresholdText.text = "~";
        }
        else
        {
            WoundThresholdText.text = threshold.ToString();
        }
    }

    void SetWoundResult(bool wounded)
    {
        if (wounded)
        {
            WoundResultText.text = $"<color=red>Wounded!</color>";
        }
        else
        {
            WoundResultText.text = $"<color=yellow>Not Wounded!</color>";
        }

    }
}