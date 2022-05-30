using CrazyShooter.Core;
using UnityEngine;

namespace CrazyShooter.CameraController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera currentCamera;
        [SerializeField] private float cameraSpeed;
        public PlayerView Player { get; set; }
        private Vector3 CamPos => currentCamera.transform.position;

        private void FixedUpdate()
        {
            if (Player == null)
                return;

            var newPos = Vector2.Lerp(CamPos, Player.transform.position, Time.deltaTime * cameraSpeed);
            currentCamera.transform.position = new Vector3(newPos.x, newPos.y, CamPos.z);
        }
    }
    
}
