using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
		public StageCreator Maker;
		// Use this for initialization
		void Awake ()
		{
				Maker = GameObject.FindWithTag ("Maker").GetComponent<StageCreator> ();
			
		}

		public void MakeStage (int num)
		{
				Maker.makeStage (num);
		}
}
