using UnityEngine;

namespace Bayat.Unity.SaveGameFree.Examples
{

	public class ExampleMoveObject : MonoBehaviour
	{

		void Update()
		{
			Vector3 position = transform.position;
			position.x += Input.GetAxis("Horizontal");
			position.y += Input.GetAxis("Vertical");
			transform.position = position;
		}

	}

}