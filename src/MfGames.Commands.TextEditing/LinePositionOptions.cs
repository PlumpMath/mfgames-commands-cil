// Copyright 2011-2013 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-gtkext-cil/license

using System;

namespace MfGames.Commands.TextEditing
{
	/// <summary>
	/// Contains the various options while calculating line indexes for a
	/// buffer range.
	/// </summary>
	[Flags]
	public enum LinePositionOptions
	{
		/// <summary>
		/// Indicates no special options. This will throw an OutOfRangeException
		/// if the line is beyond the limits of the buffer.
		/// </summary>
		None = 0,

		/// <summary>
		/// Indicates that no bounds checking will be done. If a line refers to
		/// outside of the range given, it will be set to the beginning or
		/// end of range as appropriate.
		/// </summary>
		NoBoundsChecking = 1,
	}
}
