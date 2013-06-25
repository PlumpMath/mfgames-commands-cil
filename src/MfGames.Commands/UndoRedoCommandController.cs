// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace MfGames.Commands
{
	/// <summary>
	/// Encapsulates the functionality for executing commands within a system. The
	/// controller handles undo and redo functionality, performing the actions, and
	/// keeping track of the last state of the executing command.
	/// </summary>
	/// <typeparam name="TContext">
	/// The type of state object needed to be updated with the execution of commands.
	/// </typeparam>
	public class UndoRedoCommandController<TContext>: ICommandController<TContext>
	{
		#region Properties

		/// <summary>
		/// Contains a flag if the controller has commands that can be redone.
		/// </summary>
		public bool CanRedo
		{
			get { return redoCommands.Count > 0; }
		}

		/// <summary>
		/// Contains a flag if the controller has commands that can be undone.
		/// </summary>
		public bool CanUndo
		{
			get { return undoCommands.Count > 0; }
		}

		/// <summary>
		/// Contains the maximum number of undo commands allowed on the stack. If
		/// there are more, then the oldest will be removed until the stack is
		/// within limits.
		/// </summary>
		public int MaximumUndoCommands
		{
			get { return maximumUndoCommands; }
			set
			{
				Contract.Requires<ArgumentOutOfRangeException>(value >= 0);
				maximumUndoCommands = value;
			}
		}

		/// <summary>
		/// Contains the current number of commands on the redo stack.
		/// </summary>
		public int RedoCount
		{
			get { return redoCommands.Count; }
		}

		/// <summary>
		/// Contains the current number of commands on the undo stack.
		/// </summary>
		public int UndoCount
		{
			get { return undoCommands.Count; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Schedules or requests that a command be executed after the current
		/// command is completed.
		/// </summary>
		/// <param name="command"></param>
		public void DeferDo(ICommand<TContext> command)
		{
			deferredCommands.Add(command);
		}

		/// <summary>
		/// Executes a command in the system and manages the resulting state.
		/// </summary>
		/// <param name="command">The command to execute.</param>
		/// <param name="context">The context.</param>
		public virtual void Do(
			ICommand<TContext> command,
			TContext context)
		{
			// Determine if this command is undoable or not.
			var undoableCommand = command as IUndoableCommand<TContext>;

			if (undoableCommand == null
				|| !undoableCommand.CanUndo)
			{
				// Since the command cannot be undone, we need to clear out the undo
				// buffer, which will make sure CanUndo will return false and we can
				// recover that memory.
				undoCommands.Clear();
			}

			// In all cases, we clear out the redo buffer because this is a
			// user-initiated command and redo no longer makes sense.
			redoCommands.Clear();

			// Perform the operation.
			Do(command, context, false, false, true);
		}

		/// <summary>
		/// Re-performs a command that was recently undone.
		/// </summary>
		public virtual ICommand<TContext> Redo(TContext context)
		{
			// Make sure we're in a known and valid state.
			Contract.Assert(CanRedo);

			// Pull off the first command from the redo buffer and perform it.
			ICommand<TContext> command = redoCommands[0];
			redoCommands.RemoveAt(0);
			Do(command, context, false, true, false);

			// Return the command we just redone.
			return command;
		}

		/// <summary>
		/// Undoes a command that was recently done, either through the Do() or Redo().
		/// </summary>
		public virtual ICommand<TContext> Undo(TContext context)
		{
			// Make sure we're in a known and valid state.
			Contract.Assert(CanUndo);

			// Pull off the first undo command, get its inverse operation, and
			// perform it.
			ICommand<TContext> command = undoCommands[0];
			undoCommands.RemoveAt(0);
			Do(command, context, true, true, false);

			// Return the command we just undone.
			return command;
		}

		/// <summary>
		/// Pushes the command into the undo stack while handling the additional
		/// processing for maximum undo stack size and merging commands.
		/// </summary>
		/// <param name="command">The command to add to the stack..</param>
		/// <param name="processMerges">if set to <c>true</c> then process any possible merge operations.</param>
		protected void PushUndoCommand(
			ICommand<TContext> command,
			bool processMerges)
		{
			// If we have at least one item on the undo stack already and we're
			// processing merges, see if we can combine them.
			if (processMerges && undoCommands.Count > 0)
			{
				// Grab the first item as a mergable command.
				var existingMerge = undoCommands[0] as IMergableCommand<TContext>;
				var newMerge = command as IMergableCommand<TContext>;

				if (existingMerge != null
					&& newMerge != null
					&& existingMerge.CanMergeFrom(newMerge))
				{
					// These two commands can be merged. We don't have do any other
					// check at this point.
					existingMerge.MergeFrom(newMerge);
					return;
				}
			}

			//  Check to see if we can merge the commands together.
			// Insert the command into the undo buffer.
			undoCommands.Insert(0, command);

			// If the undo buffer is too large, remove the end.
			while (undoCommands.Count > maximumUndoCommands)
			{
				undoCommands.RemoveAt(undoCommands.Count - 1);
			}
		}

		/// <summary>
		/// The internal "do" method is what performs the actual work on the project.
		/// It also handles pushing the appropriate command on the correct undo/redo
		/// list.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="context">The context for the execution.</param>
		/// <param name="useUndo">if set to <c>true</c> [use inverse].</param>
		/// <param name="ignoreDeferredCommands">if set to <c>true</c>, then don't process any deferred operations and clear the deferred list.</param>
		/// <param name="processMerges">if set to <c>true</c>, then process the command for merge operations.</param>
		private void Do(
			ICommand<TContext> command,
			TContext context,
			bool useUndo,
			bool ignoreDeferredCommands,
			bool processMerges)
		{
			// Perform the action based on undo or redo. We always do this even if
			// we're going to be merging later.
			var undoableCommand = command as IUndoableCommand<TContext>;

			if (undoableCommand == null
				|| !useUndo)
			{
				command.Do(context);
			}
			else
			{
				undoableCommand.Undo(context);
			}

			// Add the action to the appropriate buffer. This assumes that the undo
			// and redo operations have been properly managed before this method is
			// called. This does not manage the buffers since the undo/redo allows
			// the user to go back and forth between the two lists.
			if (undoableCommand != null
				&& undoableCommand.CanUndo
				&& !undoableCommand.IsTransient)
			{
				if (useUndo)
				{
					redoCommands.Insert(0, command);
				}
				else
				{
					PushUndoCommand(command, processMerges);
				}
			}

			// If we are ignoring deferred commands, we clear them to ensure they
			// aren't run in the next execution.
			if (ignoreDeferredCommands)
			{
				deferredCommands.Clear();
			}

			// If we have deferred commands, then execute them in order.
			if (deferredCommands.Count > 0)
			{
				// Copy the list and clear it out in case any of these actions
				// add any more deferred commands.
				var commands = new List<ICommand<TContext>>(deferredCommands);

				deferredCommands.Clear();

				// Go through the commands and process each one.
				foreach (ICommand<TContext> deferredCommand in commands)
				{
					Do(deferredCommand, context);
				}
			}
		}

		#endregion

		#region Constructors

		public UndoRedoCommandController()
		{
			// Initialize the collections.
			undoCommands = new List<ICommand<TContext>>();
			redoCommands = new List<ICommand<TContext>>();
			deferredCommands = new List<ICommand<TContext>>();

			// Set the default maximum commands to (effectively) unlimited.
			maximumUndoCommands = Int32.MaxValue;
		}

		#endregion

		#region Fields

		private readonly List<ICommand<TContext>> deferredCommands;
		private int maximumUndoCommands;
		private readonly List<ICommand<TContext>> redoCommands;
		private readonly List<ICommand<TContext>> undoCommands;

		#endregion
	}
}
