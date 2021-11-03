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

		[Required, SerializeField] private SO_TowerCard card = null;
		[Header("HUD Elements")]
		[Required, SerializeField] private Image icon = null;
		[Required, SerializeField] private Image cooldown = null;
		[HorizontalLine]
		[Required, SerializeField] private TMP_Text title = null;
		[Required, SerializeField] private TMP_Text cost = null;

		public bool IsInCooldown { get; private set; } = false;
		public bool DoesHaveEnoughMoney { get; private set; } = false;
		public GameObject Prefab => card.towerPrefab;
		public int Cost => card.cost;

		[Button]
		private void UpdateCardVisual()
		{
			if (!card) return;

			if(icon) icon.sprite = card.towerIcon;
			if(title) title.text = card.towerName;
			if(cost) cost.text = $"{card.cost}";
		}

		private void Awake()
		{
			CheckMoney(0);
			FindObjectOfType<MoneySystem>().OnMoneyIncrease += CheckMoney;
		}

		private void CheckMoney(int money)
		{
			if (IsInCooldown) return;

			DoesHaveEnoughMoney = card.cost > money;

			cooldown.fillAmount = DoesHaveEnoughMoney ? COOLDOWN_START_VALUE : COOLDOWN_FINISHED_VALUE;
		}

		public void StartCooldown()
		{
			IsInCooldown = true;
			cooldown.fillAmount = COOLDOWN_START_VALUE;

			cooldown.DOFillAmount(COOLDOWN_FINISHED_VALUE, card.cooldown)
				.SetEase(Ease.Linear)
				.OnComplete(() => IsInCooldown = false);

		}
	}
}