//Maded by Pedro M Marangon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S2P_Test
{
	[CreateAssetMenu(fileName = "TowerCard", menuName="ScriptableObjects/TowerCard", order=0)]
	public class TowerCard : ScriptableObject
	{
		public Sprite towerIcon;
		public GameObject towerPrefab;
		[Min(5)] public int cost;
		[Range(1, 10)] public float cooldown;
	}
}