using UnityEngine.UI;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Text txt_time;
    [SerializeField] private Timer m_timer;
    private void Update()
    {
        string txtsecond = (m_timer.second < 10) ? "0"+m_timer.second.ToString(): m_timer.second.ToString();
        txt_time.text = m_timer.minute + ":"+txtsecond;
    }
}
