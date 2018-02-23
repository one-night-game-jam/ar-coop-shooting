using UnityEngine;
using UnityEngine.Networking;

namespace NetworkingTest
{
	public class  EnemySpawner : NetworkBehaviour
	{
		[SerializeField]
		private GameObject _enemyObject;

		public override void OnStartServer()
		{
			var enemy = Instantiate(_enemyObject);
			NetworkServer.Spawn(enemy);
		}
	}
}
