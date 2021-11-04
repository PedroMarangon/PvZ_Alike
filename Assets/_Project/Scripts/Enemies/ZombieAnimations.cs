// Maded by Pedro M Marangon
using NaughtyAttributes;
using UnityEngine;

namespace S2P_Test
{
	[RequireComponent(typeof(EnemyWalker))]
	[RequireComponent(typeof(Health))]
	public class ZombieAnimations : MonoBehaviour
	{
		private const string ANIM_ATK = "Zombie_Atk";
		private const string ANIM_WALK = "Zombie_Walk";
		private const string ANIM_DEATH = "Zombie_Death";
		[Required, SerializeField] private Animator anim = null;
		private EnemyWalker walking;
		private Health health;

		private void Awake()
		{
			walking = GetComponent<EnemyWalker>();
			walking.OnStartMoving += StartWalking;
			walking.OnStopMoving += StartAttacking;

			health = GetComponent<Health>();
			health.OnDeath += Die;
		}

		//Play the attack animation
		public void StartAttacking() => anim?.PlayAnimationIfNotPlayingAlready(ANIM_ATK);
		//Play the walk animation
		public void StartWalking() => anim?.PlayAnimationIfNotPlayingAlready(ANIM_WALK);
		//Kills the enemy
		public void Die()
		{
			anim?.Play(ANIM_DEATH);
			Destroy(gameObject);
		}
	}

}