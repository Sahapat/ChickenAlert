using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject pauseObj;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseObj.SetActive(true);
            Time.timeScale = 0.01f;
        }
    }
    public void LoadScene(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }
    public void Resume()
    {
        pauseObj.SetActive(false);
        Time.timeScale = 1;
    }
}
