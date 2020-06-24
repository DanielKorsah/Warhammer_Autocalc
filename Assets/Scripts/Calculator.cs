using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEvent : UnityEvent<int> {}
public class StringEvent : UnityEvent<string> {}

public class Calculator : MonoBehaviour
{

    public static StringEvent HitInfo = new StringEvent();
    public static StringEvent HitOutput = new StringEvent();
    public static StringEvent WoundInfo = new StringEvent();
    public static StringEvent WoundOutput = new StringEvent();
    public static StringEvent PierceInfo = new StringEvent();
    public static StringEvent PierceOutput = new StringEvent();

    public static UnityEvent HitScrollAdded = new UnityEvent();
    public static UnityEvent WoundScrollAdded = new UnityEvent();
    public static UnityEvent PierceScrollAdded = new UnityEvent();
    public static UnityEvent StartScrolls = new UnityEvent();

    //singleton stuff
    private static Calculator _instance;

    public static Calculator Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    //end singleton stuff

    public void Calculate(int ballistic, int strength, int toughness, int shots, int piercing, int armour)
    {
        CheckHit(ballistic, strength, toughness, shots, piercing, armour);
    }

    void CheckHit(int ballistic, int strength, int toughness, int shots, int piercing, int armour)
    {
        StartScrolls.Invoke();

        string hitOutput = "";
        int hitCount = 0;
        string hitInfo = $"Hit Threshold: {ballistic}\nHits: ~";

        for (int i = 0; i < shots; i++)
        {
            int roll = Random.Range(1, 7);
            if (roll > ballistic)
            {
                hitCount++;
                hitOutput += $"<color=red>Hit</color>\t\troll = {roll}\n";
            }
            else
            {
                hitOutput += $"<color=yellow>Miss</color>\t\troll = {roll}\n";
            }
            HitScrollAdded.Invoke();
        }

        hitInfo = $"Hit Threshold: {ballistic}\nHits: {hitCount}";
        HitInfo.Invoke(hitInfo);
        HitOutput.Invoke(hitOutput);

        CalculateWounds(strength, toughness, hitCount, piercing, armour);
    }

    void CalculateWounds(int strength, int toughness, int hits, int piercing, int armour)
    {
        int threshold = 0;
        int woundCount = 0;
        string woundInfo = "Wound Threshold: ~\nWounds: ~";
        string woundOutput = "";

        if (strength >= (toughness * 2))
            threshold = 2;
        else if (strength > toughness && strength < (toughness * 2))
            threshold = 3;
        else if (strength == toughness)
            threshold = 4;
        else if (strength > (toughness / 2) && strength < toughness)
            threshold = 5;
        else if (strength < toughness / 2)
            threshold = 6;

        for (int i = 0; i < hits; i++)
        {
            int roll = Random.Range(1, 7);

            if (roll >= threshold)
            {
                woundCount++;
                woundOutput += $"<color=red>Wounded</color>\troll = {roll}\n";
            }
            else
            {
                woundOutput += $"<color=yellow>No Wound</color>\troll = {roll}\n";
            }
            WoundScrollAdded.Invoke();
        }
        woundInfo = $"Wound Threshold: {threshold}\nWounds: {woundCount}";
        WoundInfo.Invoke(woundInfo);
        WoundOutput.Invoke(woundOutput);

        CalculateArmourPiercing(woundCount, piercing, armour);

    }

    void CalculateArmourPiercing(int woundsCount, int piercing, int armour)
    {
        int threshold = armour + piercing;
        int pierceCount = 0;
        string pierceInfo = $"Pierce Save Threshold: ≥{threshold}\nArmour Pierced: ~";
        string pierceOutput = "";

        for (int i = 0; i < woundsCount; i++)
        {
            int roll = Random.Range(1, 7);

            if (roll < threshold)
            {
                pierceCount++;
                pierceOutput += $"<color=red>No Save! Armour Pierced!</color>\troll = {roll}\n";
            }
            else
            {
                pierceOutput += $"<color=yellow>Saved! Failed to pierce!</color>\t\troll = {roll}\n";
            }
            PierceScrollAdded.Invoke();
        }

        pierceInfo = $"Pierce Save Threshold: ≥{threshold}\nArmour Pierced: {pierceCount}";
        PierceInfo.Invoke(pierceInfo);
        PierceOutput.Invoke(pierceOutput);
    }

}