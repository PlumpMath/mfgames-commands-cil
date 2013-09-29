// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;

namespace MfGames.Commands.TextEditing.Composites
{
	/// <summary>
	/// Represents a command that joins the current line with the previous one.
	/// It sets the cursor/caret position to the end of the previous line and adds
	/// a space between the two lines.
	/// </summary>
	/// <typeparam name="TContext"></typeparam>
	public class JoinPreviousParagraphCommand<TContext>: CompositeCommand<TContext>
	{
		#region Constructors

		public JoinPreviousParagraphCommand(
			ITextEditingCommandController<TContext> controller,
			LinePosition line)
			: base(true, false)
		{
			// Establish our code contracts.
			if (line.Index <= 0)
			{
				throw new InvalidOperationException(
					"Cannot join the paragraph on the first line.");
			}

			// Joining a paragraph consists of inserting the text of the current
			// paragraph into the previous one with a space and then moving the
			// cursor to the end of the original first paragraph (and space).

			// Insert the text from the line into the prvious line.
			var joinedLine = new LinePosition((int) line - 1);

			IInsertTextFromTextRangeCommand<TContext> insertCommand =
				controller.CreateInsertTextFromTextRangeCommand(
					new TextPosition(joinedLine, CharacterPosition.End),
					new SingleLineTextRange(
						line, CharacterPosition.Begin, CharacterPosition.End));
			insertCommand.UpdateTextPosition = DoTypes.All;

			// Finally, delete the current line since we merged it.
			IDeleteLineCommand<TContext> deleteCommand =
				controller.CreateDeleteLineCommand(line);
			deleteCommand.UpdateTextPosition = DoTypes.None;

			// Add the commands into the composite and indicate that the whitespace
			// command controls where the text position will end up.
			Commands.Add(insertCommand);
			Commands.Add(deleteCommand);
		}

		#endregion
	}
}
