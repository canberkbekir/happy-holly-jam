using System;
using UnityEngine;

namespace HouseBuilding.Base
{
    public class HouseChecker : MonoBehaviour
    {
        public event Action HouseBuilt;
        
         [SerializeField] private HouseElement[] _houseElements;
         [SerializeField] private int _houseElementCount = 0;
         private void Awake()
         {
             foreach (var element in _houseElements)
             {
                 element.OnPlaced += CheckHouse;
             }
         }

         private void CheckHouse()
         {
                _houseElementCount++;
                Debug.Log(_houseElementCount);
                if (_houseElementCount == _houseElements.Length)
                {
                    HouseBuilt?.Invoke();
                    Debug.Log("House built");
                } 
         }
    }
}
