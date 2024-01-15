using System.Collections;
using System.Collections.Generic;
using Gameplay.Interface;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Manager
{
    public class EnviromentAdjustment : MonoBehaviour, IEnviroment
    {
        [SerializeField] Camera mainCamera;
        [SerializeField] GameObject backGround;

        private void Start()
        {
            mainCamera = Camera.main;
            SetEnviroment();
        }

        public void SetEnviroment()
        {
            // Get the orthographic size of the camera
            float orthoSize = mainCamera.orthographicSize;

            // Calculate the screen aspect ratio
            float aspectRatio = Screen.width * 1.0f / Screen.height;

            // Calculate the size of the quad based on the camera's orthographic size and aspect ratio
            float quadWidth = orthoSize * 2 * aspectRatio;
            float quadHeight = orthoSize * 2;

            // Adjust the size of the quad
            backGround.transform.localScale = new Vector3(quadWidth, quadHeight, 1f);

        }
    }
}