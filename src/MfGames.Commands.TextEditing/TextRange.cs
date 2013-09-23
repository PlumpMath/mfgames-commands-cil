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

		public CharacterPosition BeginCharacterPosition
		{
			get { return BeginTextPosition.CharacterPosition; }
		}

		public LinePosition BeginLinePosition
		{
			get { return BeginTextPosition.LinePosition; }
		}

		/// <summary>
		/// Contains the starting text position for the selection.
		/// </summary>
		public TextPosition BeginTextPosition { get; private set; }

		public CharacterPosition EndCharacterPosition
		{
			get { return EndTextPosition.CharacterPosition; }
		}

		public LinePosition EndLinePosition
		{
			get { return EndTextPosition.LinePosition; }
		}

		/// <summary>
		/// Contains the ending text position for the selection.
		/// </summary>
		public TextPosition EndTextPosition { get; private set; }

		public TextPosition FirstTextPosition
		{
			get
			{
				return BeginTextPosition < EndTextPosition
					? BeginTextPosition
					: EndTextPosition;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this text range represents an empty
		/// selection.
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				bool results = BeginTextPosition == EndTextPosition;
				return results;
			}
		}

		public bool IsSameLine
		{
			get
			{
				bool results = BeginLinePosition == EndLinePosition;
				return results;
			}
		}

		public TextPosition LastTextPosition
		{
			get
			{
				return BeginTextPosition < EndTextPosition
					? EndTextPosition
					: BeginTextPosition;
			}
		}

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

			return Equals(BeginTextPosition, other.BeginTextPosition)
				&& Equals(EndTextPosition, other.EndTextPosition);
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
				return ((BeginTextPosition != null
					? BeginTextPosition.GetHashCode()
					: 0) * 397) ^ (EndTextPosition != null
						? EndTextPosition.GetHashCode()
						: 0);
			}
		}

		public override string ToString()
		{
			return string.Format(
				"TextRange(({0}, {1}) to ({2}, {3}))",
				BeginTextPosition.LinePosition.GetIndexString(),
				BeginTextPosition.CharacterPosition.GetIndexString(),
				EndTextPosition.LinePosition.GetIndexString(),
				EndTextPosition.CharacterPosition.GetIndexString());
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
				new TextPosition(range.LinePosition, range.BeginCharacterPosition),
				new TextPosition(range.LinePosition, range.EndCharacterPosition))
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
			BeginTextPosition = begin;
			EndTextPosition = end;
		}

		#endregion
	}
}
