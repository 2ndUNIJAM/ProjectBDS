using Godot;
using System;
using System.Collections.Generic;

public partial class StateController : Node
{
    State state;

    [Signal]
    public delegate void OnStateUpdateEventHandler(State state, Vector2I updatedPoint);
    public override void _Ready()
    {
        state = GetNode<State>("../State");
        Tile tile = new Tile();
        tile.East = EdgeType.Connected;
        state.Inventory.Tiles.AddLast(tile);
        TileHandler.PushInventoryGrab(state.Grab, 0);
    }
    public void GrabGridSelect(Vector2I target)
    {
        if (state.Grab.GridGrab.Count <= 0 && state.Grab.InventoryGrab < 0)
            TileHandler.PushGridGrab(state.Grab, target);
        else
            TileHandler.PlaceTile(state.Grid, state.Grab, state.Inventory, target);
        EmitSignal(SignalName.OnStateUpdate, state, target);
    }

    public void GridTileRemove(Vector2I target)
    {
        TileHandler.RemoveTile(state.Grid, state.Grab, state.Inventory);
        EmitSignal(SignalName.OnStateUpdate, state, target);
    }
}
