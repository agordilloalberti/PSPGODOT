using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] 
	private float Speed = 130.0f;
	[Export] 
	private float JumpVelocity = -300.0f;
	
	private PackedScene dagger = new PackedScene();
	private AnimatedSprite2D player;
	private static bool LR=false;
	public static bool dead;

	public override void _Ready()
	{
		player = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		dagger = GD.Load<PackedScene>("res://Scenes/dagger.tscn");
	}
		
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		

		if (!dead)
		{
			shoot(LR);

			velocity = Jump(velocity);

			int direction = Move(velocity);
			
			velocity.X = direction * Speed;

			velocity = animation(velocity,delta);

		}
		else
		{
			velocity = death(velocity,delta);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	
	
	
	private Vector2 death(Vector2 velocity, double delta)
	{
		player.Play("death");
		if (player.Frame==3) 
		{
			player.Pause();
		}
		SetCollisionLayerValue(2,false);
		SetCollisionLayerValue(3,true);
		SetCollisionMaskValue(3,false);
		velocity.X = 0; 
		velocity += GetGravity() * (float)delta;
		return velocity;
	}


	private Vector2 animation(Vector2 velocity,Double delta)
	{
		if (!IsOnFloor())
		{
			player.Play("roll");
			velocity += GetGravity() * (float)delta;
		}
			
		if (velocity.X < 0)
		{
			player.FlipH = true;
			if (IsOnFloor())
			{
				player.Play("run");	
			}
			LR = true;
		}
		else if (velocity.X > 0)
		{
			player.FlipH = false;
			if (IsOnFloor())
			{
				player.Play("run");	
			}
			LR = false;
		}
		else
		{
			if (IsOnFloor())
			{
				player.Play("idle");	
			}
			player.FlipH = LR;
		}

		return velocity;
	}

	private int Move(Vector2 velocity)
	{
		int direction;
		if (Input.IsActionPressed("move_left"))
		{
			direction = -1;
		}else if (Input.IsActionPressed("move_right"))
		{
			direction = 1;
		}
		else
		{
			direction = 0;
		}
		return direction;
	}

	private Vector2 Jump(Vector2 velocity)
	{
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		return velocity;
	}

	private void shoot(bool LR)
	{
		if (Input.IsActionJustPressed("shoot"))
		{
			Dagger instDagger = dagger.Instantiate<Dagger>();
			instDagger.Position = GlobalPosition;
			instDagger.setDirection(LR);
			GetTree().Root.AddChild(instDagger);
			GD.Print("Shoot");
		}
	}
}
