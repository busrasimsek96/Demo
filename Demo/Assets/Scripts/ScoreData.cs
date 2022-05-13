using UnityEngine;

public class ScoreData : MonoBehaviour
{
    public int score = 0;

    private void OnEnable()
    {
        Actions.OnDiamondValue += ScoreIncrease;
    }

    private void OnDisable()
    {
        Actions.OnDiamondValue -= ScoreIncrease;
    }

    public void ScoreIncrease(int diamondValue)
    {
        score += diamondValue;
        Actions.OnScoreText?.Invoke(score);
        
        if (score == 100)
        {
            Actions.OnScoreFinishText?.Invoke(score);
            Actions.OnGameDone?.Invoke();
        }
        
    }

    public void ScoreReset()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }
    
    
}
