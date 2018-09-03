using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scene_Menu : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private bool isStart;
    private void Start()
    {
        Global.Day = 1;
        Global.chickenRemain = 20;
        Global.player1Selected = ControllerSelector.None;
        Global.player2Selected = ControllerSelector.None;
    }
    private void Update()
    {
        if(!isStart)
        {
            if(Input.anyKey)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                    return;
                }
                StartCoroutine(LoadScene());
                isStart = true;
            }
        }
    }
    private IEnumerator LoadScene()
    {
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
