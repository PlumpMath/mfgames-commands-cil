﻿// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;

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
	public class TextPosition: IEquatable<TextPosition>
	{
		#region Properties

		/// <summary>
		/// Contains the zero-based character index.
		/// </summary>
		public CharacterPosition CharacterPosition { get; private set; }

		/// <summary>
		/// Contains the zero-based line index.
		/// </summary>
		public LinePosition LinePosition { get; private set; }

		#endregion

		#region Methods

		public bool Equals(TextPosition other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return CharacterPosition.Equals(other.CharacterPosition)
				&& LinePosition.Equals(other.LinePosition);
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

			return Equals((TextPosition) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (CharacterPosition.GetHashCode() * 397) ^ LinePosition.GetHashCode();
			}
		}

		public override string ToString()
		{
			return string.Format(
				"TextPosition({0}, {1})",
				LinePosition.GetIndexString(),
				CharacterPosition.GetIndexString());
		}

		#endregion

		#region Operators

		public static bool operator ==(TextPosition left,
			TextPosition right)
		{
			return Equals(left, right);
		}

		public static bool operator >(TextPosition left,
			TextPosition right)
		{
			// If the line position is on the same line, then we use the
			// character positions.
			if (left.LinePosition == right.LinePosition)
			{
				// Check for the symbolic versions first. In general, End is
				// always greater than anything else.
				if (left.CharacterPosition == CharacterPosition.End)
				{
					return true;
				}

				if (right.CharacterPosition == CharacterPosition.End)
				{
					return false;
				}

				// Word is a bit more complicated. If the left is a word, then
				// it is left than right. If right is a word, then left is always
				// less than right.
				if (left.CharacterPosition == CharacterPosition.Word)
				{
					return false;
				}

				if (right.CharacterPosition == CharacterPosition.Word)
				{
					return true;
				}

				// In all other cases, we use the index.
				return left.CharacterPosition > right.CharacterPosition;
			}

			return left.LinePosition > right.LinePosition;
		}

		public static bool operator >=(TextPosition left,
			TextPosition right)
		{
			return left > right || left == right;
		}

		public static bool operator !=(TextPosition left,
			TextPosition right)
		{
			return !Equals(left, right);
		}

		public static bool operator <(TextPosition left,
			TextPosition right)
		{
			return !(left >= right);
		}

		public static bool operator <=(TextPosition left,
			TextPosition right)
		{
			return !(left > right);
		}

		#endregion

		#region Constructors

		public TextPosition(
			LinePosition line,
			CharacterPosition character)
		{
			LinePosition = line;
			CharacterPosition = character;
		}

		#endregion
	}
}
