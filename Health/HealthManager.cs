using GameDevProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Health
{
    public class HealthManager : IHealth
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }
        public bool IsAlive => CurrentHealth > 0;
        public bool IsInvincible => (DateTime.UtcNow - lastDamageTime) < TimeSpan.FromSeconds(invincibilityDuration);
        private float invincibilityDuration;
        private DateTime lastDamageTime;
        public HealthManager(int maxHealth, float invincibilityDuration) 
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            this.invincibilityDuration = invincibilityDuration;
            lastDamageTime = DateTime.MinValue;
        }

        public void Heal(int amount)
        {
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
        }

        public void TakeDamage(int damage)
        {
            if ((DateTime.UtcNow - lastDamageTime) < TimeSpan.FromSeconds(invincibilityDuration))
                return; // Ignore if we're still invincible

            lastDamageTime = DateTime.UtcNow; //damage taken, so update lastDamageTime
            CurrentHealth = Math.Max(0, CurrentHealth - damage);
        }
    }
}
