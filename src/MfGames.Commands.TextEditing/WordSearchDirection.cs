// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// An enumeration of the direction for resolving the spans of words in a 
	/// position.
	/// </summary>
	public enum WordSearchDirection
	{
		/// <summary>
		/// Indicates that <see cref="CharacterPosition"/>.NormalizeIndex() should search
		/// right in the case of a Word position.
		/// </summary>
		Right,

		/// <summary>
		/// Indicates that <see cref="CharacterPosition"/>.NormalizeIndex() should search
		/// left in the case of a Word position.
		/// </summary>
		Left,
	}
}
