// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;

namespace MfGames.Commands
{
	/// <summary>
	/// An enumeration that lists the various types of doing (Do, Undo, and Redo)
	/// in a matter that can be combined together.
	/// </summary>
	[Flags]
	public enum DoTypes
	{
		/// <summary>
		/// Indicates that none of the types should be included.
		/// </summary>
		None = 0,

		/// <summary>
		/// Indicates that the Do() operation should be included.
		/// </summary>
		Do = 1,

		/// <summary>
		/// Indicates that the Undo() operation should be included.
		/// </summary>
		Undo = 2,

		/// <summary>
		/// Indicates that the Redo() operation should be included.
		/// </summary>
		Redo = 4,

		/// <summary>
		/// Indicates that both Do() and Redo() should be included.
		/// </summary>
		DoAndRedo = Do | Redo,

		/// <summary>
		/// Indicates that all types should be included.
		/// </summary>
		All = Do | Undo | Redo,
	}
}
