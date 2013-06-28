// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;
using MfGames.Commands.Tests;
using MfGames.Commands.TextEditing;
using NUnit.Framework;

namespace MfGames.Commands.TextEditings.Tests
{
	[TestFixture]
	public class CharacterPositionTests
	{
		#region Methods

		[Test]
		public void BeginningNormalize()
		{
			// Arrange
			const string input = "one one one one";
			var position = new CharacterPosition(0);

			// Act
			int results = position.NormalizeIndex(input);

			// Assert
			Assert.AreEqual(0, results);
		}

		[Test]
		public void BeyondBeginNormalize()
		{
			// Act
			Assert.Throws<ArgumentOutOfRangeException>(() => new CharacterPosition(-1));
		}

		[Test]
		public void BeyondEndNormalize()
		{
			// Arrange
			const string input = "one one one one";
			var position = new CharacterPosition(1000);

			// Act
			Assert.Throws<IndexOutOfRangeException>(() => position.NormalizeIndex(input));
		}

		[Test]
		public void DefaultConstructor()
		{
			// Act
			var position = new CharacterPosition();

			// Assert
			Assert.AreEqual(0, position.Index);
			Assert.AreEqual("Begin", position.GetIndexString());
			Assert.AreEqual("CharacterPosition(Begin)", position.ToString());
		}

		[Test]
		public void EndConstructor()
		{
			// Act
			CharacterPosition position = CharacterPosition.End;

			// Assert
			Assert.AreEqual(CharacterPosition.End.Index, position.Index);
			Assert.AreEqual("End", position.GetIndexString());
			Assert.AreEqual("CharacterPosition(End)", position.ToString());
		}

		[Test]
		public void EndNormalize()
		{
			// Arrange
			const string input = "one one one one";
			CharacterPosition position = CharacterPosition.End;

			// Act
			int results = position.NormalizeIndex(input);

			// Assert
			Assert.AreEqual(input.Length, results);
		}

		[Test]
		public void LeftWordNormalizeInBegin()
		{
			// Arrange
			const string input = "one one one one";
			CharacterPosition position = CharacterPosition.Word;
			CharacterPosition.DefaultWordTokenizer = new OffsetWordTokenizer();

			// Act
			Assert.Throws<IndexOutOfRangeException>(
				() => position.NormalizeIndex(input, 0, WordSearchDirection.Left));
		}

		[Test]
		public void LeftWordNormalizeInMiddle()
		{
			// Arrange
			const string input = "one one one one";
			CharacterPosition position = CharacterPosition.Word;
			CharacterPosition.DefaultWordTokenizer = new OffsetWordTokenizer();

			// Act
			int results = position.NormalizeIndex(input, 10, WordSearchDirection.Left);

			// Assert
			Assert.AreEqual(5, results);
		}

		[Test]
		public void LeftWordNormalizeInNearBegin()
		{
			// Arrange
			const string input = "one one one one";
			CharacterPosition position = CharacterPosition.Word;
			CharacterPosition.DefaultWordTokenizer = new OffsetWordTokenizer();

			// Act
			int results = position.NormalizeIndex(input, 3, WordSearchDirection.Left);

			// Assert
			Assert.AreEqual(0, results);
		}

		[Test]
		public void MiddleNormalize()
		{
			// Arrange
			const string input = "one one one one";
			var position = new CharacterPosition(10);

			// Act
			int results = position.NormalizeIndex(input);

			// Assert
			Assert.AreEqual(10, results);
		}

		[Test]
		public void NullInputNormalize()
		{
			// Arrange
			const string input = "one one one one";
			var position = new CharacterPosition(input.Length);

			// Act
			Assert.Throws<ArgumentNullException>(() => position.NormalizeIndex(null));
		}

		[Test]
		public void NumericEndNormalize()
		{
			// Arrange
			const string input = "one one one one";
			var position = new CharacterPosition(input.Length);

			// Act
			int results = position.NormalizeIndex(input);

			// Assert
			Assert.AreEqual(input.Length, results);
		}

		[Test]
		public void RightWordNormalizeInEnd()
		{
			// Arrange
			const string input = "one one one one";
			CharacterPosition position = CharacterPosition.Word;
			CharacterPosition.DefaultWordTokenizer = new OffsetWordTokenizer();

			// Act
			Assert.Throws<IndexOutOfRangeException>(
				() => position.NormalizeIndex(input, 15, WordSearchDirection.Left));
		}

		[Test]
		public void RightWordNormalizeInMiddle()
		{
			// Arrange
			const string input = "one one one one";
			CharacterPosition position = CharacterPosition.Word;
			CharacterPosition.DefaultWordTokenizer = new OffsetWordTokenizer();

			// Act
			int results = position.NormalizeIndex(input, 10, WordSearchDirection.Right);

			// Assert
			Assert.AreEqual(15, results);
		}

		[Test]
		public void RightWordNormalizeInNearEnd()
		{
			// Arrange
			const string input = "one one one one";
			CharacterPosition position = CharacterPosition.Word;
			CharacterPosition.DefaultWordTokenizer = new OffsetWordTokenizer();

			// Act
			int results = position.NormalizeIndex(input, 14, WordSearchDirection.Right);

			// Assert
			Assert.AreEqual(15, results);
		}

		[Test]
		public void TenConstructor()
		{
			// Act
			var position = new CharacterPosition(10);

			// Assert
			Assert.AreEqual(10, position.Index);
			Assert.AreEqual("10", position.GetIndexString());
			Assert.AreEqual("CharacterPosition(10)", position.ToString());
		}

		[Test]
		public void ThousandConstructor()
		{
			// Act
			var position = new CharacterPosition(1000);

			// Assert
			Assert.AreEqual(1000, position.Index);
			Assert.AreEqual("1,000", position.GetIndexString());
			Assert.AreEqual("CharacterPosition(1,000)", position.ToString());
		}

		[Test]
		public void WordConstructor()
		{
			// Act
			CharacterPosition position = CharacterPosition.Word;

			// Assert
			Assert.AreEqual(CharacterPosition.Word.Index, position.Index);
			Assert.AreEqual("Word", position.GetIndexString());
			Assert.AreEqual("CharacterPosition(Word)", position.ToString());
		}

		[Test]
		public void WordNormalizeWithoutContext()
		{
			// Arrange
			const string input = "one one one one";
			CharacterPosition position = CharacterPosition.Word;

			// Act
			Assert.Throws<InvalidOperationException>(
				() => position.NormalizeIndex(input));
		}

		[Test]
		public void ZeroConstructor()
		{
			// Act
			var position = new CharacterPosition(0);

			// Assert
			Assert.AreEqual(0, position.Index);
			Assert.AreEqual("Begin", position.GetIndexString());
			Assert.AreEqual("CharacterPosition(Begin)", position.ToString());
		}

		#endregion
	}
}
