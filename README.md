# goban
Go game example implementation

This game was created to demonstrate object-oriented design to a friend of mine who was new to programming and was a fan of Go. There is no deep Go strategy included in the game so far. The computer-controlled black player attempts to surround the group that is closest to being surrounded, or to defend its own group in the same situation. The game recognizes groups of surrounded pieces and removes them from the board. You can try surrounding some black pieces to see it at work.

![board](/board.jpg?raw=true "board")

*Design*
The interface IGroup is used to represent groups of stones.

```
/// <summary>
/// Represents a logical group of positions. Provides operations for making calculations
/// based on contained and adjacent positions.
/// </summary>
public interface IGroup
{
	bool Contains(Position position);
	bool CanSurround { get; }
	Stone Stone { get; }
	bool Bounds(Position pos);
	void Accept(IPositionVisitor visitor);
	Set<Position> GetNeighbors();
	Set<Position> Union(IEnumerable<Position> positions);
}
```
The same interface is also used to represent the boundary of the board and empty board spaces, which makes the bounding calculations simple:

```
/// <summary>
/// Determine if the group is currently enclosed.
/// </summary>
protected bool IsEnclosed(IGroup group)
{
	foreach(Position p in group.GetNeighbors())
	{
		// Stones and borders can surround a group; empty spaces cannot.
		if (!FindGroup(p).CanSurround) return false;
	}
	return true;
}
```
The implementation of the computer-controlled player is surprisingly simple (~15 lines).
