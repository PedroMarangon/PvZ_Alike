// Maded by Pedro M Marangon
using NaughtyAttributes;
using System;
using UnityEngine;

namespace S2P_Test
{
	public class Health : MonoBehaviour
	{
		[SerializeField] protected int maxHealth = 10;
		[ProgressBar("maxHealth", EColor.Red), SerializeField, ReadOnly] protected int health;

		#region Properties
		
		/// <summary>
		/// THe max amount of health
		/// </summary>
		public int MaxHealth => maxHealth;
		/// <summary>
		/// The current amount of health
		/// </summary>
		public int HP => health;
		/// <summary>
		/// The percentage amount of health (goes from 0-1)
		/// </summary>
		public float Percentage => (float)health / (float) maxHealth;
		/// <summary>
		/// The inverse amount of the Health Percentage (ex.: if the Percentage is 0.6, this will return 1 - 0.6, i.e. 0.4)
		/// </summary>
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

		protected virtual void Start() => health = maxHealth;
		/// <summary>
		/// Applies armor to the damage and then damage by that amount
		/// </summary>
		/// <param name="amnt">The intented amount do damage (the armor algorithm will change this value)</param>
		public virtual void Damage(int amnt)
		{
			OnDamaged?.Invoke();
			SetHealth(health - amnt);
		}

		/// <summary>
		/// Sets the health to a specific value. If the health is less than or equal to 0, kills this object
		/// </summary>
		/// <param name="amnt"></param>
		public virtual void SetHealth(int amnt)
		{
			health = Mathf.Clamp(amnt, 0, maxHealth);
			if (health <= 0) Die();
		}

		/// <summary>
		/// Kills the object
		/// </summary>
		public virtual void Die()
		{
			if(OnDeath != null) OnDeath.Invoke();
			else Destroy(gameObject);
		}
	}
}