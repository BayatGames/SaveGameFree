namespace Bayat.Unity.SaveGameFree.Encoders
{

	/// <summary>
	/// Interface for Save Game Encoders.
	/// </summary>
	public interface ISaveGameEncoder
	{

		/// <summary>
		/// Encode the specified input with password.
		/// </summary>
		/// <param name="input">Input.</param>
		/// <param name="password">Password.</param>
		string Encode(string input, string password);

		/// <summary>
		/// Decode the specified input with password.
		/// </summary>
		/// <param name="input">Input.</param>
		/// <param name="password">Password.</param>
		string Decode(string input, string password);

	}

}