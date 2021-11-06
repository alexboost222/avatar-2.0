using System;
using System.Collections;
using System.Threading.Tasks;
using Controls;
using UnityEngine;

namespace Magic
{
    public abstract class MagicProjectile : MonoBehaviour
    {
        public abstract MagicType Type { get; }
        
        public float speed;
        public float duration;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(duration);
            
            Dissolve();
        }

        private void Update()
        {
            transform.position += transform.forward * (Time.deltaTime * speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player")
                || other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                ApplyEffect(other.GetComponentInParent<PlayerController>());
                Dissolve();
            } 
            else if (other.gameObject.layer == LayerMask.NameToLayer("MagicShield"))
            {
                TouchShield(other.GetComponentInParent<MagicShield>());
            }
            else
            {
                Dissolve();
            }
        }

        public virtual void Dissolve()
        {
            Destroy(gameObject);
        }

        protected abstract void ApplyEffect(PlayerController pc);

        protected virtual void TouchShield(MagicShield shield)
        {
            shield.Touch(this);
            
            if (shield.Type != Type)
                Dissolve();
        }
        
        
    }
}