using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    /// <summary>
    /// Represents a loading screen with a rotating image.
    /// </summary>
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] Image loadingImage;

        /// <summary>
        /// Activates the loading screen.
        /// </summary>
        public void StartLoading()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Deactivates the loading screen.
        /// </summary>
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
