// Maded by Pedro M Marangon
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace S2P_Test
{
	[RequireComponent(typeof(Collider))]
	public class ExplosiveTower : MonoBehaviour, IGridMoveable
    {
		#region Constant Values
		private const float INTERVAL_FOR_ACTIVATION = 0.1f;
		private const int GIZMO_BOX_MULTIPLIER = 2;
		#endregion

		[SerializeField] private float explosionRadius = 3f;
		[SerializeField] private int damage = 5;
		[SerializeField] private LayerMask explosionLayerMask;

		#region Get Collider
		private Collider col;
		private Collider Col
		{
			get
			{
				if (col == null) col = GetComponent<Collider>();
				return col;
			}
		}
		#endregion

		private void OnValidate() => Col.isTrigger = true;

		private void Awake()
		{
			var s = DOTween.Sequence()
				.AppendCallback(() => Col.enabled = false)
				.AppendInterval(INTERVAL_FOR_ACTIVATION)
				.AppendCallback(() => Col.enabled = true);
		}

		public void Move(Transform newGridParent)
		{
			transform.parent = newGridParent;
			transform.localPosition = Vector3.zero;
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
				GetComponent<CinemachineImpulseSource>()?.GenerateImpulse();
				Destroy(gameObject);
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, Vector3.one * explosionRadius * GIZMO_BOX_MULTIPLIER);
		}
	}
}