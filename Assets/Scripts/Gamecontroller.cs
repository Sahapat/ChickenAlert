using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamecontroller : MonoBehaviour
{
    [SerializeField] private Timer m_timer;
    public bool IsGameStart { get; private set; }
    private void Start()
    {
    }
}
