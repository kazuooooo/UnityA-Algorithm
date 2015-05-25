using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
		//プレイヤーの状態
		public enum pStatus
		{
				IDLE = -1,
				MOVE = 0,
		}
		//通るルート
		public List<RectTransform> reversedRoute;
		public  RectTransform[] intermediateArray;
		public List<RectTransform> route;
		public BlockManger blockManagerClass;

		void Awake ()
		{
				blockManagerClass = GameObject.Find ("BlockManager").GetComponent<BlockManger> ();

		}

		public void PlayerRun ()
		{

				StartCoroutine (PlayerRunCoroutine ());
		}

		IEnumerator PlayerRunCoroutine ()
		{
				//BlockManagerからルートを取得(逆で)
				reversedRoute = blockManagerClass.Route;
				intermediateArray = new RectTransform[reversedRoute.Count];
				reversedRoute.CopyTo (intermediateArray);
				route = new List<RectTransform> (intermediateArray);
				route.Reverse ();

				//順番に走らせる
				foreach (RectTransform point in route) {
						MoveTo (point);
						yield return new WaitForSeconds (1.0f);
				}
		}

		public void MoveTo (RectTransform target)
		{
				iTween.MoveTo (gameObject, target.position, 1.0f);
		}
}
