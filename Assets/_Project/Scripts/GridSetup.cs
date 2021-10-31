// Maded by Pedro M Marangon
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace S2P_Test
{
	public class GridSetup : MonoBehaviour
    {
		[Header("Grid generation")]
		[SerializeField] private Vector2Int gridSize = new Vector2Int(3, 2);
		[Min(1), SerializeField] private float cellSize = 1;
		[SerializeField] private Color DEBUG_c1 = new Color(255, 255, 255, 0.75f);
		[SerializeField] private Color DEBUG_c2 = new Color(0, 0, 0, 0.75f);

		[Header("Spawner generation")]
		[SerializeField] private EnemySpawner enemySpawnerPrefab;
		[SerializeField] private float spawnerXPos = -10;

		private void Awake()
		{
			if(enemySpawnerPrefab)
			{
				for (int y = 0; y < gridSize.y; y++)
				{
					Vector3 point = transform.position;
					point.z += y * cellSize;
					point.x += spawnerXPos;

					GameObject instantiatedSpawner = Instantiate(enemySpawnerPrefab, point, Quaternion.identity).gameObject;
					instantiatedSpawner.name = $"{enemySpawnerPrefab.name} - ROW {y+1}";
				}
			}
		}


		private void OnDrawGizmos()
		{
			Vector3 cellSizeVector = new Vector3(cellSize, 0, cellSize);
			

			for (int x = 0; x < gridSize.x; x++)
			{
				for (int y = 0; y < gridSize.y; y++)
				{
					Gizmos.color = ((x + y) % 2 == 1) ? DEBUG_c2 : DEBUG_c1;
					Vector3 point = transform.position + (new Vector3(x, 0, y) * cellSize);
					Gizmos.DrawCube(point + (Vector3.up * 0.001f), cellSizeVector);


					Gizmos.color = Color.red;
					point.x = transform.position.x;
					point.x += spawnerXPos;
					Gizmos.DrawSphere(point, 0.25f);

				}
			}


		}

	}
}