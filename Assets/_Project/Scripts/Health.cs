// Maded by Pedro M Marangon
using NaughtyAttributes;
using System;
using UnityEngine;

namespace S2P_Test
{
	public class Health : MonoBehaviour
	{
		private const float DEATH_EFFECT_UP_VALUE = 0.2f;
		private const int MIN_HEALTH = 0;
		[SerializeField] protected int maxHealth = 10;
		[SerializeField] protected int moneyToGiveWhenKilled = 0;
		[SerializeField] private GameObject deathEffect;
		[ProgressBar("maxHealth", EColor.Red), SerializeField, ReadOnly] protected int health;

		#region Properties
		
		public int MaxHealth => maxHealth;
		public int HP => health;
		public float Percentage => (float)health / (float) maxHealth;
		public float InversePercentage => 1 - Percentage;

		#endregion

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (UnityEditor.EditorApplication.isPlaying) return;
			health = maxHealth;
		}
#endif

		public Action OnDeath;
		public Action OnDamaged;

		private void Start() => health = maxHealth;

		public void Damage(int amnt)
		{
			OnDamaged?.Invoke();
			SetHealth(health - amnt);
		}

		/// <summary>
		/// Sets the health to a specific value. If the health is less than or equal to 0, kills this object
		/// </summary>
		/// <param name="amnt"></param>
		public void SetHealth(int amnt)
		{
			health = Mathf.Clamp(amnt, MIN_HEALTH, maxHealth);
			if (health <= MIN_HEALTH) Die();
		}

		public void Die()
		{
			if(deathEffect) Instantiate(deathEffect, transform.position + (Vector3.up * DEATH_EFFECT_UP_VALUE), Quaternion.identity);

			FindObjectOfType<MoneySystem>()?.AddMoney(moneyToGiveWhenKilled);

			if(OnDeath != null) OnDeath?.Invoke();
			else Destroy(gameObject);
		}
	}
}