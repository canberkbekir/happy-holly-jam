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
        [SerializeField] private bool _isHighlighted;
        
        [Space]
        [Header("References")] 
        [SerializeField] private GameObject highlightGameObject;
        [SerializeField] private GameObject defaultGameObject;
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
            highlightGameObject.SetActive(true);
            defaultGameObject.SetActive(false); 
        }
        
        public void UnHighlight()
        {
            highlightGameObject.SetActive(false);
            defaultGameObject.SetActive(true); 
            _isPlaced = true; 
            OnPlaced?.Invoke();
        }
        
        private void OnMouseDown()
        {
            if(_isPlaced)
            {
                return;
            }
            
            
           if(_houseElementController.PartType == HousePartType.Null)
           {
               return;
           }
           

           if (_houseElementController.PartType != _housePartType) return;
           UnHighlight();
           

        }
       
       
    }
}
