using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _newGame;
        [SerializeField] private Button _quit;
        [SerializeField] private string _gameScene = "GameScene";
        
        private void Awake()
        {
            _newGame.onClick.AddListener(StartGame);
            _quit.onClick.AddListener(QuitGame);
        }

        private void OnDestroy()
        {
            _newGame.onClick.RemoveListener(StartGame);
            _quit.onClick.RemoveListener(QuitGame);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(_gameScene);
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}