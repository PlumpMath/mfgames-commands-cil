// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// Represents the common functionality shared among all text editing commands
	/// including indicators for adjusting position and selection.
	/// </summary>
	/// <typeparam name="TContext">The context of the editing command.</typeparam>
	public interface ITextEditingCommand<in TContext>: IUndoableCommand<TContext>
	{
		#region Properties

		/// <summary>
		/// Indicates which operations the command should update the TextPosition
		/// property in the OperationContext.
		/// </summary>
		DoTypes UpdateTextPosition { get; set; }

		/// <summary>
		/// Indicates which operations the command should update the TextSelection
		/// property in the OperationContext.
		/// </summary>
		DoTypes UpdateTextSelection { get; set; }

		#endregion
	}
}
