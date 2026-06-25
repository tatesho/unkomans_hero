using System.Collections;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private TextMeshProUGUI normalBlockText;
    [SerializeField] private TextMeshProUGUI fallBlockText;
    [SerializeField] private TextMeshProUGUI blackHoleText;
    [SerializeField] private GameObject infoUI;
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private PlayerContoroller playerContoroller;

    public int normalBlockQuantity = 1;
    public int fallBlockQuantity = 1;
    public int blackHoleQuantity = 1;
    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        StartCoroutine(PlayIntro());
        endText.text = "";
}
    private IEnumerator PlayIntro()
    {
        playerContoroller.canControl = false;
        Time.timeScale = 0.0f;
        ShowStageInfo();
        yield return new WaitForSecondsRealtime(2.0f);
        infoUI.SetActive(false);
        Time.timeScale = 1.0f;
        playerContoroller.canControl = true;
    }
    private void ShowStageInfo()
    {
        normalBlockText.text = "Å~" + normalBlockQuantity;
        fallBlockText.text = "Å~" + fallBlockQuantity;
        blackHoleText.text = "Å~" + blackHoleQuantity;
    }
    public void GoalShowText()
    {
        endText.text = "Goal!!!";
    }
    public void GameOverShowText()
    {
        endText.text = "Game Over...";
    }
}
