// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// A range in a text buffer with the two anchors of the selection
	/// being represented by <see cref="TextPosition"/>. This is an immutable class.
	/// </summary>
	public class TextRange: IEquatable<TextRange>
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

		public bool Equals(TextRange other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}
			return Equals(Begin, other.Begin) && Equals(End, other.End);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((TextRange) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Begin != null
					? Begin.GetHashCode()
					: 0) * 397) ^ (End != null
						? End.GetHashCode()
						: 0);
			}
		}

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

		public static bool operator ==(TextRange left,
			TextRange right)
		{
			return Equals(left, right);
		}

		public static implicit operator TextRange(SingleLineTextRange range)
		{
			var textRange = new TextRange(range);
			return textRange;
		}

		public static bool operator !=(TextRange left,
			TextRange right)
		{
			return !Equals(left, right);
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
			if (begin == null)
			{
				throw new ArgumentNullException("begin");
			}
			if (end == null)
			{
				throw new ArgumentNullException("end");
			}

			// Save the positions as member variables.
			Begin = begin;
			End = end;
		}

		#endregion
	}
}
