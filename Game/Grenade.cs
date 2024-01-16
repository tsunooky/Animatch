using Godot;
using System;

public class Grenade : Node2D
{
    private const float GRAVITY = 0.1f;

    private Vector2 _dir;

    public void Init(Vector2 dir)
    {
        _dir = dir.Normalized() * Mathf.Max(2, dir.Length() * 0.01f);
    }

    public override void _Process(float delta)
    {
        GetNode<Label>("Label").Text = Math.Ceiling(GetNode<Timer>("Timer").TimeLeft).ToString();
    }

    public override void _PhysicsProcess(float delta)
    {
        if (GetNode<Timer>("Timer").TimeLeft <= 0)
        {
            // We're exploding. Don't move.
            return;
        }

        // Apply gravity
        _dir.y += GRAVITY / Mathf.Max(_dir.Length(), 1);

        // Do the actual movement
        // This could get coupled to the frame rate if you
        // calculate the number of steps allowed this frame based on `delta`
        DoSteps();
    }

    // See which movement is valid
    private void DoSteps()
    {
        // `velocity` is the steps we (try to) move this frame
        Vector2 velocity = _dir;

        // Let's start by doing all the movement on the y-Axis
        while (Mathf.Abs(velocity.y) > 0)
        {
            // Either move 1 pixel or the rest we want on the y-Axis (if it's < 1.0)
            Vector2 newPosition = Position + (Vector2.Down * Mathf.Sign(velocity.y) * Mathf.Min(Mathf.Abs(velocity.y), 1.0f));
            Vector2 normal = GetNode<Collision>("../Map").CollisionNormal(newPosition);

            // Get the normal of the new position
            velocity.y -= Mathf.Min(1.0f, Mathf.Abs(velocity.y)) * Mathf.Sign(velocity.y); // Update `velocity` to the remaining steps

            if (normal == Vector2.One)
            {
                // We are inside a wall; Don't move more
                break;
            }

            if (Mathf.Sign(normal.y) != 0 && Mathf.Sign(_dir.y) != Mathf.Sign(normal.y))
            {
                // We bounce on the y-Axis
                _dir.y *= -0.5f; // Use `-0.5` instead of `-1` to simulate friction
                velocity.y *= -0.5f;
            }

            if (Mathf.Sign(normal.x) != 0 && Mathf.Sign(_dir.x) != Mathf.Sign(normal.x))
            {
                // We bounce on the x-Axis
                _dir.x *= -0.8f;
                velocity.x *= -0.8f;
            }

            Position = newPosition;
        }

        // Movement on the x-Axis is the same as on the y-Axis above
        while (Mathf.Abs(velocity.x) > 0)
        {
            Vector2 newPosition = Position + (Vector2.Right * Mathf.Sign(velocity.x) * Mathf.Min(Mathf.Abs(velocity.x), 1.0f));
            Vector2 normal = GetNode<Collision>("../Map").CollisionNormal(newPosition);
            velocity.x -= Mathf.Min(1.0f, Mathf.Abs(velocity.x)) * Mathf.Sign(velocity.x);

            if (normal == Vector2.One)
            {
                break;
            }

            if (Mathf.Sign(normal.y) != 0 && Mathf.Sign(_dir.y) != Mathf.Sign(normal.y))
            {
                _dir.y *= -0.5f;
                velocity.y *= -0.5f;
            }

            if (Mathf.Sign(normal.x) != 0 && Mathf.Sign(_dir.x) != Mathf.Sign(normal.x))
            {
                _dir.x *= -0.8f;
                velocity.x *= -0.8f;
            }

            Position = newPosition;
        }
    }

    // Explode!
    private void _on_Timer_timeout()
    {
        // Tell the map to make a hole
        GetNode<Map>("../Map").Explosion(Position, 30);

        // Hide ourselves
        GetNode<Label>("Label").Visible = false;
        GetNode<Sprite>("Sprite").Visible = false;

        // Show the explosion animation
        GetNode<AnimatedSprite>("Explosion").Visible = true;
        GetNode<AnimatedSprite>("Explosion").Play("default");
    }

    // Exploded.
    private void _on_Explosion_animation_finished()
    {
        // Remove this grenade
        QueueFree();
    }
}
