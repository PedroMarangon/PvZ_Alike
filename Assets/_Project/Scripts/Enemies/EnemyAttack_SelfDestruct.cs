// Maded by Pedro M Marangon

namespace S2P_Test
{
	public class EnemyAttack_SelfDestruct : EnemyAttack
	{
		public override void Atk()
		{
			base.Atk();
			health.Damage(health.MaxHealth);
		}
	}

}