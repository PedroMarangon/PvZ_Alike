// Maded by Pedro M Marangon
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S2P_Test
{
	public class EnemySpawner : MonoBehaviour
	{

		#region ValidateInput
		private const string DOES_IMPLEMENT_INTERFACE = "DoesImplementInterface";
		private const string VALIDATE_MESSAGE = "All of the GameObjects associated must have a component that implements IEnemyLogic!!";
		#endregion

		[SerializeField] private List<Transform> spawnPoints = new List<Transform>();
		[ValidateInput(DOES_IMPLEMENT_INTERFACE, VALIDATE_MESSAGE)]
		[SerializeField] private List<GameObject> enemies = new List<GameObject>();

		[MinMaxSlider(0.5f, 7f), SerializeField] private Vector2 timeBetweenSpawn = new Vector2(2, 3);
		[SerializeField] private float startDelay = 1f;

		public void SetupSpawnerList()
		{
			spawnPoints.Clear();

			foreach (Transform child in transform) spawnPoints.Add(child);
		}

		private IEnumerator Start()
		{
			yield return new WaitForSeconds(startDelay);
			while(true)
			{
				float timeDelay = Random.Range(timeBetweenSpawn.x, timeBetweenSpawn.y);

				yield return new WaitForSeconds(timeDelay);

				var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
				var enemy = enemies[Random.Range(0, enemies.Count)];

				Instantiate(enemy, spawnPoint.position, Quaternion.identity);

			}
		}


		private bool DoesImplementInterface(List<GameObject> ensGO)
		{
			foreach (var en in ensGO)
			{
				if (en == null) continue;
				if (!en.TryGetComponent(out IEnemyLogic log)) return false;
			}
			return true;
		}
	}
}