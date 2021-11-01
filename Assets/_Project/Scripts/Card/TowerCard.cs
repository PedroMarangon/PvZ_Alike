// Maded by Pedro M Marangon
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace S2P_Test
{
	public class TowerCard : MonoBehaviour
    {
		private const int COOLDOWN_FINISHED_VALUE = 0;
		private const int COOLDOWN_START_VALUE = 1;

		[Required, SerializeField] private SO_TowerCard card;
		[Header("HUD Elements")]
		[Required, SerializeField] private Image icon;
		[Required, SerializeField] private Image cooldown;
		[HorizontalLine]
		[Required, SerializeField] private TMP_Text title;
		[Required, SerializeField] private TMP_Text cost;

		public bool CanMoveCard { get; private set; } = true;
		public GameObject Prefab => card.towerPrefab;

		[Button]
		private void UpdateCardVisual()
		{
			if (!card) return;

			if(icon) icon.sprite = card.towerIcon;
			if(title) title.text = card.towerName;
			if(cost) cost.text = $"{card.cost}";
		}

		public void StartCooldown()
		{
			CanMoveCard = false;
			cooldown.fillAmount = COOLDOWN_START_VALUE;

			cooldown.DOFillAmount(COOLDOWN_FINISHED_VALUE, card.cooldown)
				.SetEase(Ease.Linear)
				.OnComplete(() => CanMoveCard = true);

		}
	}
}