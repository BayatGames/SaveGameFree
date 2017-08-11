using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace BayatGames.SaveGameFree.Tests
{

	public class SaveGameTests
	{

		[Test]
		public void SaveTests ()
		{

			// Null identifier
			Assert.Catch ( () =>
			{
				SaveGame.Save<string> ( null, null );
			} );

			// Empty identifier
			Assert.Catch ( () =>
			{
				SaveGame.Save<string> ( "", null );
			} );

			// Simple save/load
			SaveGame.Save<string> ( "test/save", "saved" );
			Assert.IsTrue ( SaveGame.Exists ( "test/save" ) );
			Assert.AreEqual ( SaveGame.Load<string> ( "test/save", "not saved" ), "saved" );

			// Clear at end
			SaveGame.Clear ();
		}

		[Test]
		public void LoadTests ()
		{

			// Null identifier
			Assert.Catch ( () =>
			{
				SaveGame.Load<string> ( null, "" );
			} );

			// Empty identifier
			Assert.Catch ( () =>
			{
				SaveGame.Load<string> ( "", "" );
			} );

			// Simple save/load
			SaveGame.Save<string> ( "test/load", "saved" );
			Assert.IsTrue ( SaveGame.Exists ( "test/load" ) );
			Assert.AreEqual ( SaveGame.Load<string> ( "test/load", "not saved" ), "saved" );

			// Reset to default
			Assert.IsFalse ( SaveGame.Exists ( "test/load2" ) );
			Assert.AreEqual ( SaveGame.Load<string> ( "test/load2", "not saved" ), "not saved" );

			// Clear at end
			SaveGame.Clear ();
		}

		[Test]
		public void ExistsTests ()
		{

			// Null identifier
			Assert.Catch ( () =>
			{
				SaveGame.Exists ( null );
			} );

			// Empty identifier
			Assert.Catch ( () =>
			{
				SaveGame.Exists ( "" );
			} );

			// Check existent
			Assert.IsFalse ( SaveGame.Exists ( "test/exists" ) );
			SaveGame.Save<string> ( "test/exists", "saved" );
			Assert.IsTrue ( SaveGame.Exists ( "test/exists" ) );

			// Clear at end
			SaveGame.Clear ();
		}

		[Test]
		public void DeleteTests ()
		{

			// Null identifier
			Assert.Catch ( () =>
			{
				SaveGame.Delete ( null );
			} );

			// Empty identifier
			Assert.Catch ( () =>
			{
				SaveGame.Delete ( "" );
			} );

			// Simple delete
			SaveGame.Save<string> ( "test/delete", "saved" );
			Assert.IsTrue ( SaveGame.Exists ( "test/delete" ) );
			SaveGame.Delete ( "test/delete" );
			Assert.IsFalse ( SaveGame.Exists ( "test/delete" ) );
			Assert.AreEqual ( SaveGame.Load<string> ( "test/delete", "not saved" ), "not saved" );

			// Clear at end
			SaveGame.Clear ();
		}

		[Test]
		public void ClearTests ()
		{
			
			// Clear all
			SaveGame.Save<string> ( "test/clear", "saved" );
			SaveGame.Clear ();
			Assert.IsFalse ( SaveGame.Exists ( "test/clear" ) );
			Assert.AreEqual ( SaveGame.Load<string> ( "test/clear", "not saved" ), "not saved" );
		}
		
	}

}