// Maded by Pedro M Marangon
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace S2P_Test
{
	public class TowerShooter : MonoBehaviour, IGridMoveable
	{

		#region CONSTANT VALUES

		#region ValidateInput
		private const string DOES_IMPLEMENT_INTERFACE = "DoesImplementInterface";
		private const string VALIDATE_MESSAGE = "The GameObject associated must have a component that implements IProjectile!!";
		#endregion

		#region LOOP
		private const int INFINITE_LOOP = -1;
		private const LoopType RESTART_LOOP = LoopType.Restart;
		#endregion

		#region TOOLTIPS
		private const string TOOLTIP_SPAWNPOINT = "Position to where spawn the projectile";
		private const string TOOLTIP_PROJECTILE = "Projectile to be shot (MUST HAVE some script that has the IProjectile interface)";
		private const string TOOLTIP_TARGETPOINT = "Target position for the projectile to be shot at (only used in the grenade tower)";
		private const string TOOLTIP_PROJECTILEINTERVAL = "Interval (in seconds) for shooting the projectile";
		private const string TOOLTIP_DAMAGE = "The amount damage to apply to the enemies";
		#endregion

		private const float DEBUG_SPHERE_SIZE = 0.25f;
		#endregion

		[ValidateInput(DOES_IMPLEMENT_INTERFACE, VALIDATE_MESSAGE)]
		[Tooltip(TOOLTIP_PROJECTILE), Required, SerializeField] private GameObject projectile = null;

		[Tooltip(TOOLTIP_SPAWNPOINT), Required, SerializeField] private Transform spawnPoint = null;		
		[Tooltip(TOOLTIP_TARGETPOINT), SerializeField] private Transform targetPosition = null;
		[Tooltip(TOOLTIP_PROJECTILEINTERVAL), SerializeField] private float projectileInterval = 2f;
		[Tooltip(TOOLTIP_DAMAGE), Min(1), SerializeField] private int damage = 1;

		private void Start()
		{
			// Creates the sequence of waiting X seconds, then spawning the projectile
			var sequence = DOTween.Sequence()
				.AppendInterval(projectileInterval)
				.AppendCallback(() => CreateProjectile())
				.SetLoops(INFINITE_LOOP, RESTART_LOOP);
		}

		public void Move(Transform newGridParent)
		{
			transform.parent = newGridParent;
			transform.localPosition = Vector3.zero;
		}
		
		private void CreateProjectile()
		{
			GameObject inst = Instantiate(projectile, spawnPoint.position, Quaternion.identity);

			if (inst.TryGetComponent(out IProjectile proj))
				proj?.Init(targetPosition, damage);
		}

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
				Gizmos.DrawSphere(targetPosition.position, DEBUG_SPHERE_SIZE);
			}
		}

		//This function is only used by the ValidateInput attribute at line 34
		private bool DoesImplementInterface(GameObject go) => go != null && go.TryGetComponent(out IProjectile _);

	}
}