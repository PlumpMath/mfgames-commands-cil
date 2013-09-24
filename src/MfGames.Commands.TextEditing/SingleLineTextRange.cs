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

		public LinePosition FirstLinePosition
		{
			get
			{
				return BeginTextPosition < EndTextPosition
					? BeginTextPosition.LinePosition
					: EndTextPosition.LinePosition;
			}
		}

		public LinePosition LastLinePosition
		{
			get
			{
				return BeginTextPosition > EndTextPosition
					? BeginTextPosition.LinePosition
					: EndTextPosition.LinePosition;
			}
		}

		/// <summary>
		/// Contains the beginning character position in the line.
		/// </summary>
		public CharacterPosition BeginCharacterPosition { get; private set; }

		/// <summary>
		/// Gets a TextPosition representing the line and start character.
		/// </summary>
		public TextPosition BeginTextPosition
		{
			get
			{
				var results = new TextPosition(LinePosition, BeginCharacterPosition);
				return results;
			}
		}

		/// <summary>
		/// Contains the ending character position in the line.
		/// </summary>
		public CharacterPosition EndCharacterPosition { get; private set; }

		/// <summary>
		/// Gets a TextPosition representing the line and end character.
		/// </summary>
		public TextPosition EndTextPosition
		{
			get
			{
				var results = new TextPosition(LinePosition, EndCharacterPosition);
				return results;
			}
		}

		public CharacterPosition FirstCharacterPosition
		{
			get
			{
				return BeginCharacterPosition < EndCharacterPosition
					? BeginCharacterPosition
					: EndCharacterPosition;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this text range represents an empty
		/// selection.
		/// </summary>
		public bool IsEmpty
		{
			get { return BeginTextPosition == EndTextPosition; }
		}

		/// <summary>
		/// Gets a value indicating whether the begin point is before the
		/// end character.
		/// </summary>
		public bool IsOrdered
		{
			get
			{
				bool results = BeginCharacterPosition < EndCharacterPosition;
				return results;
			}
		}

		public CharacterPosition LastCharacterPosition
		{
			get
			{
				return BeginCharacterPosition < EndCharacterPosition
					? EndCharacterPosition
					: BeginCharacterPosition;
			}
		}

		/// <summary>
		/// Contains the line to modify.
		/// </summary>
		public LinePosition LinePosition { get; private set; }

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

			return BeginCharacterPosition.Equals(other.BeginCharacterPosition)
				&& EndCharacterPosition.Equals(other.EndCharacterPosition)
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
			return Equals((SingleLineTextRange) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = BeginCharacterPosition.GetHashCode();
				hashCode = (hashCode * 397) ^ EndCharacterPosition.GetHashCode();
				hashCode = (hashCode * 397) ^ LinePosition.GetHashCode();
				return hashCode;
			}
		}

		/// <summary>
		/// Gets a text range that is ordered so the begin character is less
		/// than the end range.
		/// </summary>
		/// <returns></returns>
		public SingleLineTextRange GetOrderedRange()
		{
			if (IsOrdered)
			{
				return this;
			}

			var results = new SingleLineTextRange(
				LinePosition, EndCharacterPosition, BeginCharacterPosition);
			return results;
		}

		public override string ToString()
		{
			return string.Format(
				"SingleLineTextRange ({0}, {1} to {2})",
				LinePosition.GetIndexString(),
				BeginCharacterPosition.GetIndexString(),
				EndCharacterPosition.GetIndexString());
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
			LinePosition = line;
			BeginCharacterPosition = characterBegin;
			EndCharacterPosition = characterEnd;
		}

		#endregion
	}
}
