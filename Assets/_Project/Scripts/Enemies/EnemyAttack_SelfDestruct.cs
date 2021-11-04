// Maded by Pedro M Marangon
using Cinemachine;
using UnityEngine;

namespace S2P_Test
{
	public class EnemyAttack_SelfDestruct : EnemyAttack
	{

		protected override void Awake()
		{
			base.Awake();
			health.OnDeath += Die;
			GetComponent<EnemyWalker>().OnStopMoving += Atk;
		}

		private void Die()
		{
			GetComponent<CinemachineImpulseSource>()?.GenerateImpulse();
			Destroy(gameObject);
		}

		public override void Atk()
		{
			base.Atk();
			health.Damage(health.MaxHealth);
		}
	}

}