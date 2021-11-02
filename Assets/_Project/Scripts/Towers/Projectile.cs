// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public class Projectile : MonoBehaviour, IProjectile
	{
		[SerializeField] private float speed = 5f;
		[SerializeField] private float timeToDestroy = 3f;

		public float Speed => speed;
		public float TimeToDestroy => timeToDestroy;

		public void Init(Transform targetPos) => Destroy(gameObject, timeToDestroy);

		public void ProcessCollision(GameObject other)
		{
			Destroy(gameObject);
		}

		private void Update() => transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);
		private void OnCollisionEnter(Collision collision) => ProcessCollision(collision.gameObject);
	}
}