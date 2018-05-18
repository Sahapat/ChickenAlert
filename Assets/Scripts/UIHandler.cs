using UnityEngine.UI;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Text txt_day;
    [SerializeField] private Text txt_time;
    [SerializeField] private Text txt_chickenRequire;
    [SerializeField] private Sprite[] items;
    [SerializeField] private Image imgItemSlot1;
    [SerializeField] private Image imgItemSlot2;
    [SerializeField] private Timer m_timer;

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private ChickenController m_chickenController;
    [SerializeField] private GameObject endGame;
    [SerializeField] private Text endGameSelect1;
    [SerializeField] private Text txt_Result;
    [SerializeField] private Text txt_day_result;

    private bool isWinner;
    private void Update()
    {
        string txtsecond = (m_timer.second < 10) ? "0" + m_timer.second.ToString() : m_timer.second.ToString();
        txt_time.text = m_timer.minute + ":" + txtsecond;
        txt_day.text = "Day " + Global.Day;
        updateItemSlot();
        updateRequireChicken();
    }
    private void updateItemSlot()
    {
        if (player1.inventory != null)
        {
            imgItemSlot1.enabled = true;
            switch (player1.inventory.itemType)
            {
                case ItemType.Bomb:
                    imgItemSlot1.sprite = items[0];
                    break;
                case ItemType.Boots:
                    imgItemSlot1.sprite = items[1];
                    break;
                case ItemType.Hay:
                    imgItemSlot1.sprite = items[2];
                    break;
                case ItemType.Plank:
                    imgItemSlot1.sprite = items[3];
                    break;
            }
        }
        else
        {
            imgItemSlot1.enabled = false;
        }

        if (player2.inventory != null)
        {
            imgItemSlot2.enabled = true;
            switch (player2.inventory.itemType)
            {
                case ItemType.Bomb:
                    imgItemSlot2.sprite = items[0];
                    break;
                case ItemType.Boots:
                    imgItemSlot2.sprite = items[1];
                    break;
                case ItemType.Hay:
                    imgItemSlot2.sprite = items[2];
                    break;
                case ItemType.Plank:
                    imgItemSlot2.sprite = items[3];
                    break;
            }
        }
        else
        {
            imgItemSlot2.enabled = false;
        }
    }
    public void Continue()
    {
        if (isWinner)
        {
            Global.chickenRemain = m_chickenController.FenceChicken.Count;
            Global.Day++;
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else
        {
            Global.chickenRemain = 20;
            Global.Day = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
    public void EndGame(bool isWinner)
    {
        endGame.SetActive(true);
        this.isWinner = isWinner;
        if(isWinner)
        {
            endGameSelect1.color = Color.green;
            endGameSelect1.text = "Next day";
        }
        else
        {
            endGameSelect1.color = Color.red;
            endGameSelect1.text = "Retry";
        }
        txt_day_result.text = "Day " + Global.Day;
    }
    private void updateRequireChicken()
    {
        string txtRequire = m_chickenController.FenceChicken.Count + "/" + "8";
        txt_chickenRequire.text = txtRequire;
        txt_Result.text = txtRequire;
    }
}
