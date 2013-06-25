﻿// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// An immutable class that represents a position inside a text buffer. This
	/// is a vector of line index and character indent, both zero-based. The index
	/// is always to the left of the character with the last possible line being
	/// a character index equal to the length.
	/// 
	/// The system in general assumes that line index refers to full lines, not the
	/// broken up lines that would resulting from word wrapping. In many cases, this
	/// would be paragraphs and headers instead of multiple lines of a very long
	/// paragraph.
	/// </summary>
	public class TextPosition
	{
		#region Properties

		/// <summary>
		/// Contains the zero-based character index.
		/// </summary>
		public CharacterPosition Character { get; private set; }

		/// <summary>
		/// Contains the zero-based line index.
		/// </summary>
		public LinePosition Line { get; private set; }

		#endregion

		#region Methods

		public override string ToString()
		{
			return string.Format(
				"TextPosition({0}, {1})", Line.GetIndexString(), Character.GetIndexString());
		}

		#endregion

		#region Constructors

		public TextPosition(
			LinePosition line,
			CharacterPosition character)
		{
			Line = line;
			Character = character;
		}

		#endregion
	}
}
