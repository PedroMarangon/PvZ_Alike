// Maded by Pedro M Marangon
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace S2P_Test
{
	public class MoneySystem : MonoBehaviour
	{
		#region Constant Values
		private const int INFINITE_LOOPS = -1;
		private const int MIN_MONEY = 0;
		#endregion

		[Min(0), SerializeField] private int maxMoney = 999999;
		[Min(0), SerializeField] private int startingMoney = 0;
		[Min(0.1f), SerializeField] private float increaseInterval = 1f;
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
				.AppendCallback(() => ClampMoney())
				.SetLoops(INFINITE_LOOPS);
		}

		public void GiveMoneyBack() => AddMoney(moneyToGiveBackIfChange);

		public void AddMoney(int amnt)
		{
			money += amnt;
			ClampMoney();
		}
		public void RemoveMoney(int amnt)
		{
			moneyToGiveBackIfChange = amnt;

			money -= amnt;
			ClampMoney();
		}

		private void ClampMoney()
		{
			money = Mathf.Clamp(money, MIN_MONEY, maxMoney);
			OnMoneyIncrease?.Invoke(money);
		}

		private void UpdateUI(int money) => moneyText.text = $"{money}";
	}
}