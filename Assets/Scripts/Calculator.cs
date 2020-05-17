using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEvent : UnityEvent<int> {}
public class BoolEvent : UnityEvent<bool> {}

public class Calculator : MonoBehaviour
{

    public static IntEvent HitValue = new IntEvent();
    public static BoolEvent HitResult = new BoolEvent();
    public static IntEvent WoundValue = new IntEvent();
    public static IntEvent WoundThreshold = new IntEvent();
    public static BoolEvent WoundResult = new BoolEvent();

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

    public void Calculate(int ballistic, int strength, int toughness)
    {
        CheckHit(ballistic, strength, toughness);
    }

    void CheckHit(int ballistic, int strength, int toughness)
    {

        bool result = false;
        int roll = Random.Range(1, 6);
        HitValue.Invoke(roll);

        if (roll > ballistic)
        {
            result = true;
        }
        else
        {
            result = false;
        }

        HitResult.Invoke(result);

        CalculateWounds(strength, toughness, result);
    }

    void CalculateWounds(int strength, int toughness, bool hit)
    {
        int threshold = 0;
        if (!hit)
        {
            WoundValue.Invoke(0);
            WoundThreshold.Invoke(0);
            WoundResult.Invoke(false);
        }
        else
        {
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

            int roll = Random.Range(1, 6);
            WoundValue.Invoke(roll);
            WoundThreshold.Invoke(threshold);

            if (roll >= threshold)
                WoundResult.Invoke(true);
            else
                WoundResult.Invoke(false);
        }
    }

}