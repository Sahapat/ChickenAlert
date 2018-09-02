using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class ChickenController : MonoBehaviour
{
    public List<Chicken> LandChicken;
    public List<Chicken> FenceChicken;
    [SerializeField] private Gamecontroller m_gamecontroller;
    public int currentChickCount;
    public bool isHaveWolf;
    public bool setDead;
    private bool isSet;
    private float clamCounter;
    private void Update()
    {
        if (!m_gamecontroller.IsGameStart && !isSet)
        {
            var temp = FindObjectsOfType<Chicken>();
            LandChicken.AddRange(temp);
            isSet = true;
        }
        else
        {
            CheckChickenStatus();
            if (clamCounter < Time.time && isHaveWolf)
            {
                SetAlert();
            }
            else
            {
                SetClam();
            }
        }
    }
    public void SetClam(float endTime)
    {
        clamCounter = endTime;
        for (int i = 0; i < LandChicken.Count; i++)
        {
            LandChicken[i].isAlert = false;
        }
    }
    public void SetClam()
    {
        for (int i = 0; i < LandChicken.Count; i++)
        {
            LandChicken[i].isAlert = false;
        }
    }
    public void SetStop()
    {
        for (int i = 0; i < LandChicken.Count; i++)
        {
            LandChicken[i].transform.parent.GetComponent<NavMeshAgent>().isStopped = true;
        }
        for (int i = 0; i < FenceChicken.Count; i++)
        {
            FenceChicken[i].transform.parent.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }
    public GameObject GetRandomChickenOnLand()
    {
        GameObject temp = null;
        try
        {
            temp = LandChicken[UnityEngine.Random.Range(0, LandChicken.Count)].gameObject;
        }
        catch(ArgumentOutOfRangeException)
        {
            temp = null;
        }
        return temp;
    }
    public void RemoveChicken(int id)
    {
        for(int i =0;i<LandChicken.Count;i++)
        {
            if(LandChicken[i].GetInstanceID() == id)
            {
                LandChicken.RemoveAt(i);
                return;
            }
        }
        for(int i = 0;i<FenceChicken.Count;i++)
        {
            if(FenceChicken[i].GetInstanceID() == id)
            {
                FenceChicken.RemoveAt(i);
                return;
            }
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
        currentChickCount = LandChicken.Count + FenceChicken.Count;
    }
}
