// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public class EnemyAttack : MonoBehaviour
	{
		private const int FIRST_ELEMENT_OF_ARRAY = 0;
		[SerializeField] private Transform atkPoint = null;
		[SerializeField] private float atkRadius = 0.25f;
		[SerializeField] private LayerMask whatIsTower = default;
		[field: SerializeField] public int Damage { get; private set; } = 3;
		protected Health health;

		public virtual void Atk()
		{
			var hits = Physics.OverlapSphere(atkPoint.position, atkRadius, whatIsTower);

			if (hits.Length > 0)
			{
				var hit = hits[FIRST_ELEMENT_OF_ARRAY];
				if(hit != null && hit.TryGetComponent(out Health hitHealth))
				{
					hitHealth.Damage(Damage);
				}
			}
		}

		protected virtual void Awake() => health = GetComponent<Health>();

		public bool IsTowerInFront() => Physics.OverlapSphere(atkPoint.position, atkRadius, whatIsTower).Length > 0;
		public Collider GetFirstTower() => Physics.OverlapSphere(atkPoint.position, atkRadius, whatIsTower)[FIRST_ELEMENT_OF_ARRAY];

		private void OnDrawGizmos()
		{
			if (!atkPoint) return;
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(atkPoint.position, atkRadius);
		}
	}

}