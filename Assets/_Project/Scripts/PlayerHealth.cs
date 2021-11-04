// Maded by Pedro M Marangon
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace S2P_Test
{
	public class PlayerHealth : MonoBehaviour
    {
		private const float HP_BAR_DURATION = 0.1f;
		private const int TIME_PAUSED = 0;
		[SerializeField] private Slider healthBar;
		[SerializeField] private GameObject gameOverScreen;
		private Health health;

		private void Awake()
		{
			Time.timeScale = 1;
			health = GetComponent<Health>();
			health.OnDamaged += Damaged;
			health.OnDeath += Death;
		}

		private void Damaged() => healthBar.DOValue(health.Percentage, HP_BAR_DURATION);

		private void Death()
		{
			Time.timeScale = TIME_PAUSED;
			gameOverScreen.SetActive(true);
		}

		public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}