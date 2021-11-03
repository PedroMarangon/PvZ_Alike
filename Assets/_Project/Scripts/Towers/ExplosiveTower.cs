// Maded by Pedro M Marangon
using DG.Tweening;
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

		private void Awake()
		{
			var s = DOTween.Sequence()
				.AppendCallback(() => GetComponent<Collider>().enabled = false)
				.AppendInterval(0.1f)
				.AppendCallback(() => GetComponent<Collider>().enabled = true);
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.TryGetComponent(out IEnemyLogic _))
			{
				Collider[] enemies = Physics.OverlapBox(transform.position, Vector3.one * explosionRadius, Quaternion.identity, explosionLayerMask);

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
			Gizmos.DrawWireCube(transform.position, Vector3.one * explosionRadius * 2);


		}

		public void Move(Transform newGridParent)
		{
			transform.parent = newGridParent;
			transform.localPosition = Vector3.zero;
		}
	}
}