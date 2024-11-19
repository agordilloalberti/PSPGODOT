using Godot;
using System;

public partial class Dagger : Area2D
{
    [Export]
    private float speed = 150f;
    private Sprite2D sprite; 
    private int direction = 1;

    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite");
    }
    

    public override void _Process(double delta)
    {
        Vector2 position = Position;
        position.X += speed*direction*(float)delta;
        Position = position;
    }
    
    
    public void setDirection(bool LR)
    {
        sprite.FlipH = LR;
        if (LR)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }
}
