// Maded by Pedro M Marangon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S2P_Test
{
	[RequireComponent(typeof(Collider))]
	public class ExplosiveTower : MonoBehaviour, IGridMoveable
    {
		[SerializeField] private float explosionRadius = 3f;
		[SerializeField] private int damage = 5;
		[SerializeField] private LayerMask explosionLayerMask;

		private void OnValidate()
		{
			GetComponent<Collider>().isTrigger = true;
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.TryGetComponent(out IEnemyLogic _))
			{
				Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

				foreach (var enemy in enemies)
				{
					if (enemy.TryGetComponent(out Health enemyHealth))
						enemyHealth.Damage(damage);
				}
				Destroy(gameObject);
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, explosionRadius);
		}

		public void Move(Transform newGridParent)
		{
			transform.parent = newGridParent;
			transform.localPosition = Vector3.zero;
		}
	}
}