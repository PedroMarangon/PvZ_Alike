// Maded by Pedro M Marangon
using DG.Tweening;
using UnityEngine;

namespace S2P_Test
{
	public class ArchedProjectile : MonoBehaviour, IProjectile
	{
		[SerializeField] private float jumpDuration = 5f;
		[Min(0), SerializeField] private float maxHeight = 2f;
		[SerializeField] private float timeToDestroy = 3f;
		[SerializeField] private GameObject explosionFX = null;

		private int damage;

		public float Speed => jumpDuration;
		public int Damage => damage;
		public float TimeToDestroy => timeToDestroy;

		public void Init(Transform targetPos, int dmg)
		{
			damage = dmg;
			if (!targetPos) return;
			transform.DOJump(targetPos.position, maxHeight, 1, jumpDuration).SetEase(Ease.Linear);
		}

		public void ProcessCollision(GameObject other)
		{
			if (other.TryGetComponent(out Health health))
			{
				health.Damage(damage);
				ExplosionFX();
				Destroy(gameObject);
			}else
			{
				Invoke(nameof(ExplosionFX), timeToDestroy);
				Destroy(gameObject, timeToDestroy);
			}
		}
		private void OnCollisionEnter(Collision collision) => ProcessCollision(collision.gameObject);

		private void ExplosionFX() => Instantiate(explosionFX, transform.position, Quaternion.identity);

	}

}