using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int _second = 0;
    public int second
    {
        get
        {
            return _second;
        }
        set
        {
            _second = value;
            if(_second < 0)
            {
                int temp = minute -1;
                if(temp < 0)
                {
                    _second = 0;
                    StopCount();
                }
                else
                {
                    _second = 59;
                }
                minute -= 1;
            }
        }
    }
    private int _minute = 0;
    public int minute
    {
        get
        {
            return _minute;
        }
        set
        {
            _minute = value;
            if(_minute < 0)
            {
                minute = 0;
            }
        }
    }
    private WaitForSeconds wait;
    private void Awake()
    {
        wait= new WaitForSeconds(1f);
    }
    public void StartCount(int direction)
    {
        StartCoroutine(Count(direction));
    }
    public void StopCount()
    {
        StopAllCoroutines();
    }
    private IEnumerator Count(int direction)
    {
        while(true)
        {
            second += direction;
            yield return wait;
        }
    }
}
