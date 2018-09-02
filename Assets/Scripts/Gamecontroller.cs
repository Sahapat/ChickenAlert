using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
public class Gamecontroller : MonoBehaviour
{
    [SerializeField] private Timer m_timer;
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private GameObject chickenObj;
    [SerializeField] private WolfController m_wolfcontroller;
    [SerializeField] private ChickenController m_chickencontroller;
    [SerializeField] private GameObject Intro;
    [SerializeField] private UIHandler uIHandler;

    [Serializable]
    private class SpawnChickenFrame
    {
        public Vector2 min = Vector2.zero;
        public Vector2 max = Vector2.zero;
    }
    [SerializeField] private SpawnChickenFrame spawnChickenFrame;
    public bool IsGameStart;
    private int maxWolfPerTime = 0;
    private float wolfTiming = 0;
    private float wolfCount = 0;
    public int currentWolf;
    private void Awake()
    {
        IsGameStart = false;
        player1.setProperty(Global.player1Selected);
        player2.setProperty(Global.player2Selected);
    }
    private void Start()
    {
        m_timer.minute = 3;
        m_timer.second = 0;
        for (int i = 0; i < Global.chickenRemain; i++)
        {
            float spawnX = UnityEngine.Random.Range(spawnChickenFrame.min.x, spawnChickenFrame.max.x);
            float spawnZ = UnityEngine.Random.Range(spawnChickenFrame.min.y, spawnChickenFrame.max.y);

            Instantiate(chickenObj, new Vector3(spawnX, 1, spawnZ), Quaternion.identity);
        }
        maxWolfPerTime = 2 + (int)(Global.Day * 1.2f);
        wolfTiming = 20 - (int)(Global.Day * 2);
        StartCoroutine(introShow());
    }
    private void Update()
    {
        if(IsGameStart)
        {
            if((m_timer.second <= 0 && m_timer.minute <= 0) ||(m_chickencontroller.LandChicken.Count <= 0) || (m_chickencontroller.currentChickCount < 8))
            {
                uIHandler.EndGame(m_chickencontroller.FenceChicken.Count >= 8);
                m_chickencontroller.SetStop();
            }
            if(currentWolf < maxWolfPerTime)
            {
            if (wolfCount < Time.time)
            {
                m_wolfcontroller.SpawnWolf();
                currentWolf++;
                wolfCount = Time.time + wolfTiming;
            }
            }

            if (currentWolf != 0)
            {
                m_chickencontroller.isHaveWolf = true;
            }
        }
    }
    public void GameStart()
    {
        m_timer.StartCount(-1);
        IsGameStart = true;
        Intro.SetActive(false);
    }
    private IEnumerator introShow()
    {
        wolfCount = Time.time + wolfTiming;
        yield return new WaitForSeconds(1.5f);
        GameStart();
    }
}
