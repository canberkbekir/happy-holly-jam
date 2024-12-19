using System;
using Globals;
using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject playingUI;
    [SerializeField] private GameObject gameOverUI;

    private void OnEnable()
    {
        GameManager.Instance.SatisfactionController.OnSatisfactionIsZero += GameOver;
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
        playingUI.SetActive(false);
    }
    
    private void Start()
    {
        mainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void PlayGame()
    {
        mainMenuUI.SetActive(false);
        playingUI.SetActive(true);
    }
    
    public void RestartGame()
    {
        // Canberk DO IT!!!
        
        gameOverUI.SetActive(false);
        playingUI.SetActive(true);
    }
    
}
