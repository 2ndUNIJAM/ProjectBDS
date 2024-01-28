using Godot;
using System;

public partial class InfoButton : TextureButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (IsHovered())
		{
			showTutorialImage();

        }
		else
		{
			hideTutorialImage();

        }
	}


	void showTutorialImage()
	{
		Sprite2D myTutorialImg = GetNode<Sprite2D>("../Tutorialimg");
		myTutorialImg.Visible = true;
	}
    void hideTutorialImage()
    {
        Sprite2D myTutorialImg = GetNode<Sprite2D>("../Tutorialimg");
        myTutorialImg.Visible = false;
    }
}
