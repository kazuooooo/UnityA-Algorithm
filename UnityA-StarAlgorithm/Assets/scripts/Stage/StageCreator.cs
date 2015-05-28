using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageCreator : MonoBehaviour
{
		//配置するオブジェクトの一覧
		public GameObject[] tileObjectPrefabs;
		//実際に配置したゲームオブジェクト
		public GameObject[,] tileObjectsArray;
		//タイル1枚あたりの高さ
		private float tileXWidth;
		//タイル1枚あたりの幅
		private float tileYWidth;
		//ステージのX方向タイル数
		public int stageXTilesNum;
		//ステージのY方向タイル数
		public int stageYTilesNum;
		//X方向単位ベクトル
		Vector2 xVec;
		//Y方向単位ベクトル
		Vector2 yVec;
		//タイル情報と番号を紐付けたhashtable
		private Hashtable tileHash;
		private StageTextDataAnalyzer textAnalyzerClass;
		public TextAsset[] stageText;
		private GameObject stageObj;

		void Start ()
		{

				//stageObjを取得
				stageObj = GameObject.Find ("Stage") as GameObject;
				//stageTextDataAnalyzerクラスを取得
				textAnalyzerClass = gameObject.GetComponent<StageTextDataAnalyzer> ();
				//タイル1つあたりのX,Y方向のサイズを取得
				tileXWidth = tileObjectPrefabs [0].gameObject.GetComponent<RectTransform> ().localScale.x;
				tileYWidth = tileObjectPrefabs [0].gameObject.GetComponent<RectTransform> ().localScale.y;
				//X,Y方向の単位ベクトルを計算
				xVec = new Vector2 (tileXWidth / 2f, -tileYWidth / 2f);
				yVec = new Vector2 (-tileXWidth / 2f, -tileYWidth / 2f);
				//Textデータと紐付けたHashテーブルを作成
				tileHash = new Hashtable ();
				tileHash.Add (1, tileObjectPrefabs [0]);
				tileHash.Add (2, tileObjectPrefabs [0]);
				tileHash.Add (3, tileObjectPrefabs [0]);
				tileHash.Add (4, tileObjectPrefabs [0]);
				tileHash.Add (5, tileObjectPrefabs [0]);
				tileHash.Add (6, tileObjectPrefabs [0]);
				tileHash.Add (7, tileObjectPrefabs [0]);
				tileHash.Add (8, tileObjectPrefabs [0]);
				tileHash.Add (9, tileObjectPrefabs [0]);
				tileHash.Add (10, tileObjectPrefabs [0]);
		}

		public void makeStage (int stageNum)
		{
				print ("makeStage");

				//DrawCallが高ければhttp://qiita.com/kuuki_yomenaio/items/eaea133479bb8be96870
				//に書いてある方法でバッチングした方がよいかも
				//指定したステージ番号のステージのanalyzeDataを取得
				int[,] analyzedTextData = textAnalyzerClass.GetTileData (stageText [stageNum - 1]);
				//ステージのX方向、Y方向のタイル数を取得
				stageXTilesNum = analyzedTextData.GetLength (0);
				stageYTilesNum = analyzedTextData.GetLength (1);

				//作ったゲームオブジェクトを座標に合わせて格納する２次元配列を生成
				tileObjectsArray = new GameObject[stageXTilesNum, stageYTilesNum];

				//blockObject
				GameObject tileObj = null;

				//パネルをずらして順番に設置
				for (int y = 0; y <= stageYTilesNum - 1; y++) {
						for (int x = 0; x <= stageXTilesNum - 1; x++) {
								//tileを生成
								tileObj = Instantiate (tileHash [analyzedTextData [x, y]]as GameObject, getTilePosition (x, y), Quaternion.identity) as GameObject;
								//tileのステータスをNoneに設定
								tileObj.GetComponent<Block> ().SetStatus (Block.Status.None);
								//座標を設定
								tileObj.GetComponent<Block> ().SetPosition (x, y);
								//名前を座標情報に合わせて変更
								tileObj.name = "Tile(" + x.ToString () + "," + y.ToString () + ")"; 
								//stageObjを親に設定
								tileObj.transform.SetParent (stageObj.transform, false);
								//配列に格納
								tileObjectsArray [x, y] = tileObj; 
						}
				}
		}

		public Vector2 getTilePosition (int x, int y)
		{
				Vector2 tilePos = new Vector2 (xVec.x * (x - 0.5f) + yVec.x * (y - 0.5f), xVec.y * (x - 0.5f) + yVec.y * (y - 0.5f)); 
				print ("tilePos" + tilePos);
				return tilePos;
		}
}
