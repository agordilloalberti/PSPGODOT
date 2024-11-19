using Godot;
using System;

public partial class GameManager : Node
{
    private static int points=0;
    private static Label scoreLabel;

    public override void _Ready()
    {
        scoreLabel = GetNode<Label>("/root/Game/Labels/ScoreLabel");
    }

    public static void addPoints(int i)
    {
        points += i;
        scoreLabel.Text = "Has obtenido\n" +points+" puntos";
    }

    public static void reset()
    {
        points = 0;
    }
}
