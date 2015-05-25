using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageCreator : MonoBehaviour
{
		//配置するオブジェクトの一覧
		public GameObject[] tileObjects;
		//タイル1枚あたりの高さ
		private float tileXWidth;
		//タイル1枚あたりの幅
		private float tileZWidth;
		//タイル情報と番号を紐付けたhashtable
		private Hashtable tileHash;
		private StageTextDataAnalyzer textAnalyzerClass;
		public TextAsset[] stageText;
		private GameObject stageObj;

		void Awake ()
		{
				//stageObjを取得
				stageObj = GameObject.Find ("Stage") as GameObject;
				//stageTextDataAnalyzerクラスを取得
				textAnalyzerClass = gameObject.GetComponent<StageTextDataAnalyzer> ();
				//タイルのx方向の大きさを取っておく
				tileZWidth = tileObjects [0].gameObject.renderer.bounds.size.z;
				//タイルのY方向の大きさ取っておく
				tileXWidth = tileObjects [0].gameObject.renderer.bounds.size.x;
				//Hashテーブルを作成
				tileHash = new Hashtable ();
				//hash(ID,Object)
				tileHash.Add (1, tileObjects [0]);
				tileHash.Add (2, tileObjects [1]);
				tileHash.Add (3, tileObjects [2]);
				tileHash.Add (4, tileObjects [3]);
				tileHash.Add (5, tileObjects [4]);
				tileHash.Add (6, tileObjects [5]);
				tileHash.Add (7, tileObjects [6]);
				tileHash.Add (8, tileObjects [7]);
				tileHash.Add (9, tileObjects [8]);
				tileHash.Add (10, tileObjects [9]);

				makeStage (1);
		}

		public void makeStage (int stageNum)
		{
				//指定したステージ番号のステージのanalyzeDataを取得
				int[,] analyzedTextData = textAnalyzerClass.GetTileData (stageText [stageNum - 1]);
				//ステージのX方向、Z方向の幅を取得
				int stageZWidth = analyzedTextData.GetLength (0);
				int stageXWidth = analyzedTextData.GetLength (1);
				//blockObject
				GameObject blockObj = null;
				//幅-1が0,0から始まる
				int tileZ = stageZWidth - 1;
				int tileX = stageXWidth - 1;
				float tilePosX = 0;
				float tilePosY = 0;
				float tilePosZ = 0;



				//パネルをずらして順番に設置
				for (int z = 1; z <= stageZWidth; z++) {
						for (int x = 1; x <= stageXWidth; x++) {
								print ("panel_x" + tileX + "panel_z" + tileZ);
								blockObj = Instantiate (tileHash [analyzedTextData [tileZ, tileX]]as GameObject, new Vector3 (tilePosX, tilePosY, tilePosZ), Quaternion.identity) as GameObject;
								blockObj.transform.SetParent (stageObj.transform, false);
								//座標をx方向に１つずらす
								tileX--;
								//Xのpositionを幅分ずらす
								tilePosX += tileXWidth;
						}
						//座標をz方向に１つずらす
						tileZ--;
						//Zのpositionを幅分ずらす
						tilePosZ += tileZWidth;
						//x座標を初期値に戻す
						tileX = stageXWidth - 1;
						//Xのpositionを初期値に戻す
						tilePosX = 0;

				}
		}
}
