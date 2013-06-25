// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// Represents a range of characters on a single line.
	/// </summary>
	public class SingleLineTextRange: IEquatable<SingleLineTextRange>
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

		public bool Equals(SingleLineTextRange other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}
			return CharacterBegin.Equals(other.CharacterBegin)
				&& CharacterEnd.Equals(other.CharacterEnd) && Line.Equals(other.Line);
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
			return Equals((SingleLineTextRange) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = CharacterBegin.GetHashCode();
				hashCode = (hashCode * 397) ^ CharacterEnd.GetHashCode();
				hashCode = (hashCode * 397) ^ Line.GetHashCode();
				return hashCode;
			}
		}

		public override string ToString()
		{
			return string.Format(
				"SingleLineTextRange ({0}, {1} to {2})",
				Line.GetIndexString(),
				CharacterBegin.GetIndexString(),
				CharacterEnd.GetIndexString());
		}

		#endregion

		#region Operators

		public static bool operator ==(SingleLineTextRange left,
			SingleLineTextRange right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(SingleLineTextRange left,
			SingleLineTextRange right)
		{
			return !Equals(left, right);
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
