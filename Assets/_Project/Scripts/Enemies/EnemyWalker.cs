// Maded by Pedro M Marangon
using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace S2P_Test
{
	public class EnemyWalker : MonoBehaviour, IEnemyLogic
    {
		private const string ANIM_ATK = "Zombie_Atk";
		private const string ANIM_WALK = "Zombie_Walk";
		private const string ANIM_DEATH = "Zombie_Death";
		private const string HEADER_MOVEMENT = "Movement";
		private const string HEADER_CHECK_STOPPING_POINT = "Check to detect stopping point";
		private const string HEADER_REFERENCES = "References";

		[Header(HEADER_MOVEMENT)]
		[SerializeField] private float speed = 2;
		[SerializeField] private float stoppingDistance = 2.5f;
		[Header(HEADER_CHECK_STOPPING_POINT)]
		[SerializeField] private float checkDistance = 100f;
		[SerializeField] private float checkHeight = 1;
		[SerializeField] private float checkRate = 0.2f;
		[Header(HEADER_REFERENCES)]
		[SerializeField] private Animator anim = null;
		[Required, SerializeField] private EnemyAttack attack = null;

		private Collider col;
		private Health health;
		private bool isDead;
		private Ray ray;
		private RaycastHit hit;

		public float Speed => speed;
		public float Damage => attack.Damage;

		private void Awake()
		{
			health = GetComponent<Health>();
			col = GetComponent<Collider>();
		}

		private void Start()
        {
			health.OnDeath += OnDead;
			health.OnHealthChanged += OnDamage;

			StartCoroutine(CheckForStoppingPoint());
		}

		private IEnumerator CheckForStoppingPoint()
		{
			if (this == null) yield break;

			if (!attack.IsTowerInFront())
			{
				ray = new Ray(transform.position + Vector3.up * checkHeight, Vector3.right);
				if (Physics.Raycast(ray, out hit, checkDistance))
				{
					Vector3 position = hit.collider.transform.position + (Vector3.left * stoppingDistance);
					position.y = transform.position.y;
					position.z = transform.position.z;

					MoveToPosition(position);
				}
			}
			else StopMoving();


			yield return new WaitForSeconds(checkRate);

			yield return StartCoroutine(CheckForStoppingPoint());
		}

		#region Movement
		
		public void MoveToPosition(Vector3 pos)
		{
			DOTween.Kill(transform);
			if (isDead) return;

			anim.PlayAnimationIfNotPlayingAlready(ANIM_WALK);
			transform.DOMove(pos, Speed)
				.SetSpeedBased(true)
				.OnComplete(() => StopMoving());
		}

		private void StopMoving()
		{
			DOTween.Kill(transform);

			if(attack.IsTowerInFront())
				anim.PlayAnimationIfNotPlayingAlready(ANIM_ATK);
		}

		#endregion

		private void OnDead()
		{
			isDead = true;
			StopCoroutine(CheckForStoppingPoint());
			Destroy(col);

			anim?.Play(ANIM_DEATH);
		}

		private void OnDamage()
		{
			
		}


#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			if (UnityEditor.EditorApplication.isPlaying) return;
			Gizmos.color = Color.red;
			Vector3 startPos = transform.position + Vector3.up * checkHeight;
			Gizmos.DrawLine(startPos, startPos + (Vector3.right * checkDistance));
		}
#endif

	}

}