using Godot;
using System;

public partial class Coin : Area2D
{
	public void _on_body_entered(Node2D area)
	{
		GameManager.addPoints(10);
		Hide();
        Dispose();
	}
}
