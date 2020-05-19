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
    public int BumpAmount = 60;

    private int hitLines;
    private int woundLines;
    private Vector2 startRect;
    private Vector2 hitTopPos = new Vector2(0, 0);
    private Vector2 woundTopPos = new Vector2(0, 0);

    void Start()
    {
        Calculator.HitNumber.AddListener(SetHitNumber);
        Calculator.HitOutput.AddListener(SetHitResult);
        Calculator.WoundInfo.AddListener(SetWoundInfo);
        Calculator.WoundOutput.AddListener(SetWoundOutput);
        Calculator.HitScrollAdded.AddListener(BumpHitScroll);
        Calculator.WoundScrollAdded.AddListener(BumpWoundScroll);
        Calculator.StartScrolls.AddListener(ResetScroll);

        startRect = HitScroll.offsetMax;

    }

    void SetHitNumber(int hits)
    {
        HitNumText.text = $"Hits: {hits}";
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
        {
            hitTopPos.y += BumpAmount;
            HitScroll.offsetMax += new Vector2(0, BumpAmount);
            HitScroll.Translate(-hitTopPos);
        }

    }
    void BumpWoundScroll()
    {
        woundLines++;

        if (woundLines > MaxLinesBeforeScroll)
        {
            woundTopPos.y += BumpAmount;
            WoundScroll.offsetMax += new Vector2(0, BumpAmount);
            WoundScroll.Translate(-woundTopPos);
        }

    }

    void ResetScroll()
    {
        HitScroll.offsetMax = startRect;
        WoundScroll.offsetMax = startRect;
        hitLines = 0;
        woundLines = 0;
    }
}