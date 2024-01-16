using Godot;
using System;

public class MapCollision : Node
{
    private const int WIDTH = 1024;
    private const int HEIGHT = 600;
    private bool[,] collision;

    public void InitMap(Array line)
    {
        collision = new bool[WIDTH, HEIGHT];

        for (int x = 0; x < WIDTH; x++)
        {
            collision.Append(new bool[HEIGHT]);

            for (int y = 0; y < HEIGHT; y++)
            {
                if ((float)line[x] > y)
                {
                    collision[x, y] = false;
                }
                else
                {
                    collision[x, y] = true;
                }
            }
        }
    }

    public Vector2 CollisionNormal(Vector2 pos)
    {
        if (pos.x <= 0)
        {
            return Vector2.Right;
        }

        if (pos.x >= WIDTH)
        {
            return Vector2.Left;
        }

        if (pos.y <= 0)
        {
            return Vector2.Down;
        }

        if (pos.y >= HEIGHT)
        {
            return Vector2.Up;
        }

        if (collision[(int)pos.x, (int)pos.y])
        {
            return new Vector2(Mathf.Randf(), Mathf.Randf()).Normalized();
        }

        Vector2 normal = Vector2.Zero;

        foreach (Vector2 direction in new Vector2[] { Vector2.Up, Vector2.Down, Vector2.Left, Vector2.Right })
        {
            Vector2 observedPixel = pos + direction;

            if (observedPixel.x < 0 || observedPixel.x >= WIDTH ||
                observedPixel.y < 0 || observedPixel.y >= HEIGHT ||
                collision[(int)observedPixel.x, (int)observedPixel.y])
            {
                normal += direction * -1;
            }
        }

        return normal.Normalized();
    }

    public void Explosion(Vector2 pos, int radius)
    {
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if (new Vector2(x, y).Length() > radius)
                {
                    continue;
                }

                Vector2 pixel = pos + new Vector2(x, y);

                if (pixel.x < 0 || pixel.x >= WIDTH || pixel.y < 0 || pixel.y >= HEIGHT)
                {
                    continue;
                }

                collision[(int)pixel.x, (int)pixel.y] = false;
            }
        }
    }
}
