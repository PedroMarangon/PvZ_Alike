// Maded by Pedro M Marangon
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace S2P_Test
{
	public class TowerShooter : MonoBehaviour
	{
		[ValidateInput("DoesImplementInterface", "The GameObject associated must have a component that implements IProjectile!!")]
		[Required, SerializeField] private GameObject projectile = null;

		[Required, SerializeField] private Transform spawnPoint = null;
		[SerializeField] private Transform targetPosition = null;
		[SerializeField] private float projectileInterval = 2f;
		[Min(1), SerializeField] private int damage = 1;

		private void Start()
		{
			var sequence = DOTween.Sequence()
				.AppendInterval(projectileInterval)
				.AppendCallback(() => CreateProjectile())
				.SetLoops(-1, LoopType.Restart);
		}

		private void CreateProjectile()
		{
			var inst = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
			if (inst.TryGetComponent(out IProjectile proj))
				proj?.Init(targetPosition, damage);
		}

		private bool DoesImplementInterface(GameObject go) => go != null && go.TryGetComponent(out IProjectile _);

		private void OnDrawGizmos()
		{
			if(spawnPoint)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(spawnPoint.position, spawnPoint.position + Vector3.left);
			}

			if(targetPosition)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawSphere(targetPosition.position, 0.25f);
			}
		}
	}

}