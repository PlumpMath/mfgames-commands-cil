// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;
using System.Diagnostics.Contracts;

namespace MfGames.Commands.TextEditing.Composites
{
	/// <summary>
	/// Represents a command that joins the current line with the previous one.
	/// It sets the cursor/caret position to the end of the previous line and adds
	/// a space between the two lines.
	/// </summary>
	/// <typeparam name="TContext"></typeparam>
	public class JoinNextParagraphCommand<TContext>: CompositeCommand<TContext>
	{
		#region Constructors

		public JoinNextParagraphCommand(
			ITextEditingCommandController<TContext> controller,
			LinePosition line)
			: base(true, false)
		{
			// Establish our code contracts.
			Contract.Requires<InvalidOperationException>(line.Index > 0);

			// Joining a paragraph consists of inserting the text of the current
			// paragraph into the previous one with a space and then moving the
			// cursor to the end of the original first paragraph (and space).

			// We start by appending the whitespace to the end of the first line.
			var joinedLine = new LinePosition(line);

			IInsertTextCommand<TContext> whitespaceCommand =
				controller.CreateInsertTextCommand(
					new TextPosition(joinedLine, CharacterPosition.End), " ");
			whitespaceCommand.UpdateTextPosition = DoTypes.All;

			// Insert the text from the line into the prvious line.
			IInsertTextFromTextRangeCommand<TContext> insertCommand =
				controller.CreateInsertTextFromTextRangeCommand(
					new TextPosition(joinedLine, CharacterPosition.End),
					new SingleLineTextRange(
						(int) line + 1, CharacterPosition.Begin, CharacterPosition.End));

			// Finally, delete the current line since we merged it.
			IDeleteLineCommand<TContext> deleteCommand =
				controller.CreateDeleteLineCommand((int) line + 1);

			// Add the commands into the composite and indicate that the whitespace
			// command controls where the text position will end up.
			Commands.Add(whitespaceCommand);
			Commands.Add(insertCommand);
			Commands.Add(deleteCommand);
		}

		#endregion
	}
}
