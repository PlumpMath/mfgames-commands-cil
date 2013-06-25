// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;
using System.Diagnostics.Contracts;

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// A structure for representing a zero-based index for lines.
	/// </summary>
	public struct LinePosition: IEquatable<LinePosition>
	{
		#region Methods

		public bool Equals(LinePosition other)
		{
			return Index == other.Index;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			return obj is LinePosition && Equals((LinePosition) obj);
		}

		public override int GetHashCode()
		{
			return Index;
		}

		/// <summary>
		/// Formats the index into a symbolic string.
		/// </summary>
		/// <returns>The numeric value or a symbol for Begin, End, or Word.</returns>
		public string GetIndexString()
		{
			// Figure out the formatting value for the string.
			string value;

			switch (Index)
			{
				case EndIndex:
					value = "End";
					break;
				case BeginIndex:
					value = "Begin";
					break;
				default:
					value = Index.ToString("N0");
					break;
			}
			return value;
		}

		/// <summary>
		/// Translates the magic values (End, Beginning) into actual values based
		/// on the given count.
		/// </summary>
		/// <param name="count">The number of items in the current collection.</param>
		/// <returns>The normalized index.</returns>
		/// <exception cref="System.IndexOutOfRangeException">Encountered an invalid index:  + Index</exception>
		public int NormalizeIndex(int count)
		{
			// All the magic values are negative, so if we don't have one, there is
			// nothing to do.
			if (Index >= 0)
			{
				return Index;
			}

			// If we have the end magic number, then the index is equal to the end
			// of the text line.
			if (Index == End.Index)
			{
				return count;
			}

			// If we got this far, we don't know how to process this.
			throw new IndexOutOfRangeException("Encountered an invalid index: " + Index);
		}

		public override string ToString()
		{
			string value = GetIndexString();
			return string.Format("LinePosition({0})", value);
		}

		#endregion

		#region Operators

		public static bool operator ==(LinePosition left,
			LinePosition right)
		{
			return left.Equals(right);
		}

		public static explicit operator int(LinePosition characterPosition)
		{
			int index = characterPosition.Index;
			return index;
		}

		public static bool operator >(LinePosition left,
			LinePosition right)
		{
			return left.Index > right.Index;
		}

		public static bool operator >=(LinePosition left,
			LinePosition right)
		{
			return left.Index >= right.Index;
		}

		public static implicit operator LinePosition(int index)
		{
			var position = new LinePosition(index);
			return position;
		}

		public static bool operator !=(LinePosition left,
			LinePosition right)
		{
			return !left.Equals(right);
		}

		public static bool operator <(LinePosition left,
			LinePosition right)
		{
			return left.Index < right.Index;
		}

		public static bool operator <=(LinePosition left,
			LinePosition right)
		{
			return left.Index <= right.Index;
		}

		#endregion

		#region Constructors

		public LinePosition(int index)
		{
			// Establish our contracts.
			Contract.Requires<ArgumentOutOfRangeException>(
				index == EndIndex || index >= 0);

			// Save the index for later.
			Index = index;
		}

		public LinePosition(LinePosition line)
			: this(line.Index)
		{
		}

		#endregion

		#region Fields

		private const int BeginIndex = 0;
		private const int EndIndex = -991;

		/// <summary>
		/// A position that represents the beginning of a line or range.
		/// </summary>
		public static CharacterPosition Begin = new CharacterPosition(BeginIndex);

		/// <summary>
		/// A magic number that represents the end of a buffer (for Line) or the end
		/// of the line (for Character).
		/// </summary>
		public static readonly CharacterPosition End = new CharacterPosition(EndIndex);

		/// <summary>
		/// Contains the zero-based index for the position.
		/// </summary>
		public readonly int Index;

		#endregion
	}
}
