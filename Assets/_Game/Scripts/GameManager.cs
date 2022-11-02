using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private string gameplayScene;
        private void Awake()
        {
            Instance = this;
        }
        public void QuitGame()
        {
            Application.Quit();
        }

        public void StartGame()
        {
            SceneManager.LoadScene(gameplayScene);
        }
    }