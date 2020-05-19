using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI HitNumText;
    public TextMeshProUGUI HitsOutputText;
    public TextMeshProUGUI WoundInfoText;
    public TextMeshProUGUI WoundOutputText;

    public RectTransform HitScroll;
    public RectTransform WoundScroll;

    public int MaxLinesBeforeScroll = 12;

    private int hitLines;
    private int woundLines;

    void Start()
    {
        Calculator.HitNumber.AddListener(SetHitNumber);
        Calculator.HitOutput.AddListener(SetHitResult);
        Calculator.WoundInfo.AddListener(SetWoundInfo);
        Calculator.WoundOutput.AddListener(SetWoundOutput);
        Calculator.HitScrollAdded.AddListener(BumpHitScroll);
        Calculator.WoundScrollAdded.AddListener(BumpWoundScroll);

    }

    void SetHitNumber(int hits)
    {
        HitNumText.text = hits.ToString();
    }

    void SetHitResult(string result)
    {
        HitsOutputText.text = result;
    }

    void SetWoundInfo(string woundInfo)
    {

        WoundInfoText.text = woundInfo;

    }

    void SetWoundOutput(string wounds)
    {
        if (wounds == "")
        {
            WoundOutputText.text = "~";
        }
        else
        {
            WoundOutputText.text = wounds;
        }

    }

    void BumpHitScroll()
    {
        hitLines++;

        if (hitLines > MaxLinesBeforeScroll)
            HitScroll.offsetMax += new Vector2(0, 45);
    }
    void BumpWoundScroll()
    {
        woundLines++;

        if (woundLines > MaxLinesBeforeScroll)
            WoundScroll.offsetMax += new Vector2(0, 45);
    }
}