using System;
using Controls;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHealthView : MonoBehaviour
    {
        public StatsController stats;
        
        [SerializeField]
        private Slider slider;

        private void FixedUpdate()
        {
            slider.value = (float) stats.Health / stats.maxHealth;
        }
    }
}