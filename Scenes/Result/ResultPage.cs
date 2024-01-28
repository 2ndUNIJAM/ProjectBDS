using Godot;
using System;
using System.Diagnostics;

public partial class ResultPage: Control
{
    [Export]
    float AtlasScale = 1280;

    [Export]
    Texture2D[] Stamps = new Texture2D[4];

    

    public override void _Ready()
    {
        SetResult(3);

        Button submitButton = GetNode<Button>("../HUD/Submit");
        submitButton.Text = "SUBMIT";
        submitButton.Pressed += GameOver;
        //AddChild(submitButton);
    }

    void GameOver()
    {
        GD.Print("over here");
        Control myInventory = GetNode<Control>("../HUD/Inventory");
        myInventory.Visible = false;
        Control myResult = GetNode<Control>("../HUD/Result");
        myResult.Visible = true;

        SetResult(0);
    }

    public void SetResult(int grade)
    {
        TextureRect mayorTexture = GetNode<TextureRect>("%Mayor");
        AtlasTexture mayorAtlasTexture = mayorTexture.Texture as AtlasTexture;
        Debug.Assert(mayorAtlasTexture != null);

        mayorAtlasTexture.Region = new Rect2(AtlasScale * grade, 0, AtlasScale, AtlasScale);

        TextureRect stamp = GetNode<TextureRect>("%Stamp");
        stamp.Texture = Stamps[grade];
    }
}
