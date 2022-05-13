using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject FinishPanel;
    public GameObject MenuPanel;

    public Text ScoreText;
    public Text FinishScoreText;

    private void OnEnable()
    {
        Actions.OnScoreText += SetGameplayScoreText;
        Actions.OnScoreFinishText += SetFinishScoreText;
        Actions.OnGameDone += GameDone;
    }

    private void OnDisable()
    {
        Actions.OnScoreText -= SetGameplayScoreText;
        Actions.OnScoreFinishText -= SetFinishScoreText;
        Actions.OnGameDone -= GameDone;
    }

    private void Start()
    {
        MenuPanel.SetActive(true);
    }

    private void GameDone()
    {
        ScoreText.gameObject.SetActive(false);
        FinishPanel.SetActive(true);
    }

    public void SetGameplayScoreText(int value)
    {
        ScoreText.text = "Score : " + value;
    }
    
    public void SetFinishScoreText(int value)
    {
        FinishScoreText.text = "Total Score : " + value;
    }

    #region Button 
        public void GameStartButton()
        {
            MenuPanel.SetActive(false);
            ScoreText.text = "Score : " + 0;
            ScoreText.gameObject.SetActive(true);
            
            Actions.OnGameStart?.Invoke();
        }

        public void RetryButton()
        {
            SceneManager.LoadScene("SampleScene");
        }
    #endregion
    
}
