// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System.Collections.Generic;

namespace MfGames.Commands
{
	/// <summary>
	/// A command that consists of an order list of inner commands.
	/// </summary>
	/// <typeparam name="TContext"></typeparam>
	public class CompositeCommand<TContext>: IUndoableCommand<TContext>
	{
		#region Properties

		public bool CanUndo { get; private set; }
		public List<IUndoableCommand<TContext>> Commands { get; private set; }
		public bool IsTransient { get; private set; }

		#endregion

		#region Methods

		public void Do(TContext context)
		{
			// Allow extending class to prepare for the operation.
			PreDo(context);

			// To implement the command, simply iterate through the list
			// of commands and execute each one. The state comes from the last
			// command executed.
			foreach (IUndoableCommand<TContext> command in Commands)
			{
				Do(command, context);
			}

			// Allow extending classes to complete the operation.
			PostDo(context);
		}

		public void Redo(TContext context)
		{
			// Allow extending class to prepare for the operation.
			PreRedo(context);

			// To implement the command, simply iterate through the list
			// of commands and execute each one. The state comes from the last
			// command executed.
			foreach (IUndoableCommand<TContext> command in Commands)
			{
				Redo(command, context);
			}

			// Allow extending classes to complete the operation.
			PostRedo(context);
		}

		public void Undo(TContext context)
		{
			// Allow extending class to prepare for the operation.
			PreUndo(context);

			// To implement the command, simply iterate through the list
			// of commands and execute each one. The state comes from the last
			// command executed.
			List<IUndoableCommand<TContext>> commands = Commands;

			for (int index = commands.Count - 1;
				index >= 0;
				index--)
			{
				IUndoableCommand<TContext> command = commands[index];
				Undo(command, context);
			}

			// Allow extending classes to complete the operation.
			PostUndo(context);
		}

		/// <summary>
		/// Internal method for executing a command with a specific context.
		/// </summary>
		/// <param name="command">The command to execute.</param>
		/// <param name="context">The context of the execution.</param>
		protected virtual void Do(
			IUndoableCommand<TContext> command,
			TContext context)
		{
			// Execute the command and get its state.
			command.Do(context);
		}

		protected virtual void PostDo(TContext context)
		{
		}

		protected virtual void PostRedo(TContext context)
		{
		}

		protected virtual void PostUndo(TContext context)
		{
		}

		protected virtual void PreDo(TContext context)
		{
		}

		protected virtual void PreRedo(TContext context)
		{
		}

		protected virtual void PreUndo(TContext context)
		{
		}

		/// <summary>
		/// Internal method for redoing a command with a specific context.
		/// </summary>
		/// <param name="command">The command to execute.</param>
		/// <param name="context">The context of the execution.</param>
		protected virtual void Redo(
			IUndoableCommand<TContext> command,
			TContext context)
		{
			// Execute the command and get its state.
			command.Redo(context);
		}

		/// <summary>
		/// Internal method for undoing a command with a specific context.
		/// </summary>
		/// <param name="command">The command to execute.</param>
		/// <param name="context">The context of the execution.</param>
		protected virtual void Undo(
			IUndoableCommand<TContext> command,
			TContext context)
		{
			// Execute the command and get its state.
			command.Undo(context);
		}

		#endregion

		#region Constructors

		public CompositeCommand(
			bool canUndo,
			bool isTransient)
		{
			// Save the member variables.
			CanUndo = canUndo;
			IsTransient = isTransient;

			// Initialize the collection.
			Commands = new List<IUndoableCommand<TContext>>();
		}

		#endregion
	}
}
