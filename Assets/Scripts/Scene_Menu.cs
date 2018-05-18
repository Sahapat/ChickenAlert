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
        Global.player1Id = 0;
        Global.player2Id = 0;
        Global.Day = 1;
        Global.chickenRemain = 16;
        Global.player1Isjoy = false;
        Global.player2Isjoy = false;
    }
    private void Update()
    {
        if(!isStart)
        {
            if(Input.anyKey)
            {
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
