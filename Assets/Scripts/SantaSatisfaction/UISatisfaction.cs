using System;
using Globals;
using UnityEngine;
using UnityEngine.UI;

namespace SantaSatisfaction
{
    public class UISatisfaction : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] private TMPro.TextMeshProUGUI satisfactionText;
        [SerializeField] private Slider satisfactionSlider;
        
        private SatisfactionController _satisfactionController;

        private void Awake()
        {
            _satisfactionController = GameManager.Instance.SatisfactionController;
            
            if (_satisfactionController == null)
            {
                Debug.LogError("_satisfactionController is null!");
            }
        }

        private void Start()
        {
            satisfactionSlider.minValue = 0;
            satisfactionSlider.maxValue = _satisfactionController.MaxSatisfaction;
            
            _satisfactionController.OnSatisfactionChanged += UpdateSatisfaction; 
        }

        private void UpdateSatisfaction(float satisfaction)
        {
            satisfactionSlider.value = satisfaction;
            satisfactionText.text = $"%{(int)_satisfactionController.SatisfactionPercentage}";
        }
        
    }
}
