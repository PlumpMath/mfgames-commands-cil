// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// Represents a range of characters on a single line.
	/// </summary>
	public class SingleLineTextRange
	{
		#region Properties

		/// <summary>
		/// Contains the beginning character position in the line.
		/// </summary>
		public CharacterPosition CharacterBegin { get; private set; }

		/// <summary>
		/// Contains the ending character position in the line.
		/// </summary>
		public CharacterPosition CharacterEnd { get; private set; }

		/// <summary>
		/// Contains the line to modify.
		/// </summary>
		public LinePosition Line { get; private set; }

		#endregion

		#region Methods

		public override string ToString()
		{
			return string.Format(
				"SingleLineTextRange ({0}, {1} to {2})",
				Line.GetIndexString(),
				CharacterBegin.GetIndexString(),
				CharacterEnd.GetIndexString());
		}

		#endregion

		#region Constructors

		public SingleLineTextRange(
			LinePosition line,
			CharacterPosition characterBegin,
			CharacterPosition characterEnd)
		{
			Line = line;
			CharacterBegin = characterBegin;
			CharacterEnd = characterEnd;
		}

		#endregion
	}
}
