namespace Animatch.Game;

using Godot;
using System;

public class Map : Node2D
{
    private TextureRect fg;
    private TextureRect bg;

    private Color TRANSPARENT = new Color(0, 0, 0, 0);

    private float[] line;

    public override void _Ready()
    {
        randomize(); // Generate a new random seed
        _GenerateMap(); // Generate a "map"
        GetNode<Collision>("Collision").InitMap(line); // Initialize the "map" for collision
    }

    public Vector2 CollisionNormal(Vector2 pos)
    {
        return GetNode<Collision>("Collision").CollisionNormal(pos); // Collision implements this
    }

    private void _GenerateMap()
    {
        fg = GetNode<TextureRect>("FG");
        bg = GetNode<TextureRect>("BG");

        Image fgData = fg.Texture.GetData();
        fgData.Lock();
        Image bgData = bg.Texture.GetData();
        bgData.Lock();

        OpenSimplexNoise noise = new OpenSimplexNoise();
        noise.Seed = (int)GD.RandRange(0, 1000000);
        noise.Octaves = 2;
        noise.Period = 180.0f;
        noise.Persistence = 0.8f;

        for (int x = 0; x < fgData.GetWidth(); x++)
        {
            float high = ((float)(noise.GetNoise1d(x) + 1) * fgData.GetHeight() * 0.4f) + fgData.GetHeight() * 0.08f;
            line[x] = high;

            for (int y = 0; y < high; y++)
            {
                fgData.SetPixelv(new Vector2(x, y), TRANSPARENT);
                bgData.SetPixelv(new Vector2(x, y), TRANSPARENT);
            }
        }

        fgData.Unlock();
        fg.Texture.SetData(fgData);
        bgData.Unlock();
        bg.Texture.SetData(bgData);
    }

    public void Explosion(Vector2 pos, int radius)
    {
        GetNode<Collision>("Collision").Explosion(pos, radius);

        Image fgData = fg.Texture.GetData();
        fgData.Lock();

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if (new Vector2(x, y).Length() > radius)
                {
                    continue;
                }

                Vector2 pixel = pos + new Vector2(x, y);

                if (pixel.x < 0 || pixel.x >= fgData.GetWidth() || pixel.y < 0 || pixel.y >= fgData.GetHeight())
                {
                    continue;
                }

                fgData.SetPixelv(pixel, TRANSPARENT);
            }
        }

        radius -= 10;

        Image bgData = bg.Texture.GetData();
        bgData.Lock();

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if (new Vector2(x, y).Length() > radius)
                {
                    continue;
                }

                Vector2 pixel = pos + new Vector2(x, y);

                if (pixel.x < 0 || pixel.x >= bgData.GetWidth() || pixel.y < 0 || pixel.y >= bgData.GetHeight())
                {
                    continue;
                }

                bgData.SetPixelv(pixel, TRANSPARENT);
            }
        }

        fgData.Unlock();
        fg.Texture.SetData(fgData);
        bgData.Unlock();
        bg.Texture.SetData(bgData);
    }
}
