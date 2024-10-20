using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform[] SpawnPlaces;
    public int CurrentSpawnPlace = 0;
    public CarContollingScripts.CarController[] Cars;
    public int CurrentCar = 0;
    public TakeChildrenPosition CameraFollowScript;

    public CarContollingScripts.CarController _carInGame;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(_carInGame.gameObject);
            GameObject car = Instantiate(Cars[CurrentCar].gameObject, SpawnPlaces[CurrentSpawnPlace].position, Quaternion.identity);
            _carInGame = car.GetComponent<CarContollingScripts.CarController>();
            CameraFollowScript.target = _carInGame.FollowCameraObject;
        }
    }
}
