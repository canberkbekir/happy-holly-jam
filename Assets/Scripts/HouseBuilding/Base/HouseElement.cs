using System;
using Globals;
using UnityEngine;

namespace HouseBuilding.Base
{
    public enum HousePartType
    {
        Null,
        Floor,
        Wall,
        Roof 
    }
    public class HouseElement : MonoBehaviour
    { 
        public event Action OnPlaced;
        
        [Header("Settings")]
        [SerializeField] private HousePartType _housePartType;
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private bool _isHighlighted;
        
        [Space]
        [Header("References")]
        [SerializeField] private Renderer _renderer;
        
        private HouseElementController _houseElementController;
        private bool _isPlaced = false;
        
        public HousePartType HousePartType => _housePartType;
        
        private void Awake()
        {
            _houseElementController = GameManager.Instance.HouseElementController;
            
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
            _isPlaced = true; 
            OnPlaced?.Invoke();
        }
        
        private void OnMouseDown()
        {
           if(_houseElementController.PartType == HousePartType.Null)
           {
               return;
           }
           

           if (_houseElementController.PartType != _housePartType) return;
           UnHighlight();          

        }
       
       
    }
}
