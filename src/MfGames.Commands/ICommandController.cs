// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

namespace MfGames.Commands
{
	/// <summary>
	/// Describes the interface for a command controller, the system for executing
	/// commands.
	/// </summary>
	/// <typeparam name="TContext">
	/// The type of object that would represent the state of the system after executing a command.
	/// </typeparam>
	public interface ICommandController<TContext>
	{
		#region Properties

		/// <summary>
		/// Contains a flag if the controller has commands that can be redone.
		/// </summary>
		bool CanRedo { get; }

		/// <summary>
		/// Contains a flag if the controller has commands that can be undone.
		/// </summary>
		bool CanUndo { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Schedules or requests that a command be executed after the current
		/// command is completed.
		/// </summary>
		/// <param name="command">The command to execute after the execution of
		/// the current command.</param>
		void DeferDo(ICommand<TContext> command);

		/// <summary>
		/// Executes a command in the system and manages the resulting context.
		/// </summary>
		/// <param name="command">The command to execute.</param>
		/// <param name="context">The context for the execution.</param>
		void Do(
			ICommand<TContext> command,
			TContext context);

		/// <summary>
		/// Re-performs a command that was recently undone.
		/// </summary>
		ICommand<TContext> Redo(TContext state);

		/// <summary>
		/// Undoes a command that was recently done, either through the Do() or Redo().
		/// </summary>
		ICommand<TContext> Undo(TContext state);

		#endregion
	}
}
