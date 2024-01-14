using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Manager
{
    public class CameraOffset : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        private void Start()
        {
            mainCamera = Camera.main;
        }

        public void SetCameraOffset(int totalNoOfPlayers)
        {
            mainCamera.orthographicSize = (totalNoOfPlayers / 2) + 2;
        }
    }
}