using UnityEngine;
using System.Collections;

public class Caliculater : MonoBehaviour
{
		//実コストを計算
		public int CalcCCost (GameObject block)
		{
				return 0;
		}
		//推定スコアを計算
		public int CalcHCost (GameObject block, GameObject targetBlock)
		{
				int hCost = 0;
				//x座標の差分を取得
				int dx = System.Math.Abs (block.GetComponent<Block> ().blockPos.posX - targetBlock.GetComponent<Block> ().blockPos.posX);
				print ("dx" + dx.ToString ());
				//y座標の差分を取得
				int dy = System.Math.Abs (block.GetComponent<Block> ().blockPos.posY - targetBlock.GetComponent<Block> ().blockPos.posY);
				print ("dx" + dy.ToString ());
				//大きい方をhCostに設定
				if (dx >= dy) {
						hCost = dx;
				} else {
						hCost = dy;
				}
				return hCost;
		}
		//スコアを計算
		public int CalcScore (GameObject block)
		{
				int cCost = block.GetComponent<Block> ().blockScore.C;
				int hCost = block.GetComponent<Block> ().blockScore.H;
				int score = cCost + hCost;
				return score;
		}
}
