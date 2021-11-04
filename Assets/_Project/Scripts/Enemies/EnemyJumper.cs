// Maded by Pedro M Marangon
using DG.Tweening;
using NaughtyAttributes;
using System;
using UnityEngine;

namespace S2P_Test
{
	public class EnemyJumper : MonoBehaviour, IEnemyLogic
	{
		private const string HEADER_MOVEMENT = "Movement";
		private const string HEADER_REFERENCES = "References";

		[Header(HEADER_MOVEMENT)]
		[SerializeField] private float jumpDuration = 2f;
		[SerializeField] private float distanceToJump = 2f;
		[SerializeField] private float jumpHeight = 2f;
		[SerializeField] private float timeToJump = 2f;
		[SerializeField] private AnimationCurve easeCurve;
		[Header(HEADER_REFERENCES)]
		[Required, SerializeField] private EnemyAttack attack = null;
		private Health health;
		private Vector3 position;
		private Sequence s;

		public float Speed => jumpDuration;
		public float Damage => attack.Damage;

		public Action OnStartMoving;

		private void Awake() => health = GetComponent<Health>();

		private void Start()
		{
			health.OnDeath += OnDead;
			MoveToPosition(transform.position);
		}

		public void MoveToPosition(Vector3 pos)
		{
			position = pos + Vector3.right * distanceToJump;
			s.Kill();

			if (attack.IsTowerInFront())
			{
				StopMoving();
				return;
			}

			OnStartMoving?.Invoke();
			s = DOTween.Sequence()
				.AppendCallback(() =>
				{
				})
				.Append(transform.DOJump(position, jumpHeight, 1, jumpDuration).SetEase(easeCurve))
				.AppendInterval(timeToJump)
				.OnComplete(() =>
				{
					if (!attack.IsTowerInFront())
						MoveToPosition(position);
					else
						StopMoving();
				});
		}

		private void StopMoving()
		{
			s.Kill();
			if (attack.IsTowerInFront())
			{
				if(transform.position != attack.GetFirstTower().transform.position)
					position += Vector3.right * (distanceToJump / 2);

				s = DOTween.Sequence()
				.Append(transform.DOJump(position, jumpHeight, 1, jumpDuration).SetEase(easeCurve))
				.OnComplete(() =>
				{
					attack.Atk();
					health.Damage(health.MaxHealth);
				});
			}
		}

		private void OnDead()
		{
			s?.Kill();
			Destroy(gameObject);
		}


#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(transform.position + Vector3.right * distanceToJump, 0.25f);
		}
#endif

	}

}