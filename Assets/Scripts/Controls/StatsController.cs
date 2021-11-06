using System;
using UnityEngine;

namespace Controls
{
    public class StatsController : MonoBehaviour
    {
        public event Action PlayerDied;
        
        public int maxHealth;

        private int _health;
        public int Health
        {
            get => _health;
            private set
            {
                _health = value;
                if (_health <= 0)
                    PlayerDied?.Invoke();
            }
        }

        private void Start()
        {
            Health = maxHealth;
        }

        public void ApplyDamage(int damage)
        {
            Health -= damage;
        }
        

    }
}