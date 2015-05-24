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

		void Start ()
		{
				//テキストオブジェクトを取得
				cText = gameObject.transform.FindChild ("C").gameObject.GetComponent<Text> ();
				hText = gameObject.transform.FindChild ("H").gameObject.GetComponent<Text> ();
				sText = gameObject.transform.FindChild ("S").gameObject.GetComponent<Text> ();
		}
		//全てのスコアをセット
		public void SetScore (int c, int h, int s)
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

		public void SetParentNode (GameObject parentBlock)
		{
				parentPos = parentBlock.GetComponent<Block> ().blockPos;
		}
}
