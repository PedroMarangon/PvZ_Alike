// Maded by Pedro M Marangon
using NaughtyAttributes;
using UnityEngine;

namespace S2P_Test
{
	[RequireComponent(typeof(Health))]
	public class ChangeEmissionBasedOnHealth : MonoBehaviour
	{
		private const string EMISSION = "_EmissionColor";
		[Required, SerializeField] private Renderer rend = null;
		[Min(0), SerializeField] private float emissionIntensity = 5f;
		[SerializeField] private Gradient colorGradient = new Gradient();
		private Health health;

		private void Awake()
		{
			health = GetComponent<Health>();
			health.OnDamaged += UpdateColor;
			UpdateColor();
		}

		private void UpdateColor() => rend.material.SetColor(EMISSION, colorGradient.Evaluate(health.Percentage) * emissionIntensity);
	}
}