using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace HouseBuilding.Base
{
    public class HouseElementController : MonoBehaviour
    { 
        public event Action<HousePartType> OnHousePartTypeChanged;
        
        [Header("References")]
        [SerializeField] private SelectableHouseElement[] selectableHouseElements;
        
        private HousePartType _partType = HousePartType.Null;
        public HousePartType PartType => _partType;
        
        private void Awake()
        {
            foreach (var selectableHouseElement in selectableHouseElements)
            {
                selectableHouseElement.OnElementClicked += OnElementClicked;
            }
        }

        private void OnElementClicked(HousePartType obj)
        {
            SetHousePartType(obj);
        }

        public void SetHousePartType(HousePartType newHousePartType)
       {
           this._partType = newHousePartType;
           OnHousePartTypeChanged?.Invoke(newHousePartType); 
       } 
         
       
    }
}
