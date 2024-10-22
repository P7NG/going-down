using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace GameMenu
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject PausePanel;
        private AudioSource[] audioSources;
        public GameController gameController;
        public GameObject mainMenu;

        public bool IsPause = false;
        public GameObject MobileUI;
        private bool _isMobile = true;

        void Start()
        {
            _isMobile = !YandexGame.EnvironmentData.isDesktop;
        }

        

        public void Pause()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            if (_isMobile) MobileUI.SetActive(false);
            PauseAllAudio();
        }

        public void Resume()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            if (_isMobile) MobileUI.SetActive(true);
            ResumeAllAudio();
        }

        public void MainMenu()
        {
            gameController.Spawn();
            Time.timeScale = 1;
        }

        public void Restart()
        {
            gameController.Spawn();
            Time.timeScale = 1;
            Resume();
        }

        private void PauseAllAudio()
        {
            // Update audio sources every time pause is called
            audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audio in audioSources)
            {
                audio.Pause();
            }
        }

        private void ResumeAllAudio()
        {
            // Update audio sources every time resume is called
            audioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource audio in audioSources)
            {
                audio.UnPause();
            }
        }

        public void OpenMenu()
        {
            mainMenu.SetActive(true);
            gameController.Delete();
            Resume();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PausePanel.SetActive(false);
        }
    }
}
