// Maded by Pedro M Marangon
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace S2P_Test
{
	[RequireComponent(typeof(Health))]
	public class BlinkOnDamage : MonoBehaviour
    {
		[Min(1), SerializeField] private float blinkIntensity = 2;
		[Min(0.01f), SerializeField] private float blinkDuration = 0.1f;
		[SerializeField] private Color blinkColor = Color.white;
		private List<Renderer> renderers;
		private Health health;
		private float blinkTimer = -1f;

		private void Awake()
        {
			health = GetComponent<Health>();
			renderers = GetComponentsInChildren<Renderer>().ToList();

			health.OnDamaged += Blink;
		}

		private void Update()
		{
			renderers.RemoveAll(rend => rend == null);

			blinkTimer -= Time.deltaTime;
			float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
			float intensity = (lerp * blinkIntensity) + 1;
			foreach (Renderer rend in renderers)
			{
				if (!rend) continue;
				rend.material.color = blinkColor * intensity;
			}

		}

		private void Blink() => blinkTimer = blinkDuration;
	}
}