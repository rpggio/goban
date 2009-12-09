using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Goban.Model;

namespace Goban
{
    /// <summary>
    /// Saves/load board data using binary format.
    /// </summary>
	public class BoardStore
	{
		public void Save(Stream stream, Board board)
		{
			new BinaryFormatter().Serialize(stream, board);
		}

		public Board Load(Stream stream)
		{
			return (Board)new BinaryFormatter().Deserialize(stream);
		}
	}
}