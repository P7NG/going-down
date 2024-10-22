using UnityEngine;
using CarContollingScripts;
namespace MobileInputForCar
{


    public class MobileInput : MonoBehaviour
    {
        public CarController carController;
        public GameMenu.PauseMenu pauseMenu;

        public void OnBrakeButtonDown()
        {
            carController.BrakeInput(true);
        }

        public void OnBrakeButtonUp()
        {
            carController.BrakeInput(false);
        }

        // Add similar methods for MoveInput and SteerInput if needed

        public void CarControllerMove(int direction)
        {
            carController.MoveInput(direction);
        }

        public void CarCOntrollerRotate(int direction)
        {
            carController.SteerInput(direction);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && carController != null)
            {
                if (!pauseMenu.PausePanel.activeInHierarchy)
                {
                    pauseMenu.Pause();
                }
                else
                {
                    pauseMenu.Resume();
                }
            }
        }

    }
}
