using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
		public Pos parentPos;
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
		//親ノードポインターを設定
		public void SetParentNode (GameObject parentBlock)
		{
				parentPos = parentBlock.GetComponent<Block> ().blockPos;
		}

		public void DoProcess ()
		{
				StartCoroutine (ProcessLate ());
		}

		private IEnumerator ProcessLate ()
		{
				yield return new WaitForSeconds (1.0f);
				int cCost;
				int hCost;
				int score;

				//自身をOpenにする
				SetStatus (Status.Open);
				//実コストを求める

				//推定コストを求める
				hCost = caliculaterClass.CalcHCost (gameObject, blockManagerClass.targetBlock);
				SetHCost (hCost);

				//スコアを求める
				score = caliculaterClass.CalcScore (gameObject);
				SetScore (score);
				//親ノードポインタを求める

				//周りのブロックにプロセスを呼ぶ
				//周りのブロックを取得
				aroundBlock = blockManagerClass.getAroundBlocks (gameObject);
				//周りに対してDoProsessを呼ぶ
				foreach (GameObject block in aroundBlock) {
						if (block != null && block.GetComponent<Block> ().blockStatus == Status.None) {
								block.GetComponent<Block> ().DoProcess ();
						}
				}
				//終わったのでCloseにする
				SetStatus (Status.Closed);
		}
}