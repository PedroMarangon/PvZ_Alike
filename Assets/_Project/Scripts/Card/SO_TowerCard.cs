//Maded by Pedro M Marangon
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S2P_Test
{
	[CreateAssetMenu(fileName = "TowerCard", menuName="ScriptableObjects/TowerCard", order=0)]
	public class SO_TowerCard : ScriptableObject
	{
		public Sprite towerIcon;
		public string towerName = "";
		public GameObject towerPrefab;
		[Min(5)] public int cost = 10;
		[Range(1, 10)] public float cooldown = 2;
	}
}