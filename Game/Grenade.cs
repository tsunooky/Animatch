using Godot;
using System;

public class Grenade : Node2D
{
<<<<<<< Updated upstream
    private const float GRAVITY = 0.1f;

=======
<<<<<<< Updated upstream
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
=======
    private const float GRAVITY = 0.1f;
>>>>>>> Stashed changes
    private Vector2 _dir;

    public void Init(Vector2 dir)
    {
        _dir = dir.Normalized() * Mathf.Max(2, dir.Length() * 0.01f);
    }

<<<<<<< Updated upstream
    public override void _Process(float delta)
=======
    public void _Process(float delta)
>>>>>>> Stashed changes
    {
        GetNode<Label>("Label").Text = Math.Ceiling(GetNode<Timer>("Timer").TimeLeft).ToString();
    }

<<<<<<< Updated upstream
    public override void _PhysicsProcess(float delta)
=======
    public void _PhysicsProcess(float delta)
>>>>>>> Stashed changes
    {
        if (GetNode<Timer>("Timer").TimeLeft <= 0)
        {
            // We're exploding. Don't move.
            return;
        }

        // Apply gravity
<<<<<<< Updated upstream
        _dir.y += GRAVITY / Mathf.Max(_dir.Length(), 1);
=======
        _dir.Y += GRAVITY / Mathf.Max(_dir.Length(), 1);
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
        // Let's start by doing all the movement on the y-Axis
        while (Mathf.Abs(velocity.y) > 0)
        {
            // Either move 1 pixel or the rest we want on the y-Axis (if it's < 1.0)
            Vector2 newPosition = Position + (Vector2.Down * Mathf.Sign(velocity.y) * Mathf.Min(Mathf.Abs(velocity.y), 1.0f));
            Vector2 normal = GetNode<Collision>("../Map").CollisionNormal(newPosition);

            // Get the normal of the new position
            velocity.y -= Mathf.Min(1.0f, Mathf.Abs(velocity.y)) * Mathf.Sign(velocity.y); // Update `velocity` to the remaining steps
=======
        // Let's start by doing all the movement on the Y-Axis
        while (Mathf.Abs(velocity.Y) > 0)
        {
            // Either move 1 pixel or the rest we want on the Y-Axis (if it's < 1.0)
            Vector2 newPosition = Position + (Vector2.Down * Mathf.Sign(velocity.Y) * Mathf.Min(Mathf.Abs(velocity.Y), 1.0f));
            Vector2 normal = GetNode<Collision>("../Map/Collision").CollisionNormal(newPosition);

            // Get the normal of the new position
            velocity.Y -= Mathf.Min(1.0f, Mathf.Abs(velocity.Y)) * Mathf.Sign(velocity.Y); // Update `velocity` to the remaining steps
>>>>>>> Stashed changes

            if (normal == Vector2.One)
            {
                // We are inside a wall; Don't move more
                break;
            }

<<<<<<< Updated upstream
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
=======
            if (Mathf.Sign(normal.Y) != 0 && Mathf.Sign(_dir.Y) != Mathf.Sign(normal.Y))
            {
                // We bounce on the Y-Axis
                _dir.Y *= -0.5f; // Use `-0.5` instead of `-1` to simulate friction
                velocity.Y *= -0.5f;
            }

            if (Mathf.Sign(normal.X) != 0 && Mathf.Sign(_dir.X) != Mathf.Sign(normal.X))
            {
                // We bounce on the X-Axis
                _dir.X *= -0.8f;
                velocity.X *= -0.8f;
>>>>>>> Stashed changes
            }

            Position = newPosition;
        }

<<<<<<< Updated upstream
        // Movement on the x-Axis is the same as on the y-Axis above
        while (Mathf.Abs(velocity.x) > 0)
        {
            Vector2 newPosition = Position + (Vector2.Right * Mathf.Sign(velocity.x) * Mathf.Min(Mathf.Abs(velocity.x), 1.0f));
            Vector2 normal = GetNode<Collision>("../Map").CollisionNormal(newPosition);
            velocity.x -= Mathf.Min(1.0f, Mathf.Abs(velocity.x)) * Mathf.Sign(velocity.x);
=======
        // Movement on the X-Axis is the same as on the Y-Axis above
        while (Mathf.Abs(velocity.X) > 0)
        {
            Vector2 newPosition = Position + (Vector2.Right * Mathf.Sign(velocity.X) * Mathf.Min(Mathf.Abs(velocity.X), 1.0f));
            Vector2 normal = GetNode<Collision>("../Map/Collision").CollisionNormal(newPosition);
            velocity.X -= Mathf.Min(1.0f, Mathf.Abs(velocity.X)) * Mathf.Sign(velocity.X);
>>>>>>> Stashed changes

            if (normal == Vector2.One)
            {
                break;
            }

<<<<<<< Updated upstream
            if (Mathf.Sign(normal.y) != 0 && Mathf.Sign(_dir.y) != Mathf.Sign(normal.y))
            {
                _dir.y *= -0.5f;
                velocity.y *= -0.5f;
            }

            if (Mathf.Sign(normal.x) != 0 && Mathf.Sign(_dir.x) != Mathf.Sign(normal.x))
            {
                _dir.x *= -0.8f;
                velocity.x *= -0.8f;
=======
            if (Mathf.Sign(normal.Y) != 0 && Mathf.Sign(_dir.Y) != Mathf.Sign(normal.Y))
            {
                _dir.Y *= -0.5f;
                velocity.Y *= -0.5f;
            }

            if (Mathf.Sign(normal.X) != 0 && Mathf.Sign(_dir.X) != Mathf.Sign(normal.X))
            {
                _dir.X *= -0.8f;
                velocity.X *= -0.8f;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        GetNode<Sprite>("Sprite").Visible = false;

        // Show the explosion animation
        GetNode<AnimatedSprite>("Explosion").Visible = true;
        GetNode<AnimatedSprite>("Explosion").Play("default");
=======
>>>>>>> Stashed changes
    }

    // Exploded.
    private void _on_Explosion_animation_finished()
    {
        // Remove this grenade
        QueueFree();
    }
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
}
