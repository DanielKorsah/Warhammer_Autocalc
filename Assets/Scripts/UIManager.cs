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
    public TextMeshProUGUI PierceInfoText;
    public TextMeshProUGUI PierceOutputText;

    public RectTransform HitScroll;
    public RectTransform WoundScroll;
    public RectTransform PierceScroll;

    public int MaxLinesBeforeScroll = 12;
    public float BumpAmount = 60;

    private int hitLines;
    private int woundLines;
    private int pierceLines;

    private Vector2 startRectOffsetNarrow;
    private Vector2 startSizeDeltaNarrow;
    private Vector2 startRectOffsetWide;
    private Vector2 startSizeDeltaWide;
    private Vector2 hitTopPos = new Vector2(0, 0);
    private Vector2 woundTopPos = new Vector2(0, 0);
    private Vector2 pierceTopPos = new Vector2(0, 0);

    void Start()
    {
        Calculator.HitNumber.AddListener(SetHitNumber);
        Calculator.WoundInfo.AddListener(SetWoundInfo);
        Calculator.PierceInfo.AddListener(SetPierceInfo);

        Calculator.HitOutput.AddListener(SetHitResult);
        Calculator.WoundOutput.AddListener(SetWoundOutput);
        Calculator.PierceOutput.AddListener(SetPierceOutput);

        Calculator.HitScrollAdded.AddListener(BumpHitScroll);
        Calculator.WoundScrollAdded.AddListener(BumpWoundScroll);
        Calculator.PierceScrollAdded.AddListener(BumpPierceScroll);

        Calculator.StartScrolls.AddListener(ResetScroll);

        startRectOffsetNarrow = HitScroll.offsetMax;
        startSizeDeltaNarrow = HitScroll.sizeDelta;

        startRectOffsetWide = PierceScroll.offsetMax;
        startSizeDeltaWide = PierceScroll.sizeDelta;

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

    void SetPierceInfo(string pierceInfo)
    {
        PierceInfoText.text = pierceInfo;
    }

    void SetPierceOutput(string pierceData)
    {
        if (pierceData == "")
        {
            PierceOutputText.text = "~";
        }
        else
        {
            PierceOutputText.text = pierceData;
        }
    }

    void BumpHitScroll()
    {
        hitLines++;

        if (hitLines > MaxLinesBeforeScroll)
        {
            hitTopPos.y += BumpAmount;
            HitScroll.sizeDelta += new Vector2(0, BumpAmount);
            HitScroll.Translate(-hitTopPos);
        }

    }
    void BumpWoundScroll()
    {
        woundLines++;

        if (woundLines > MaxLinesBeforeScroll)
        {
            woundTopPos.y += BumpAmount;
            WoundScroll.sizeDelta += new Vector2(0, BumpAmount);
            WoundScroll.Translate(-woundTopPos);
        }

    }

    void BumpPierceScroll()
    {
        pierceLines++;

        if (pierceLines > MaxLinesBeforeScroll)
        {
            pierceTopPos.y += BumpAmount;
            PierceScroll.sizeDelta += new Vector2(0, BumpAmount);
            PierceScroll.Translate(-pierceTopPos);
        }

    }

    void ResetScroll()
    {
        HitScroll.offsetMax = startRectOffsetNarrow;
        HitScroll.sizeDelta = startSizeDeltaNarrow;
        WoundScroll.offsetMax = startRectOffsetNarrow;
        WoundScroll.sizeDelta = startSizeDeltaNarrow;
        PierceScroll.offsetMax = startRectOffsetWide;
        PierceScroll.sizeDelta = startSizeDeltaWide;
        hitLines = 0;
        woundLines = 0;
        pierceLines = 0;
    }
}