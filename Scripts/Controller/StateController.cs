using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class StateController : Node
{
    State state;

    [Signal]
    public delegate void OnStateUpdateEventHandler(State state, Vector2I updatedPoint);

    bool bOnIntialize;
    public override void _Ready()
    {
        state = GetNode<State>("../State");
        bOnIntialize = false;
    }

    public override void _Process(double delta)
    {
        if (!bOnIntialize)
        {
            bOnIntialize = true;
            EmitSignal(SignalName.OnStateUpdate, state, Vector2I.Zero);
        }
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

	public void GrabInventorySelect(int index)
	{
        GD.Print($"{state.Inventory.Tiles.ElementAt<Tile>(index).atlas_coord}");
		TileHandler.AddInventoryGrab(state.Grab, index);
		EmitSignal(SignalName.OnStateUpdate, state, Vector2I.Zero);
	}
}
