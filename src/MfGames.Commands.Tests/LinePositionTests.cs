// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;
using MfGames.Commands.TextEditing;
using NUnit.Framework;

namespace MfGames.Commands.TextEditings.Tests
{
	[TestFixture]
	public class LinePositionTests
	{
		#region Methods

		[Test]
		public void BeginningNormalize()
		{
			// Arrange
			var position = new LinePosition(0);

			// Act
			int results = position.NormalizeIndex(15);

			// Assert
			Assert.AreEqual(0, results);
		}

		[Test]
		public void BeyondBeginNormalize()
		{
			// Act
			Assert.Throws<ArgumentOutOfRangeException>(() => new LinePosition(-1));
		}

		[Test]
		public void BeyondEndNormalize()
		{
			// Arrange
			var position = new LinePosition(1000);

			// Act
			Assert.Throws<IndexOutOfRangeException>(() => position.NormalizeIndex(10));
		}

		[Test]
		public void DefaultConstructor()
		{
			// Act
			var position = new LinePosition();

			// Assert
			Assert.AreEqual(0, position.Index);
			Assert.AreEqual("Begin", position.GetIndexString());
			Assert.AreEqual("LinePosition(Begin)", position.ToString());
		}

		[Test]
		public void EndConstructor()
		{
			// Act
			LinePosition position = LinePosition.End;

			// Assert
			Assert.AreEqual(LinePosition.End.Index, position.Index);
			Assert.AreEqual("End", position.GetIndexString());
			Assert.AreEqual("LinePosition(End)", position.ToString());
		}

		[Test]
		public void EndNormalize()
		{
			// Arrange
			const string input = "one one one one";
			LinePosition position = LinePosition.End;

			// Act
			int results = position.NormalizeIndex(15);

			// Assert
			Assert.AreEqual(input.Length, results);
		}

		[Test]
		public void MiddleNormalize()
		{
			// Arrange
			var position = new LinePosition(10);

			// Act
			int results = position.NormalizeIndex(15);

			// Assert
			Assert.AreEqual(10, results);
		}

		[Test]
		public void NumericEndNormalize()
		{
			// Arrange
			const string input = "one one one one";
			var position = new LinePosition(input.Length);

			// Act
			int results = position.NormalizeIndex(15);

			// Assert
			Assert.AreEqual(input.Length, results);
		}

		[Test]
		public void TenConstructor()
		{
			// Act
			var position = new LinePosition(10);

			// Assert
			Assert.AreEqual(10, position.Index);
			Assert.AreEqual("10", position.GetIndexString());
			Assert.AreEqual("LinePosition(10)", position.ToString());
		}

		[Test]
		public void ThousandConstructor()
		{
			// Act
			var position = new LinePosition(1000);

			// Assert
			Assert.AreEqual(1000, position.Index);
			Assert.AreEqual("1,000", position.GetIndexString());
			Assert.AreEqual("LinePosition(1,000)", position.ToString());
		}

		[Test]
		public void ZeroConstructor()
		{
			// Act
			var position = new LinePosition(0);

			// Assert
			Assert.AreEqual(0, position.Index);
			Assert.AreEqual("Begin", position.GetIndexString());
			Assert.AreEqual("LinePosition(Begin)", position.ToString());
		}

		#endregion
	}
}
