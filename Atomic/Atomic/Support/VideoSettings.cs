using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Atomic
{

    /*
     * The VideoSettings class manages settings such as the current/available resolutions, fullscreen, 
     * and other quality/performance settings.
     * It also provides a few functions to load/save video setting profiles for multiple users.
     */

    static class VideoSettings
    {
        public static readonly string DEFAULT_SETTINGS_FNAME = "VSET.conf";
        public static readonly int DEFAULT_BB_WIDTH = 1920;
        public static readonly int DEFAULT_BB_HEIGHT = 1080;
        public static readonly bool DEFAULT_FULLSCREEN = true;

        public static Point resolution;
        public static bool fullScreen;

        public static List<int[]> resolutions = new List<int[]>();

        static GraphicsDeviceManager graphics;

        public static void init(GraphicsDeviceManager graphics) { init(graphics, DEFAULT_SETTINGS_FNAME); }
        public static void init(GraphicsDeviceManager graphics, string fname)
        {
            AddResolution(640, 480);
            AddResolution(800, 600);
            AddResolution(1024, 768);
            AddResolution(1366, 768);
            AddResolution(1280, 800);
            AddResolution(1280, 1024);
            AddResolution(1600, 900);
            AddResolution(1920, 1080);
            AddResolution(1920, 1200);            


            resolution = new Point();            

            VideoSettings.graphics = graphics;
            LoadVideoSettings(fname);
            ApplyChanges();
        }

        static void AddResolution(int x, int y)
        {
            resolutions.Add(new int[2] { x, y });
        }

        public static void ApplyChanges()
        {
            graphics.PreferredBackBufferWidth = resolution.X;
            graphics.PreferredBackBufferHeight = resolution.Y;
            graphics.IsFullScreen = fullScreen;
            graphics.ApplyChanges();
        }

        public static void SaveVideoSettings()
        {
            SaveVideoSettings(DEFAULT_SETTINGS_FNAME);
        }
        public static void SaveVideoSettings(string filename)
        {
            List<string> lines = new List<string>();
            lines.Add(resolution.X.ToString());
            lines.Add(resolution.Y.ToString());
            lines.Add(fullScreen.ToString());
            System.IO.File.WriteAllLines(filename, lines.ToArray());
        }

        public static void LoadDefaultSettings()
        {            
            resolution.X = DEFAULT_BB_WIDTH;
            resolution.Y = DEFAULT_BB_HEIGHT;
            fullScreen = DEFAULT_FULLSCREEN;
        }

        public static void LoadVideoSettings()
        {
            LoadVideoSettings(DEFAULT_SETTINGS_FNAME);
        }
        public static void LoadVideoSettings(string filename)
        {
            if (!System.IO.File.Exists(filename))
            {
                LoadDefaultSettings();
            }
            else
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(filename);
                    resolution.X = int.Parse(lines[0]);
                    resolution.Y = int.Parse(lines[1]);
                    fullScreen = bool.Parse(lines[2]);
                }
                catch
                {
                    LoadDefaultSettings();
                }
            }
        }
    }
}
