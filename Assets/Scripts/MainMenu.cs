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
    private void Start()
    {
        YandexGame.GameReadyAPI();
        _isMobile = !YandexGame.EnvironmentData.isDesktop;
        GetData();
    }

    private void GetData()
    {
        _volumeSlider.value = YandexGame.savesData.Volume;
        _mixer.SetFloat("All", _volumeSlider.value);
    }

    public void Play()
    {
        if (_isMobile)
        {
            _mobileUI.SetActive(true);
        }

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
