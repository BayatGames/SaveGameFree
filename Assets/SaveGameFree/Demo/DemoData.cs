using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveGameFree
{

	[Serializable]
	public class DemoData
	{

		public int clickCount = 0;
		public string yourName = "Enter Your Name";

		public override string ToString ()
		{
			return string.Format ( "[DemoData] Click Count: {0}, Your Name: {1}", clickCount, yourName );
		}

	}

}