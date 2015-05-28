using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Pos
{
		public int posX;
		public int posY;
}

public class Block : MonoBehaviour
{
		//ステータスのenum
		public enum Status
		{
				None = 0,
				Open = 1,
				Closed = -1,
				Unabled = -2,
		}
		//スコアの構造体
		public struct Score
		{
				public int C;
				public int H;
				public int S;
		}
		//XY座標
		public Pos blockPos;
		//ステータス
		public Status blockStatus;
		//スコア
		public Score blockScore;
		private Text cText;
		private Text hText;
		private Text sText;
		//親ノードクラスのポインタ
		public GameObject parentBlock;
		//BlockManager
		private BlockManger blockManagerClass;
		private Caliculater caliculaterClass;
		public GameObject[] aroundBlock;

		void Start ()
		{
				blockManagerClass = GameObject.Find ("BlockManager").gameObject.GetComponent<BlockManger> ();
				caliculaterClass = GameObject.Find ("Caliculator").gameObject.GetComponent<Caliculater> ();
				//テキストオブジェクトを取得
				cText = gameObject.transform.FindChild ("C").gameObject.GetComponent<Text> ();
				hText = gameObject.transform.FindChild ("H").gameObject.GetComponent<Text> ();
				sText = gameObject.transform.FindChild ("S").gameObject.GetComponent<Text> ();
		}
		//全てのスコアをセット
		public void SetProperty (int c, int h, int s)
		{
				SetCCost (c);
				SetHCost (h);
				SetScore (s);
		}
		//実コストをセット
		public void  SetCCost (int value)
		{
				blockScore.C = value;
				cText.text = "C = " + value.ToString ();
		}
		//推定コストをセット
		public void  SetHCost (int value)
		{
				blockScore.H = value;
				hText.text = "H = " + value.ToString ();
		}
		//スコアをセット
		public void  SetScore (int value)
		{
				blockScore.S = value;
				sText.text = "S = " + value.ToString ();
		}
		//座標をセット
		public void SetPosition (int x, int y)
		{
				blockPos.posX = x;
				blockPos.posY = y;
		}
		//ステータスをセット
		public void SetStatus (Block.Status value)
		{
				blockStatus = value;
				switch (value) {
				case Status.None:
						gameObject.GetComponent<Image> ().color = Color.white;
						break;
				case Status.Open:
						gameObject.GetComponent<Image> ().color = Color.red;
						break;
				case Status.Closed:
						gameObject.GetComponent<Image> ().color = Color.green;
						break;
				}
		}

		public void DoProcess (GameObject parentBlock)
		{
				if (blockManagerClass.targetBlock.GetComponent<Block> ().blockStatus != Status.Closed) {
						StartCoroutine (ProcessLate (parentBlock));
				}
		}

		private IEnumerator ProcessLate (GameObject parentBlock)
		{
				yield return new WaitForSeconds (1.0f);
				int cCost;
				int hCost;
				int score;

				//スタートブロックの場合
				if (gameObject == blockManagerClass.startBlock) {
						//自身をOpenにする
						SetStatus (Status.Open);
						cCost = 0;
						SetCCost (cCost);
						hCost = caliculaterClass.CalcHCost (gameObject, blockManagerClass.targetBlock);
						SetHCost (hCost);
						score = caliculaterClass.CalcScore (gameObject);
						SetScore (score);
				}
						
				print ("Process" + gameObject.name);
				//周りのブロックを取得
				aroundBlock = blockManagerClass.getAroundBlocks (gameObject);

				//周りにブロックをOpenにする。
				foreach (GameObject block in aroundBlock) {
						if (block != null && block.GetComponent<Block> ().blockStatus == Status.None) {
								Block blockClass = block.GetComponent<Block> ();
								//openにする
								blockClass.SetStatus (Status.Open);
								//実コストを設定
								cCost = this.blockScore.C + 1;
								blockClass.SetCCost (cCost);
								//推定コストを求める
								hCost = caliculaterClass.CalcHCost (block, blockManagerClass.targetBlock);
								blockClass.SetHCost (hCost);
								//スコアを求める
								score = caliculaterClass.CalcScore (block);
								blockClass.SetScore (score);
								//親ノードに設定
								blockClass.parentBlock = gameObject;
						}
				}

				//終わったのでCloseにする
				SetStatus (Status.Closed);

				//Openのブロックの中から一番スコアの低いものに対してDoProcessを呼ぶ
				if (gameObject != blockManagerClass.targetBlock) {
						int minimumScore = blockManagerClass.getMinimumScore ();
						List<GameObject> nextBlocks = blockManagerClass.getMinimumScoreOpenBlocks (minimumScore);
						foreach (GameObject block in nextBlocks) {
								block.GetComponent<Block> ().DoProcess (gameObject);
						}
				}
						
		}

		public void SetRouteColor ()
		{
				print ("callRouteColor");
				gameObject.GetComponent<Image> ().color = Color.yellow;
		}

		public void SetUnableBlock ()
		{
				gameObject.GetComponent<Image> ().color = Color.black;
				blockStatus = Block.Status.Unabled;
		}
}