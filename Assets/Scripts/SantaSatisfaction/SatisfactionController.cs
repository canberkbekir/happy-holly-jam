using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SantaSatisfaction
{
    public class SatisfactionController : MonoBehaviour
    {
        #region Events

        public event Action<float> OnSatisfactionChanged;
        public event Action OnSatisfactionIsZero;
        public event Action OnSatisfactionIsMax;

        #endregion

        #region Properties

        public float SatisfactionPercentage => (_currentSatisfaction / maxSatisfaction) * 100;
        public float MaxSatisfaction => maxSatisfaction;

        #endregion

        [Header("Satisfaction Settings")]
        [SerializeField] private float maxSatisfaction = 100f;
        [SerializeField] private float multiplerOfDecreaseSatisfaction = 1f;
        [SerializeField] private float multiplerOfIncreaseSatisfaction = 10f;

        [Space]
        [Header("Interval Settings")]
        [SerializeField] private float decreaseInterval = 3f; // Editable in Inspector

        [Space]
        [Header("Debug Settings")]
        [SerializeField] private bool debugMode = false;
        [SerializeField] private Key debugIncreaseKey = Key.F;

        private float _currentSatisfaction;
        private bool _isPaused;

        private void Awake()
        {
            _currentSatisfaction = maxSatisfaction;
        }

        private void Start()
        {
            OnSatisfactionChanged?.Invoke(_currentSatisfaction);
        }

        private void Update()
        {
            if (debugMode && Keyboard.current[debugIncreaseKey].wasPressedThisFrame)
            {
                IncreaseSatisfaction();
            }
        }

        private void FixedUpdate()
        {
            if (!_isPaused)
            {
                DecreaseSatisfaction();
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void DecreaseSatisfaction()
        {
            _currentSatisfaction -= multiplerOfDecreaseSatisfaction * Time.fixedDeltaTime;
            _currentSatisfaction = Mathf.Clamp(_currentSatisfaction, 0, maxSatisfaction);
            OnSatisfactionChanged?.Invoke(_currentSatisfaction);

            if (_currentSatisfaction <= 0)
                OnSatisfactionIsZero?.Invoke();
        }

        public void IncreaseSatisfaction()
        {
            _currentSatisfaction += multiplerOfIncreaseSatisfaction;
            _currentSatisfaction = Mathf.Clamp(_currentSatisfaction, 0, maxSatisfaction);
            OnSatisfactionChanged?.Invoke(_currentSatisfaction);

            if (_currentSatisfaction >= maxSatisfaction)
                OnSatisfactionIsMax?.Invoke();

            // Pause decreasing for the decreaseInterval duration
            StartCoroutine(PauseDecreasingCoroutine());
        }

        private System.Collections.IEnumerator PauseDecreasingCoroutine()
        {
            _isPaused = true;
            yield return new WaitForSeconds(decreaseInterval);
            _isPaused = false;
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                StopAllCoroutines();
                _isPaused = false;
            }
        }

     
    }
}
