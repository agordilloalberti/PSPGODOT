using Godot;
using System;

public partial class Castle : Area2D
{
    private Player player;
    
    public override void _Ready()
    {
        //player = GetTree().Root.GetNode<Player>("Player");
    }

    private void _on_body_entered(Node2D body)
    {
        GD.Print("body entered");
        //GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
    }
}
