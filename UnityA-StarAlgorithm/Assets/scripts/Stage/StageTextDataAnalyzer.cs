using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageTextDataAnalyzer : MonoBehaviour
{
		//生のテキスト
		string stageText;
		//ステージを業単位に分解したもの
		string[] lines;
		//ステージの高さ
		private int stageHeight;
		//ステージの幅
		private int stageWidth;
		int[,] tileData;
		int[,] tileDataSend;
		int num_x = 0;
		int num_y = 0;
		public int[,] testData;
		//取得した行を入れるリスト
		// Use this for initialization
		//タイルデータを２次元配列で取得
		public int[,] GetTileData (TextAsset stageDataText)
		{
				//文字列を取得
				stageText = stageDataText.text;
				//文字列を業単位に分割
				lines = stageText.Split ('\n');
				//ステージの高さ、幅を取得
				stageWidth = GetParam ("width");
				stageHeight = GetParam ("height");
				List<string> tileLineDataList = new List<string> ();
				int[,] tileData = new int[stageWidth, stageHeight];

       
				//データの始まりの行を取得
				int dataLineNum = System.Array.IndexOf (lines, "type=GroundLayer") + 2;


				//空白になるまで
				while (lines [dataLineNum] != "") {
						//行ごとのデータを取得してリストに格納
						tileLineDataList.Add (lines [dataLineNum]);
						dataLineNum++;
				}

				foreach (string tileLine in tileLineDataList) {
						//カンマで区切る
						string[] tile_list = tileLine.Split (',');

						//区切った数字を２次元配列に格納
						foreach (string tile in tile_list) {

								if (tile == "") {
										num_x = 0;
										num_y++;
								}

								if (tile != "") {
										tileData [num_x, num_y] = int.Parse (tile);
										// Debug.Log ("(" + num_x + "," + num_y + ")" + tile);
										num_x++;
								}


						}
				}

				return tileData;
            
		}
		//必要なパラメータを取得して返す(ex)height = 10みたいなものに使用)
		private int GetParam (string requisiteData)
		{
				int param = 0;
				//読み込み
				foreach (string line in lines) {
						if (line.StartsWith (requisiteData)) {
								string[] words = line.Split ('=');
								param = int.Parse (words [1]);
								// Debug.Log (param + param);
						}
				}
				return param;
		}
		//デバッグ用
		private void printAllData ()
		{
				foreach (string line in lines) {
						Debug.Log (line);
				}
		}

		public T GetAtIndex<T> (T[] pArray, int i)
		{
				return pArray [i];
		}
}
