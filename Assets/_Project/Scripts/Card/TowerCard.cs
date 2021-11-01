// Maded by Pedro M Marangon
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace S2P_Test
{
    public class TowerCard : MonoBehaviour
    {
		[SerializeField] private SO_TowerCard card;
		[SerializeField] private Image icon;
		[SerializeField] private Image cooldown;
		[SerializeField] private TMP_Text title;
		[SerializeField] private TMP_Text cost;

		public bool CanMoveCard { get; private set; } = true;

		private void OnValidate()
		{
			if (!card) return;
			icon.sprite = card.towerIcon;
			title.text = card.towerName;
			cost.text = $"{card.cost}";
		}

		public void StartCooldown()
		{
			CanMoveCard = false;
			cooldown.fillAmount = 1;

			cooldown.DOFillAmount(0, card.cooldown)
				.SetEase(Ease.Linear)
				.OnComplete(() => CanMoveCard = true);

		}
	}
}