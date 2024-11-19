using Godot;
using System;

public partial class Killzone : Area2D
{
    private Timer timer;
    
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.OneShot = true;
        timer.Autostart = true;
    }

    public void _on_body_entered(Node2D area)
    {
        Player.dead = true;
        Engine.TimeScale = 0.5;
        timer.Start(1.5);
    }

    public void _on_timer_timeout()
    {
        GetTree().ReloadCurrentScene();
        GameManager.reset();
        Engine.TimeScale = 1;
        Player.dead = false;
    }
}
