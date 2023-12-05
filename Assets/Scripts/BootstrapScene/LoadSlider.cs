using System.Collections;
using Architecture;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BootstrapScene
{
    public class LoadSlider : MonoBehaviour
    {
        [SerializeField] 
        private Slider _sliderLoading;
        [SerializeField] 
        private float _sliderSpeed;
        [SerializeField]
        public TextMeshProUGUI _progressDownloadInPercent;

        private void Start()
        {
            _sliderLoading.maxValue = 100;
            StartCoroutine(UpdateSlider());
        }

        private IEnumerator UpdateSlider()
        {
            while (_sliderLoading.value < _sliderLoading.maxValue)
            {
                _sliderLoading.value += _sliderSpeed * Time.deltaTime;
                _progressDownloadInPercent.text = $"{Mathf.RoundToInt(_sliderLoading.value)}%";
                yield return null;
            }
            LoadScene();
        }
        
        private void LoadScene()
        {
            SceneManager.LoadScene(GlobalConstants.GAME);
        }
    }
}
