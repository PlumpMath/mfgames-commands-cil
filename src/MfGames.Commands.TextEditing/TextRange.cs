﻿// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;
using System.Diagnostics.Contracts;

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// A range in a text buffer with the two anchors of the selection
	/// being represented by <see cref="TextPosition"/>. This is an immutable class.
	/// </summary>
	public class TextRange
	{
		#region Properties

		/// <summary>
		/// Contains the starting text position for the selection.
		/// </summary>
		public TextPosition Begin { get; private set; }

		/// <summary>
		/// Contains the ending text position for the selection.
		/// </summary>
		public TextPosition End { get; private set; }

		#endregion

		#region Methods

		public override string ToString()
		{
			return string.Format(
				"TextRange(({0}, {1}) to ({2}, {3}))",
				Begin.Line.GetIndexString(),
				Begin.Character.GetIndexString(),
				End.Line.GetIndexString(),
				End.Character.GetIndexString());
		}

		#endregion

		#region Operators

		public static implicit operator TextRange(SingleLineTextRange range)
		{
			var range = new TextRange(range);
			return range;
		}

		#endregion

		#region Constructors

		public TextRange(SingleLineTextRange range)
			: this(
				new TextPosition(range.Line, range.CharacterBegin),
				new TextPosition(range.Line, range.CharacterEnd))
		{
		}

		public TextRange(
			TextPosition begin,
			TextPosition end)
		{
			// Establish our code contracts.
			Contract.Requires<ArgumentNullException>(begin != null);
			Contract.Requires<ArgumentNullException>(end != null);

			// Save the positions as member variables.
			Begin = begin;
			End = end;
		}

		#endregion
	}
}
