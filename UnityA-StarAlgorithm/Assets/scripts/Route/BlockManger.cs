using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManger : MonoBehaviour
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
		// blockArray
		public GameObject[,] blockArray;
		public List<RectTransform> Route;

		public void Play ()
		{
				startBlock = maker.blockObjects [startBlockPos.posX, startBlockPos.posY];
				targetBlock = maker.blockObjects [targetBlockPos.posX, targetBlockPos.posY];
				blockArray = maker.blockObjects;
				startBlock.GetComponent<Block> ().DoProcess (startBlock);
		}
		//周りのブロックを返す
		public GameObject[] getAroundBlocks (GameObject block)
		{
				GameObject[] aroundBlocks = new GameObject [8];
				int posX = block.GetComponent<Block> ().blockPos.posX;
				int posY = block.GetComponent<Block> ().blockPos.posY;

				//上のブロックを取得
				aroundBlocks [0] = getBlock (posX, posY - 1);
				aroundBlocks [1] = getBlock (posX + 1, posY - 1);
				aroundBlocks [2] = getBlock (posX + 1, posY);
				aroundBlocks [3] = getBlock (posX + 1, posY + 1);
				aroundBlocks [4] = getBlock (posX, posY + 1);
				aroundBlocks [5] = getBlock (posX - 1, posY + 1);
				aroundBlocks [6] = getBlock (posX - 1, posY);
				aroundBlocks [7] = getBlock (posX - 1, posY - 1);

				return aroundBlocks;
		}

		private GameObject getBlock (int x, int y)
		{
				if (x >= 0 && x < 5 && y >= 0 && y < 5) {
						return blockArray [x, y];

				}
				return null;
		}

		public List<GameObject> getMinimumScoreOpenBlocks (int minimumScore)
		{
				List<GameObject> MinimumScoreBlocks = new List<GameObject> ();
				int bestValue = 0;
				foreach (GameObject block in blockArray) {
						//状態がOpenだったら
						if (block.GetComponent<Block> ().blockStatus == Block.Status.Open) {
								if (block.GetComponent<Block> ().blockScore.S == minimumScore) {
										//MinimumScoreBlocksに追加
										MinimumScoreBlocks.Add (block);
								}
						}
				}
				return MinimumScoreBlocks;
		}
		//最小スコアを算出する
		public int getMinimumScore ()
		{
				int minimumScore = 9999;

				foreach (GameObject block in blockArray) {
						//状態がOpenだったら
						if (block.GetComponent<Block> ().blockStatus == Block.Status.Open) {
								if (block.GetComponent<Block> ().blockScore.S <= minimumScore) {
										minimumScore = block.GetComponent<Block> ().blockScore.S;
								}
						}
				}
				return minimumScore;
		}

		public void showRoute ()
		{
				GameObject block = targetBlock;
				Route = new List<RectTransform> ();
				Route.Add (targetBlock.GetComponent<RectTransform> ());
				while (true) {
						if (block != startBlock) {
								block.GetComponent<Block> ().SetRouteColor ();
								block = block.GetComponent<Block> ().parentBlock;
								Route.Add (block.GetComponent<RectTransform> ());
								//StartBlockだったら色だけ付けて終了
						} else {
								block.GetComponent<Block> ().SetRouteColor ();
								break;
						}
				} 
		}
}
