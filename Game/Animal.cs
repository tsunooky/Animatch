using Godot;
using System;

public class Animal : Node2D
{
    private PackedScene GrenadeScene = GD.Load<PackedScene>("res://Grenade.tscn");

<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
=======
>>>>>>> Stashed changes
    private int jump = 0;
    private int jumpDir = 0;

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed)
        {
            Grenade grenade = (Grenade)GrenadeScene.Instance();
            grenade.Position = Position + Vector2.Up * 12;
            Vector2 grenadeDirection = GetGlobalMousePosition() - GlobalPosition + Vector2.Up * 12;
            grenade.Init(grenadeDirection);
            GetParent().AddChild(grenade);
        }
    }

<<<<<<< Updated upstream
    public override void _PhysicsProcess(float delta)
=======
    public void _PhysicsProcess(float delta)
>>>>>>> Stashed changes
    {
        if (jump > 0)
        {
            Vector2 validPos = Position;

            for (int i = 0; i >= -5; i--)
            {
                Vector2 dir = new Vector2(jumpDir, i);
                Vector2 pos = Position + dir;

                if (GetNode<Collision>("../Map").CollisionNormal(pos) == Vector2.Zero)
                {
                    validPos = pos;
                    break;
                }
            }

            jump--;
            Position = validPos;
            return;
        }

        int walk = jumpDir;

        if (GetNode<Collision>("../Map").CollisionNormal(Position + Vector2.Down) != Vector2.Zero)
        {
            jumpDir = 0;

            if (Input.IsActionJustPressed("jump") && jump == 0)
            {
                jumpDir = (int)(Input.GetActionStrength("right") - Input.GetActionStrength("left"));
                jump = 10;
            }

            walk = (int)(Input.GetActionStrength("right") - Input.GetActionStrength("left"));
        }

        Vector2 validPosWalk = Position;

        for (int i = -3; i <= 3; i++)
        {
            Vector2 dir = new Vector2(walk, i);
            Vector2 pos = Position + dir;

            if (GetNode<Collision>("../Map").CollisionNormal(pos) == Vector2.Zero)
            {
                validPosWalk = pos;
                break;
            }
        }

        Position = validPosWalk;
    }
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
}
