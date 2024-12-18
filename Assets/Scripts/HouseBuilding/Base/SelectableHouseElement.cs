using System;
using UnityEngine;

namespace HouseBuilding.Base
{
    public class SelectableHouseElement : MonoBehaviour
    {  
        public event Action<HousePartType> OnElementClicked;
        
        [Header("Settings")]
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private bool _isHighlighted;
        [SerializeField] private Renderer _renderer; 
        [SerializeField] private HousePartType _housePartType;
        
        private void Awake()
        {
            if (_isHighlighted)
            {
                Highlight();
            }
            else
            {
                UnHighlight(); 
            }
        }
        
        public void Highlight()
        { 
            _renderer.material = highlightMaterial; 
        }
        
        public void UnHighlight()
        {
            _renderer.material = defaultMaterial;
        }

        private void OnMouseDown()
        {
            OnElementClicked?.Invoke(_housePartType);
        }

        private void OnMouseEnter()
        {
            Highlight();
        }
        
        private void OnMouseExit()
        {
            UnHighlight();
        }
    }
}
