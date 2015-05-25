using UnityEngine;
using System.Collections;

public class Scoreler : MonoBehaviour
{
		//開始ブロック
		public Pos startBlockPos;
		public GameObject startBlock;
		//目的地ブロック
		public Pos targetBlockPos;
		public GameObject targetBlock;
		//Caliculater
		public Caliculater caliculator;
		//Maker
		public Maker maker;
		// Use this for initialization
		public void Play ()
		{
				startBlock = maker.blockObjects [startBlockPos.posX, startBlockPos.posY];
				targetBlock = maker.blockObjects [targetBlockPos.posX, targetBlockPos.posY];
				initScore ();
		}

		private void initScore ()
		{
				int cCost = 0;
				int hCost;
				int score;
				//statusをOpenに変更
				startBlock.GetComponent<Block> ().SetStatus (Block.Status.Open);
				//hCostを設定
				hCost = caliculator.CalcHCost (startBlock, targetBlock);
				startBlock.GetComponent<Block> ().SetHCost (hCost);
				//scoreを設定
				score = caliculator.CalcScore (startBlock);
				startBlock.GetComponent<Block> ().SetScore (score);
				//親ノードはNoneなので何もしない

		}
}
