using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GameController : MonoBehaviour
{
    public Transform[] SpawnPlaces;
    public int CurrentSpawnPlace = 0;
    public CarContollingScripts.CarController[] Cars;
    public int CurrentCar = 0;
    public TakeChildrenPosition CameraFollowScript;

    public CarContollingScripts.CarController _carInGame;
    public bool RandomPhase = false;

    public bool IsMobile;
    public MobileInputForCar.MobileInput CanvasInputScript;
    public MainMenu mainMenu;

    public void CloseMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        
        _carInGame = null;

        CurrentCar = YandexGame.savesData.CurrentCar;
        CurrentSpawnPlace = YandexGame.savesData.CurrentSpawnPlace;
        RandomPhase = YandexGame.savesData.RandomPhase;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Spawn();
        }
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            mainMenu.ChangeSoundScript(-80);
        }
        else
        {
            mainMenu.ChangeSoundScript(YandexGame.savesData.Volume);
        }
    }

    public void Delete()
    {
        Destroy(_carInGame.gameObject);
    }

    private IEnumerator InvisibleMouse()
    {
        yield return new WaitForSeconds(0.5f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Spawn()
    {
        if (_carInGame != null)
        {
            Destroy(_carInGame.gameObject);
        }
        GameObject car = Instantiate(Cars[CurrentCar].gameObject, SpawnPlaces[CurrentSpawnPlace].position, Quaternion.identity);
        _carInGame = car.GetComponent<CarContollingScripts.CarController>();
        CameraFollowScript.target = _carInGame.FollowCameraObject;
        _carInGame.Controller = this;
        CanvasInputScript.carController = _carInGame;
        _carInGame.gameController = this;
        YandexGame.FullscreenShow();
    }

    public void CursorControll()
    {
        StartCoroutine(InvisibleMouse());
    }

    public void NextLevel()
    {
        if (!RandomPhase)
        {
            if(CurrentCar == 4)
            {
                CurrentCar = 0;

                if(CurrentSpawnPlace == 4)
                {
                    RandomPhase = true;
                }
                else
                {
                    CurrentSpawnPlace++;
                }
            }
            else
            {
                CurrentCar++;
            }
        }
        else
        {
            CurrentSpawnPlace = Random.RandomRange(0, 4);
            CurrentCar = Random.RandomRange(0, 4);
        }

        YandexGame.savesData.CurrentCar = CurrentCar;
        YandexGame.savesData.CurrentSpawnPlace = CurrentSpawnPlace;
        YandexGame.savesData.RandomPhase = RandomPhase;
        YandexGame.savesData.Scores++;
        YandexGame.NewLeaderboardScores("Scores", YandexGame.savesData.Scores);
        YandexGame.SaveProgress();

        YandexGame.FullscreenShow();

        Spawn();
    }
}
