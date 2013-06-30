// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

namespace MfGames.Commands
{
	/// <summary>
	/// Represents a command that can be merged with another command. This is used
	/// for commands that can be bundled (such as grouping multiple character presses
	/// into a single command).
	/// </summary>
	/// <typeparam name="TContext"></typeparam>
	public interface IMergableCommand<TContext>: IUndoableCommand<TContext>
	{
		#region Methods

		/// <summary>
		/// Determines whether this instance can have the given command merged into it.
		/// </summary>
		/// <param name="command">The command to test.</param>
		/// <returns>
		///   <c>true</c> if this instance can be merged into the specified command; otherwise, <c>false</c>.
		/// </returns>
		bool CanMergeFrom(IMergableCommand<TContext> command);

		/// <summary>
		/// Merges informaton from the specified command into the current command.
		/// </summary>
		/// <param name="command">The command to merge from.</param>
		void MergeFrom(IMergableCommand<TContext> command);

		#endregion
	}
}
