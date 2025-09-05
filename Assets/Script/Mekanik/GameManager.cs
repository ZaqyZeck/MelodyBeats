using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameFinish;
    [SerializeField] ScoreCounter scoreCounter;
    [SerializeField] GameObject finishPanel;

    public void FinishGame()
    {
        gameFinish = true;
        scoreCounter.SetScoreTime();
        finishPanel.SetActive(true);
    }

    
}