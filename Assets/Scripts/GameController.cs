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

    private void Start()
    {
        
        _carInGame = null;
       
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Spawn();
        }
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

        Spawn();
    }
}
