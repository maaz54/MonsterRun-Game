using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] Image loadingImage;

        public void StartLoading()
        {
            gameObject.SetActive(true);
        }

        public void StopLoading()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            loadingImage!.transform.Rotate(-Vector3.forward * 100 * Time.deltaTime);
        }
    }
}
