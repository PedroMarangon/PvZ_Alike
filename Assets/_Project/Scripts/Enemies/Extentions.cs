// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public static class Extentions
	{
		public static bool IsPlayingAnimation(this Animator anim, string name, int layer = 0) => anim.GetCurrentAnimatorStateInfo(layer).IsName(name);
		public static void PlayAnimationIfNotPlayingAlready(this Animator anim, string name, int layer = 0)
		{
			if (!anim.IsPlayingAnimation(name, layer))
				anim.Play(name, layer);
		}
	}

}