using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private GameObject _mobileUI;
    [SerializeField] private GameController _gameController;
    [SerializeField] private AudioMixer _mixer;

    private bool _isMobile;
    private void Awake()
    {
        YandexGame.GameReadyAPI();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _isMobile = !YandexGame.EnvironmentData.isDesktop;
        GetData();
        _gameController.IsMobile = _isMobile;
    }

    private void GetData()
    {
        _volumeSlider.value = YandexGame.savesData.Volume;
        _mixer.SetFloat("All", _volumeSlider.value);
        Debug.Log(YandexGame.SDKEnabled);
    }

    public void Play()
    {
        if (_isMobile)
        {
            _mobileUI.SetActive(true);
        }
        else
        {
            _mobileUI.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _gameController.Spawn();
        gameObject.SetActive(false);
    }

    public void ChangeVolume()
    {
        _mixer.SetFloat("All", _volumeSlider.value);
        YandexGame.savesData.Volume = _volumeSlider.value;
        YandexGame.SaveProgress();
    }
}
