using HouseBuilding.Base;
using SantaSatisfaction;
using UnityEngine;

namespace Globals
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Managers and Controllers")]
        public SatisfactionController SatisfactionController;
        public HouseElementController HouseElementController;

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
        
    }
}
