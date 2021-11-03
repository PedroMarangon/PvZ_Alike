// Maded by Pedro M Marangon
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace S2P_Test
{
	public class MoneySystem : MonoBehaviour
	{
		[Min(0), SerializeField] private int maxMoney = 999999;
		[Min(0), SerializeField] private int startingMoney = 0;
		[Min(1), SerializeField] private float increaseInterval = 1f;
		[Min(1), SerializeField] private int incrementAmount = 2;
		[SerializeField] private TMP_Text moneyText = null;

		public Action<int> OnMoneyIncrease;
		private int money;
		private int moneyToGiveBackIfChange;

		private void Awake()
		{
			OnMoneyIncrease += UpdateUI;

			money = startingMoney;
			OnMoneyIncrease?.Invoke(money);

			Sequence s = DOTween.Sequence()
				.AppendInterval(increaseInterval)
				.AppendCallback(() => money += incrementAmount)
				.AppendCallback(() => money = Mathf.Clamp(money, 0, maxMoney))
				.AppendCallback(() => OnMoneyIncrease?.Invoke(money))
				.SetLoops(-1);
		}

		public void GiveMoneyBack()
		{
			money += moneyToGiveBackIfChange;
			money = Mathf.Clamp(money, 0, maxMoney);
			OnMoneyIncrease?.Invoke(money);
		}

		public void RemoveMoney(int amnt)
		{
			moneyToGiveBackIfChange = amnt;

			money -= amnt;
			money = Mathf.Clamp(money, 0, maxMoney);
			OnMoneyIncrease?.Invoke(money);
		}

		private void UpdateUI(int money) => moneyText.text = $"{money}";
	}
}