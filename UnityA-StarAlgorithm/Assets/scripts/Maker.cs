using UnityEngine;
using System.Collections;

public class Maker : MonoBehaviour
{
		public GameObject blockPrefab;
		public int stageHeight = 5;
		public int stageWidth = 5;
		public GameObject[,] blockObjects;
		public GameObject stage;

		void Awake ()
		{
				blockObjects = new GameObject[stageHeight, stageWidth];
				initStage ();
		}

		public void initStage ()
		{
				for (int h = 0; h <= stageHeight - 1; h++) {
						for (int w = 0; w <= stageWidth - 1; w++) {
								//blockを生成
								GameObject block = Instantiate (blockPrefab) as GameObject;
//								座標を設定
								block.GetComponent<Block> ().SetPosition (w, h);
								//ステータスをNoneに設定
								block.GetComponent<Block> ().SetStatus (Block.Status.None);
								//ステージに配置
								block.transform.SetParent (stage.transform, false);
								//配列に格納
								blockObjects [w, h] = block;
						}
				}
		}
}
