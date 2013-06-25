// Copyright 2011-2013 Moonfire Games
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
		public CharacterPosition Character { get; private set; }

		/// <summary>
		/// Contains the zero-based line index.
		/// </summary>
		public LinePosition Line { get; private set; }

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
			return Character.Equals(other.Character) && Line.Equals(other.Line);
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
				return (Character.GetHashCode() * 397) ^ Line.GetHashCode();
			}
		}

		public override string ToString()
		{
			return string.Format(
				"TextPosition({0}, {1})", Line.GetIndexString(), Character.GetIndexString());
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
			if (left.Line == right.Line)
			{
				return left.Character > right.Character;
			}

			return left.Line > right.Line;
		}

		public static bool operator >=(TextPosition left,
			TextPosition right)
		{
			if (left.Line == right.Line)
			{
				return left.Character >= right.Character;
			}

			return left.Line >= right.Line;
		}

		public static bool operator !=(TextPosition left,
			TextPosition right)
		{
			return !Equals(left, right);
		}

		public static bool operator <(TextPosition left,
			TextPosition right)
		{
			if (left.Line == right.Line)
			{
				return left.Character < right.Character;
			}

			return left.Line < right.Line;
		}

		public static bool operator <=(TextPosition left,
			TextPosition right)
		{
			if (left.Line == right.Line)
			{
				return left.Character <= right.Character;
			}

			return left.Line <= right.Line;
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
