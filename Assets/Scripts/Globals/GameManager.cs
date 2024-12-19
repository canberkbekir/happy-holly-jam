using System;
using HouseBuilding.Base;
using SantaSatisfaction;
using Unity.VisualScripting;
using UnityEngine;

namespace Globals
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event Action RestartGame;
        
        [Header("Managers and Controllers")]
        public SatisfactionController SatisfactionController;
        public HouseElementController HouseElementController;
        public HouseSpawnManager HouseSpawnManager;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void RestartWholeGame()
        {
            RestartGame.Invoke();
        }
        
        
        
        
    }
}
