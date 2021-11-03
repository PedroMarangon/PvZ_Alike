﻿// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public class EnemyAttack : MonoBehaviour
	{
		[SerializeField] private Transform atkPoint = null;
		[SerializeField] private float atkRadius = 0.25f;
		[SerializeField] private LayerMask whatIsTower = default;
		[field: SerializeField] public int Damage { get; private set; } = 3;

		public void Atk()
		{
			var hits = Physics.OverlapSphere(atkPoint.position, atkRadius, whatIsTower);

			if (hits.Length > 0)
			{
				var hit = hits[0];
				if(hit != null && hit.TryGetComponent(out Health health))
				{
					health.Damage(Damage);
				}
			}
		}

		public bool IsTowerInFront() => Physics.OverlapSphere(atkPoint.position, atkRadius, whatIsTower).Length > 0;

		private void OnDrawGizmos()
		{
			if (!atkPoint) return;
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(atkPoint.position, atkRadius);
		}
	}

}