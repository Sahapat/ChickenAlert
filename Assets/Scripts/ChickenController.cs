using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    [SerializeField] private List<Chicken> LandChicken;
    [SerializeField] private List<Chicken> FenceChicken;
    [SerializeField] private int startChickenCount;
    public int currentChickCount;
    public bool setAlert;
    private void Awake()
    {
        var temp = FindObjectsOfType<Chicken>();
        startChickenCount = temp.Length;
        LandChicken.AddRange(temp);
    }
    private void Update()
    {
        CheckChickenStatus();
        if (setAlert)
        {
            SetAlert();
        }
    }
    private void SetAlert()
    {
        for (int i = 0; i < LandChicken.Count; i++)
        {
            LandChicken[i].isAlert = true;
        }
    }
    private void CheckChickenStatus()
    {
        for (int i = 0; i < LandChicken.Count; i++)
        {
            try
            {
                if (LandChicken[i].isSafe)
                {
                    FenceChicken.Add(LandChicken[i]);
                    LandChicken.RemoveAt(i);
                }
            }
            catch(NullReferenceException)
            {
                LandChicken.RemoveAt(i);
            }
        }
        for (int i = 0; i < FenceChicken.Count; i++)
        {
            try
            {
                if (!FenceChicken[i].isSafe)
                {
                    LandChicken.Add(FenceChicken[i]);
                    FenceChicken.RemoveAt(i);
                }
            }
            catch(NullReferenceException)
            {
                FenceChicken.RemoveAt(i);
            }
        }
    }
}
