using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private const float Speed = 130.0f;
	private const float JumpVelocity = -300.0f;
	
	private AnimatedSprite2D player;
	private static bool LR;
	public static bool dead;

	public override void _Ready()
	{
		player = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
		
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		

		if (!dead)
		{
			if (Input.IsActionJustPressed("jump") && IsOnFloor())
			{
				velocity.Y = JumpVelocity;
			}

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
			velocity.X = direction * Speed;
			
			
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

		}
		else
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
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
