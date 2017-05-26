using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaveGameFree
{

	public class Demo : MonoBehaviour
	{

		public DemoData demoData;
		public string fileName = "gameData";

		/// <summary>
		/// Use awake function to initialize our game save and load.
		/// </summary>
		void Awake ()
		{

			Saver.OnSaved += Saver_OnSaved;
			Saver.OnLoaded += Saver_OnLoaded;

			// Initialize our game data
			demoData = new DemoData ();

			// Initialize the Saver with the default configurations
			Saver.Initialize ();

			// Load game data after initialization
			demoData = Saver.Load<DemoData> ( fileName );

		}

		void Saver_OnLoaded ( object obj )
		{
			Debug.Log ( "Loaded Successfully: " + obj.ToString () );
		}

		void Saver_OnSaved ( object obj )
		{
			Debug.Log ( "Saved Succesfully: " + obj.ToString () );
		}

		void OnGUI ()
		{
			GUILayout.Label ( "This will get saved automatically when you change or input." );
			if ( GUILayout.Button ( string.Format ( "Click Count: {0}", demoData.clickCount ) ) )
			{
				demoData.clickCount++;
			}
			demoData.yourName = GUILayout.TextField ( demoData.yourName );
			if ( GUILayout.Button ( "Save" ) )
			{
				// Save the game data
				Saver.Save ( demoData, fileName );
			}
			if ( GUILayout.Button ( "Load" ) )
			{
				// Load the game data
				demoData = Saver.Load<DemoData> ( fileName );
			}
			if ( GUILayout.Button ( "Reload" ) )
			{
				Application.LoadLevel ( Application.loadedLevel );
			}
		}

	}

}