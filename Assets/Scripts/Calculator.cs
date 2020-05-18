using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEvent : UnityEvent<int> {}
public class StringEvent : UnityEvent<string> {}

public class Calculator : MonoBehaviour
{

    public static IntEvent HitNumber = new IntEvent();
    public static StringEvent HitOutput = new StringEvent();
    public static StringEvent WoundInfo = new StringEvent();
    public static StringEvent WoundOutput = new StringEvent();

    public static UnityEvent HitScrollAdded = new UnityEvent();
    public static UnityEvent WoundScrollAdded = new UnityEvent();

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

    public void Calculate(int ballistic, int strength, int toughness, int shots)
    {
        CheckHit(ballistic, strength, toughness, shots);
    }

    void CheckHit(int ballistic, int strength, int toughness, int shots)
    {
        string hitOutput = "";
        int hitCount = 0;

        for (int i = 0; i < shots; i++)
        {
            int roll = Random.Range(1, 6);
            if (roll > ballistic)
            {
                hitCount++;
                hitOutput += $"Hit: roll = {roll}\n";
            }
            else
            {
                hitOutput += $"Miss: roll = {roll}\n";
            }
        }

        HitScrollAdded.Invoke();
        HitNumber.Invoke(hitCount);
        HitOutput.Invoke(hitOutput);

        CalculateWounds(strength, toughness, hitCount);
    }

    void CalculateWounds(int strength, int toughness, int hits)
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
            int roll = Random.Range(1, 6);

            if (roll >= threshold)
            {
                woundCount++;
                WoundScrollAdded.Invoke();
                woundOutput += $"Wounded: roll = {roll}\n";
            }
            else
            {
                woundOutput += $"No : roll = {roll}\n";
            }
        }
        woundInfo = $"Wound Threshold: {threshold}\nWounds: {woundCount}";
        WoundInfo.Invoke(woundInfo);
        WoundOutput.Invoke(woundOutput);

    }

}