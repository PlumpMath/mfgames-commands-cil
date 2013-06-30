// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// A <see cref="ITextEditingCommandController"/>-specific command for deleting
	/// text within a single line. Most other text commands are a composite of this
	/// and the other <see cref="ITextEditingCommand"/> objects.
	/// </summary>
	/// <typeparam name="TContext">The type of context object used for executing.</typeparam>
	public interface IDeleteTextCommand<in TContext>: ITextEditingCommand<TContext>
	{
	}
}
