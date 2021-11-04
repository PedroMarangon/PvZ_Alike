// Maded by Pedro M Marangon
using UnityEngine;

namespace S2P_Test
{
	public static class Extentions
	{
		public static void PlayAnimationIfNotPlayingAlready(this Animator anim, string name, int layer = 0)
		{
			if (anim.GetCurrentAnimatorStateInfo(layer).IsName(name))
				anim.Play(name, layer);
		}
	}

}