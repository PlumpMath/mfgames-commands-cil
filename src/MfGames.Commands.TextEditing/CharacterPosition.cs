// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;
using MfGames.Languages;

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// A structure for representing a zero-based index within a character string.
	/// </summary>
	public struct CharacterPosition
	{
		#region Properties

		/// <summary>
		/// Contains the default word tokenizer used for identifying word boundaries
		/// when using CharacterPosition.Word.
		/// </summary>
		public static IWordTokenizer DefaultWordTokenizer { get; set; }

		#endregion

		#region Methods

		public bool Equals(CharacterPosition other)
		{
			return Index == other.Index;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			return obj is CharacterPosition && Equals((CharacterPosition) obj);
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
				case WordIndex:
					value = "Word";
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
		/// Translates the magic values (End, Beginning, Word) into actual values
		/// based on the given count.
		/// </summary>
		/// <param name="text">The text used for normalizing.</param>
		/// <param name="searchPosition">The character position to start searching for words.</param>
		/// <param name="direction">The direction to search for Word positions.</param>
		/// <returns>
		/// The normalized index.
		/// </returns>
		/// <exception cref="System.IndexOutOfRangeException">Encountered an invalid index:  + Index</exception>
		public int NormalizeIndex(
			string text,
			CharacterPosition searchPosition,
			WordSearchDirection direction)
		{
			return NormalizeIndex(text, searchPosition, direction, DefaultWordTokenizer);
		}

		/// <summary>
		/// Translates the magic values (End, Beginning, Word) into actual values
		/// based on the given count.
		/// </summary>
		/// <param name="text">The text used for normalizing.</param>
		/// <param name="searchPosition">The character position to start searching for words.</param>
		/// <param name="direction">The direction to search for Word positions.</param>
		/// <param name="wordTokenizer">The word tokenizer to use for word positions.</param>
		/// <returns>
		/// The normalized index.
		/// </returns>
		/// <exception cref="System.IndexOutOfRangeException">Encountered an invalid index:  + Index</exception>
		public int NormalizeIndex(
			string text,
			CharacterPosition searchPosition,
			WordSearchDirection direction,
			IWordTokenizer wordTokenizer)
		{
			// Make sure we have a sane state before we start.
			if (wordTokenizer == null)
			{
				throw new ArgumentNullException("wordTokenizer");
			}

			// All the magic values are negative, so if we don't have one, there is
			// nothing to do.
			if (Index >= 0)
			{
				return Index;
			}

			// If we have the end magic number, then the index is equal to the end
			// of the text line.
			if (Index == EndIndex)
			{
				return text.Length;
			}

			// If we have the word, then we go either left or right.
			if (Index == WordIndex)
			{
				// Perform some additional checks on the range and index.
				if (searchPosition.Index == 0
					&& direction == WordSearchDirection.Left)
				{
					throw new IndexOutOfRangeException(
						"Cannot find Word position at beginning of string.");
				}

				if (searchPosition.Index == text.Length
					&& direction == WordSearchDirection.Right)
				{
					throw new IndexOutOfRangeException(
						"Cannot find Word position at end of string.");
				}

				// Depending on the direction, we use the word tokenizer in
				// the appropriate direction.
				if (direction == WordSearchDirection.Right)
				{
					int index = wordTokenizer.GetNextWordBoundary(text, searchPosition.Index);
					return index;
				}
				else
				{
					int index = wordTokenizer.GetPreviousWordBoundary(
						text, searchPosition.Index);
					return index;
				}
			}

			// If we got this far, we don't know how to process this.
			throw new IndexOutOfRangeException("Encountered an invalid index: " + Index);
		}

		/// <summary>
		/// Translates the magic values (End, Beginning) into actual values
		/// based on the given count.
		/// </summary>
		/// <param name="text">The text used for normalizing.</param>
		/// <returns>
		/// The normalized index.
		/// </returns>
		/// <exception cref="System.IndexOutOfRangeException">Encountered an invalid index:  + Index</exception>
		public int NormalizeIndex(string text)
		{
			// Establish our code contracts.
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (Index == WordIndex)
			{
				throw new InvalidOperationException(
					"Cannot normalize index with a Word position without as string.");
			}

			// All the magic values are negative, so if we don't have one, there is
			// nothing to do.
			if (Index >= 0)
			{
				// If we are beyond the string, throw an exception.
				if (Index > text.Length)
				{
					throw new IndexOutOfRangeException(
						string.Format(
							"Character position {0} is beyond input string length {1}.",
							Index,
							text.Length));
				}

				return Index;
			}

			// If we have the end magic number, then the index is equal to the end
			// of the text line.
			if (Index == EndIndex)
			{
				return text.Length;
			}

			// If we got this far, we don't know how to process this.
			throw new IndexOutOfRangeException("Encountered an invalid index: " + Index);
		}

		public override string ToString()
		{
			string value = GetIndexString();
			return string.Format("CharacterPosition({0})", value);
		}

		#endregion

		#region Operators

		public static bool operator ==(CharacterPosition left,
			CharacterPosition right)
		{
			return left.Equals(right);
		}

		public static explicit operator int(CharacterPosition characterPosition)
		{
			int index = characterPosition.Index;
			return index;
		}

		public static bool operator >(CharacterPosition left,
			CharacterPosition right)
		{
			return left.Index > right.Index;
		}

		public static bool operator >=(CharacterPosition left,
			CharacterPosition right)
		{
			return left.Index >= right.Index;
		}

		public static implicit operator CharacterPosition(int index)
		{
			var position = new CharacterPosition(index);
			return position;
		}

		public static bool operator !=(CharacterPosition left,
			CharacterPosition right)
		{
			return !left.Equals(right);
		}

		public static bool operator <(CharacterPosition left,
			CharacterPosition right)
		{
			return left.Index < right.Index;
		}

		public static bool operator <=(CharacterPosition left,
			CharacterPosition right)
		{
			return left.Index <= right.Index;
		}

		#endregion

		#region Constructors

		static CharacterPosition()
		{
			DefaultWordTokenizer = new WordTokenizer();
		}

		public CharacterPosition(int index)
		{
			// Establish our contracts.
			if (index != WordIndex
				&& index != EndIndex
				&& index < 0)
			{
				throw new ArgumentOutOfRangeException(
					"index", "Cannot use a negative index that isn't End or Word.");
			}

			// Save the index for later.
			Index = index;
		}

		public CharacterPosition(CharacterPosition characterPosition)
			: this(characterPosition.Index)
		{
		}

		#endregion

		#region Fields

		private const int BeginIndex = 0;
		private const int EndIndex = -113;
		private const int WordIndex = -1053;

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

		/// <summary>
		/// A magic number that represents a word break for the line.
		/// </summary>
		public static readonly CharacterPosition Word =
			new CharacterPosition(WordIndex);

		#endregion
	}
}
