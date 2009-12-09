using System;

namespace Goban.Model
{
    /// <summary>
    /// A color of stone
    /// </summary>
	[Serializable]
	public enum Stone
	{
        /// <summary>
        /// No stone - represents an unoccupied position.
        /// </summary>
		None,

		Black,

		White 
	}
}