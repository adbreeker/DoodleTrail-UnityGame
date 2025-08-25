using UnityEngine;
using System.Reflection;

public class Levels
{
    public Level getLevel(int level_Id)
    {
        MethodInfo method = this.GetType().GetMethod("CreateLvL" + level_Id.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);
        return (Level)method.Invoke(this, new object[] { });
    }

    public int LevelsCount()
    {
        int counter = 0;
        foreach (System.Reflection.MethodInfo method in this.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if(method.Name.Contains("CreateLvL"))
            {
                counter++;
            }
        }
        return counter;
    }

    public static int GetLvLStatus(int lvl_id)
    {
        string key = "LvL" + lvl_id.ToString() + "Status";
        int status;
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, -1);
            status = -1;
        }
        else
        {
            status = PlayerPrefs.GetInt(key);
        }

        return status;
    }

    public int CountStars()
    {
        int stars = 0;
        for(int i = 0; i < LevelsCount(); i++)
        {
            if(Levels.GetLvLStatus(i) > 0)
            {
                stars += Levels.GetLvLStatus(i);
            }
        }
        return stars;
    }



    // structs -----------------------------------------------------------------------------------------------------
    public struct PlayerFinish
    {
        public Vector3 position;
        public Quaternion rotation;

        public PlayerFinish(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }


    public struct Obstacle
    {
        public Vector3 position;
        public Quaternion rotation;
        public int type;

        public Obstacle(Vector3 position, Quaternion rotation, int type)
        {
            this.position = position;
            this.rotation = rotation;
            this.type = type;
        }
    }

    public struct Level
    {
        public PlayerFinish player;
        public PlayerFinish finish;
        public Obstacle[] obstacles;

        public Level(PlayerFinish player, PlayerFinish finish, Obstacle[] obstacles)
        {
            this.player = player;
            this.finish = finish;
            this.obstacles = obstacles;
        }
    }

    // list of levels: -------------------------------------------------------------------------------------------------
    
    Level CreateLvL0() // this is tutorial level
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.00f, -4.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6.00f, -3.82f), Quaternion.Euler(0, 0, 50.00f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(0.00f, -3.00f), Quaternion.identity, 0);

        Obstacle[] obstacles = { Type0_Star1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL1()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.00f, 2.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6.00f, -4.00f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(0.00f, 0.00f), Quaternion.identity, 0);

        Obstacle[] obstacles = { Type0_Star1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL2()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(5.00f, 2.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-6.00f, -4.00f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(5.00f, 0.50f), Quaternion.identity, 0);

        Obstacle[] obstacles = { Type0_Star1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL3()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0.00f, 3.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0.00f, -4.00f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(0.00f, 1.60f), Quaternion.identity, 0);

        Obstacle[] obstacles = { Type0_Star1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL4()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.00f, -4.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6.00f, 2.00f), Quaternion.Euler(0, 0, 110.00f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(0.00f, -3.00f), Quaternion.identity, 0);

        Obstacle[] obstacles = { Type0_Star1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL5()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0.00f, 1.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-4.83f, -2.00f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(1.50f, 4.50f), Quaternion.identity, 0);

        Obstacle[] obstacles = { Type0_Star1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL6()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-5.56f, 1.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-5.57f, -2.75f), Quaternion.Euler(0, 0, 281.65f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(1.50f, 4.50f), Quaternion.identity, 0);

        Obstacle[] obstacles = { Type0_Star1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL7()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.00f, 0.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6.00f, 0.00f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(0.00f, -1.50f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(-2.00f, 1.50f), Quaternion.identity, 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(2.00f, 1.50f), Quaternion.identity, 1);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL8()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.00f, -4.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(5.00f, -0.53f), Quaternion.Euler(0, 0, 284.87f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(6.60f, -1.99f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(0.85f, -1.24f), Quaternion.Euler(0.00f, 0.00f, 315.00f), 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(-1.09f, -2.20f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall3 = new Obstacle(new Vector2(-3.06f, -3.16f), Quaternion.Euler(0.00f, 0.00f, 315.00f), 1);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2, Type1_Wall3 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL9()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.00f, -3.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(4.50f, -3.00f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(3.00f, 3.00f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(0.00f, -4.00f), Quaternion.identity, 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(0.00f, -1.50f), Quaternion.identity, 1);
        Obstacle Type1_Wall3 = new Obstacle(new Vector2(0.00f, 1.00f), Quaternion.identity, 1);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2, Type1_Wall3 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL10()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(6.00f, -3.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-4.50f, 0.00f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(-5.53f, 4.06f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(-6.10f, 1.15f), Quaternion.identity, 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(-2.91f, 1.15f), Quaternion.identity, 1);
        Obstacle Type1_Wall3 = new Obstacle(new Vector2(-0.53f, 4.47f), Quaternion.Euler(0.00f, 0.00f, 315.00f), 1);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2, Type1_Wall3 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL11()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.00f, -2.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(8.00f, 2.00f), Quaternion.Euler(0, 0, 90.00f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(-2.00f, -3.50f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(7.00f, 3.50f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(7.00f, 0.50f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall3 = new Obstacle(new Vector2(5.94f, 3.65f), Quaternion.Euler(0.00f, 0.00f, 315.00f), 1);
        Obstacle Type1_Wall4 = new Obstacle(new Vector2(6.00f, 0.18f), Quaternion.Euler(0.00f, 0.00f, 45.00f), 1);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2, Type1_Wall3, Type1_Wall4 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL12()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0.00f, -4.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0.00f, 2.00f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(-5.21f, 3.06f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(-1.50f, 1.50f), Quaternion.identity, 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(1.50f, 1.50f), Quaternion.identity, 1);
        Obstacle Type1_Wall3 = new Obstacle(new Vector2(-1.68f, 2.53f), Quaternion.Euler(0.00f, 0.00f, 315.00f), 1);
        Obstacle Type1_Wall4 = new Obstacle(new Vector2(1.76f, 2.47f), Quaternion.Euler(0.00f, 0.00f, 45.00f), 1);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2, Type1_Wall3, Type1_Wall4 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL13()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0.00f, 3.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0.00f, -4.00f), Quaternion.Euler(0, 0, 313.45f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(0.00f, 2.00f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(-1.25f, -2.00f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(1.25f, -2.00f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall3 = new Obstacle(new Vector2(-3.50f, -2.00f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall4 = new Obstacle(new Vector2(3.50f, -2.00f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2, Type1_Wall3, Type1_Wall4 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL14()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(7.00f, 0.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-7.00f, 0.00f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(0.00f, 0.00f), Quaternion.identity, 0);
        Obstacle Type2_Saw1 = new Obstacle(new Vector2(3.00f, 0.50f), Quaternion.identity, 2);
        Obstacle Type2_Saw2 = new Obstacle(new Vector2(0.00f, 2.00f), Quaternion.identity, 2);
        Obstacle Type2_Saw3 = new Obstacle(new Vector2(-3.00f, 0.50f), Quaternion.identity, 2);

        Obstacle[] obstacles = { Type0_Star1, Type2_Saw1, Type2_Saw2, Type2_Saw3 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL15()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-7.00f, -4.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(7.00f, -4.00f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(1.94f, -1.91f), Quaternion.identity, 0);
        Obstacle Type2_Scissors1 = new Obstacle(new Vector2(0.00f, -4.00f), Quaternion.identity, 2);
        Obstacle Type2_Scissors2 = new Obstacle(new Vector2(0.00f, -3.00f), Quaternion.identity, 2);
        Obstacle Type2_Scissors3 = new Obstacle(new Vector2(0.00f, -2.00f), Quaternion.identity, 2);
        Obstacle Type2_Scissors4 = new Obstacle(new Vector2(0.00f, -1.00f), Quaternion.identity, 2);
        Obstacle Type2_Scissors5 = new Obstacle(new Vector2(0.00f, 0.00f), Quaternion.identity, 2);
        Obstacle Type2_Scissors6 = new Obstacle(new Vector2(0.00f, 1.00f), Quaternion.identity, 2);
        Obstacle Type2_Scissors7 = new Obstacle(new Vector2(0.00f, 2.00f), Quaternion.identity, 2);
        Obstacle Type2_Scissors8 = new Obstacle(new Vector2(0.00f, 3.00f), Quaternion.identity, 2);
        Obstacle Type2_Scissors9 = new Obstacle(new Vector2(0.00f, 4.00f), Quaternion.identity, 2);

        Obstacle[] obstacles = { Type0_Star1, Type2_Scissors1, Type2_Scissors2, Type2_Scissors3, Type2_Scissors4, Type2_Scissors5, Type2_Scissors6, Type2_Scissors7, Type2_Scissors8, Type2_Scissors9 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL16()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-2.66f, 2.71f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(2.29f, -3.70f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(-6.34f, 0.79f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(0.69f, -2.78f), Quaternion.identity, 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(3.91f, -2.84f), Quaternion.identity, 1);
        Obstacle Type3_RotatingWall1 = new Obstacle(new Vector2(3.87f, -1.75f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 3);
        Obstacle Type3_RotatingWall2 = new Obstacle(new Vector2(0.68f, -1.75f), Quaternion.Euler(0.00f, 180.00f, 90.00f), 3);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2, Type3_RotatingWall1, Type3_RotatingWall2 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL17()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0.00f, -4.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-0.26f, 1.26f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(7.69f, -0.37f), Quaternion.identity, 0);
        Obstacle Type3_RotatingWall1 = new Obstacle(new Vector2(1.77f, 4.16f), Quaternion.identity, 3);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(0.88f, 4.49f), Quaternion.Euler(0.00f, 0.00f, 70.00f), 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(-0.79f, 4.49f), Quaternion.Euler(0.00f, 0.00f, 290.00f), 1);
        Obstacle Type1_Wall3 = new Obstacle(new Vector2(-1.60f, 3.15f), Quaternion.identity, 1);

        Obstacle[] obstacles = { Type0_Star1, Type3_RotatingWall1, Type1_Wall1, Type1_Wall2, Type1_Wall3 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL18()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-4.99f, -4.08f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(4.72f, -3.97f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(4.77f, 2.71f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(-7.32f, -3.13f), Quaternion.identity, 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(-2.66f, -3.13f), Quaternion.identity, 1);
        Obstacle Type1_Wall3 = new Obstacle(new Vector2(-6.44f, -1.30f), Quaternion.Euler(0.00f, 0.00f, 130.00f), 1);
        Obstacle Type3_RotatingWall1 = new Obstacle(new Vector2(-4.24f, -0.72f), Quaternion.Euler(0.00f, 180.00f, 300.00f), 3);
        Obstacle Type1_Wall4 = new Obstacle(new Vector2(-5.14f, -0.69f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2, Type1_Wall3, Type3_RotatingWall1, Type1_Wall4 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL19()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-5.16f, -3.32f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0.97f, 0.19f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(5.69f, -0.01f), Quaternion.identity, 0);
        Obstacle Type3_RotatingWall1 = new Obstacle(new Vector2(6.94f, -0.05f), Quaternion.identity, 3);
        Obstacle Type3_RotatingWall2 = new Obstacle(new Vector2(6.94f, -0.05f), Quaternion.Euler(0.00f, 0.00f, 27.38f), 3);
        Obstacle Type3_RotatingWall3 = new Obstacle(new Vector2(-0.27f, 1.57f), Quaternion.Euler(0.00f, 180.00f, 180.00f), 3);

        Obstacle[] obstacles = { Type0_Star1, Type3_RotatingWall1, Type3_RotatingWall2, Type3_RotatingWall3 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL20()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(3.82f, 1.97f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(3.75f, -4.71f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(-6.81f, -0.32f), Quaternion.identity, 0);
        Obstacle Type3_RotatingWall1 = new Obstacle(new Vector2(1.34f, 3.66f), Quaternion.identity, 3);
        Obstacle Type3_RotatingWall2 = new Obstacle(new Vector2(6.34f, 3.70f), Quaternion.identity, 3);
        Obstacle Type3_RotatingWall3 = new Obstacle(new Vector2(-5.53f, -0.65f), Quaternion.identity, 3);
        Obstacle Type3_RotatingWall4 = new Obstacle(new Vector2(-7.89f, 0.76f), Quaternion.identity, 3);
        Obstacle Type3_RotatingWall5 = new Obstacle(new Vector2(2.49f, -3.70f), Quaternion.identity, 3);

        Obstacle[] obstacles = { Type0_Star1, Type3_RotatingWall1, Type3_RotatingWall2, Type3_RotatingWall3, Type3_RotatingWall4, Type3_RotatingWall5 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL21()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-5.75f, -2.11f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6.53f, -2.20f), Quaternion.Euler(0, 0, 37.81f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(1.97f, -1.40f), Quaternion.identity, 0);
        Obstacle Type4_Eraser1 = new Obstacle(new Vector2(1.03f, -1.91f), Quaternion.identity, 4);
        Obstacle Type4_Eraser2 = new Obstacle(new Vector2(1.03f, -0.41f), Quaternion.identity, 4);

        Obstacle[] obstacles = { Type0_Star1, Type4_Eraser1, Type4_Eraser2 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL22()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(6.01f, -3.06f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-6.49f, 3.07f), Quaternion.Euler(0, 0, 245.00f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(1.04f, 0.67f), Quaternion.identity, 0);
        Obstacle Type4_Eraser1 = new Obstacle(new Vector2(-0.82f, 0.41f), Quaternion.Euler(0.00f, 0.00f, 335.00f), 4);

        Obstacle[] obstacles = { Type0_Star1, Type4_Eraser1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL23()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0.00f, 2.37f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0.00f, -4.74f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(0.00f, 1.00f), Quaternion.identity, 0);
        Obstacle Type4_Eraser1 = new Obstacle(new Vector2(-1.80f, 0.30f), Quaternion.Euler(0.00f, 0.00f, 315.00f), 4);
        Obstacle Type4_Eraser2 = new Obstacle(new Vector2(1.80f, 0.30f), Quaternion.Euler(0.00f, 0.00f, 225.00f), 4);
        Obstacle Type4_Eraser3 = new Obstacle(new Vector2(2.00f, -2.80f), Quaternion.identity, 4);
        Obstacle Type4_Eraser4 = new Obstacle(new Vector2(-2.00f, -2.80f), Quaternion.Euler(0.00f, 0.00f, 180.00f), 4);

        Obstacle[] obstacles = { Type0_Star1, Type4_Eraser1, Type4_Eraser2, Type4_Eraser3, Type4_Eraser4 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL24()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.50f, 0.79f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(7.91f, -4.86f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(7.96f, 3.02f), Quaternion.identity, 0);
        Obstacle Type4_Eraser1 = new Obstacle(new Vector2(-2.98f, 1.00f), Quaternion.Euler(0.00f, 0.00f, 270.00f), 4);
        Obstacle Type4_Eraser2 = new Obstacle(new Vector2(1.01f, 1.00f), Quaternion.Euler(0.00f, 180.00f, 90.00f), 4);
        Obstacle Type4_Eraser3 = new Obstacle(new Vector2(4.97f, 1.00f), Quaternion.Euler(0.00f, 0.00f, 270.00f), 4);
        Obstacle Type4_Eraser4 = new Obstacle(new Vector2(9.29f, 1.00f), Quaternion.Euler(0.00f, 180.00f, 90.00f), 4);

        Obstacle[] obstacles = { Type0_Star1, Type4_Eraser1, Type4_Eraser2, Type4_Eraser3, Type4_Eraser4 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL25()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(6.69f, -4.14f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-3.03f, -0.05f), Quaternion.Euler(0, 0, 88.42f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(0.00f, 1.00f), Quaternion.identity, 0);
        Obstacle Type5_Sharpener1 = new Obstacle(new Vector2(6.70f, -1.00f), Quaternion.identity, 5);
        Obstacle Type5_Sharpener2 = new Obstacle(new Vector2(-6.91f, -1.41f), Quaternion.identity, 5);

        Obstacle[] obstacles = { Type0_Star1, Type5_Sharpener1, Type5_Sharpener2 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL26()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0.00f, 2.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0.00f, 1.00f), Quaternion.Euler(0, 0, 180.00f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(6.22f, -2.18f), Quaternion.identity, 0);
        Obstacle Type5_Sharpener1 = new Obstacle(new Vector2(4.00f, -0.50f), Quaternion.Euler(0.00f, 180.00f, 0.00f), 5);

        Obstacle[] obstacles = { Type0_Star1, Type5_Sharpener1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL27()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0.07f, 2.41f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6.84f, 0.36f), Quaternion.Euler(0, 0, 50.00f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(-5.60f, 3.33f), Quaternion.identity, 0);
        Obstacle Type5_Sharpener1 = new Obstacle(new Vector2(-0.08f, -1.16f), Quaternion.identity, 5);

        Obstacle[] obstacles = { Type0_Star1, Type5_Sharpener1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL28()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-5.13f, -3.10f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-1.35f, 1.48f), Quaternion.Euler(0, 0, 52.67f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(5.68f, -0.13f), Quaternion.identity, 0);
        Obstacle Type5_Sharpener1 = new Obstacle(new Vector2(-0.08f, 3.20f), Quaternion.identity, 5);
        Obstacle Type5_Sharpener2 = new Obstacle(new Vector2(6.68f, -2.58f), Quaternion.identity, 5);

        Obstacle[] obstacles = { Type0_Star1, Type5_Sharpener1, Type5_Sharpener2 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL29()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-2.21f, -4.22f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(3.59f, 2.17f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(1.18f, 1.61f), Quaternion.identity, 0);
        Obstacle Type5_Sharpener1 = new Obstacle(new Vector2(-2.14f, -1.88f), Quaternion.identity, 5);
        Obstacle Type4_Eraser1 = new Obstacle(new Vector2(6.73f, -3.92f), Quaternion.identity, 4);

        Obstacle[] obstacles = { Type0_Star1, Type5_Sharpener1, Type4_Eraser1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL30()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-5.43f, -3.92f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(3.95f, 0.78f), Quaternion.Euler(0, 0, 270.00f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(0.00f, 1.00f), Quaternion.identity, 0);
        Obstacle Type2_Scissors1 = new Obstacle(new Vector2(3.87f, 2.19f), Quaternion.identity, 2);
        Obstacle Type3_RotatingWall1 = new Obstacle(new Vector2(5.48f, -0.43f), Quaternion.identity, 3);

        Obstacle[] obstacles = { Type0_Star1, Type2_Scissors1, Type3_RotatingWall1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL31()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.64f, -0.68f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(7.74f, -0.35f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(4.40f, 0.09f), Quaternion.identity, 0);
        Obstacle Type3_RotatingWall1 = new Obstacle(new Vector2(-2.44f, -0.68f), Quaternion.Euler(0.00f, 180.00f, 270.00f), 3);
        Obstacle Type3_RotatingWall2 = new Obstacle(new Vector2(1.76f, -0.68f), Quaternion.Euler(0.00f, 180.00f, 155.00f), 3);
        Obstacle Type2_Scissors1 = new Obstacle(new Vector2(6.38f, 0.28f), Quaternion.identity, 2);
        Obstacle Type2_Scissors2 = new Obstacle(new Vector2(9.14f, 0.33f), Quaternion.identity, 2);

        Obstacle[] obstacles = { Type0_Star1, Type3_RotatingWall1, Type3_RotatingWall2, Type2_Scissors1, Type2_Scissors2 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL32()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(4.27f, 1.99f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(7.92f, -3.74f), Quaternion.Euler(0, 0, 31.23f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(4.19f, 1.22f), Quaternion.identity, 0);
        Obstacle Type2_Scissors1 = new Obstacle(new Vector2(7.94f, 0.95f), Quaternion.identity, 2);
        Obstacle Type2_Scissors2 = new Obstacle(new Vector2(8.82f, -1.24f), Quaternion.identity, 2);
        Obstacle Type2_Scissors3 = new Obstacle(new Vector2(1.66f, 1.54f), Quaternion.identity, 2);
        Obstacle Type2_Scissors4 = new Obstacle(new Vector2(0.40f, 1.54f), Quaternion.identity, 2);
        Obstacle Type2_Scissors5 = new Obstacle(new Vector2(-1.00f, 1.54f), Quaternion.identity, 2);
        Obstacle Type2_Scissors6 = new Obstacle(new Vector2(-2.46f, 1.54f), Quaternion.identity, 2);
        Obstacle Type2_Scissors7 = new Obstacle(new Vector2(-6.15f, 1.54f), Quaternion.identity, 2);
        Obstacle Type2_Scissors8 = new Obstacle(new Vector2(-7.56f, 1.54f), Quaternion.identity, 2);
        Obstacle Type2_Scissors9 = new Obstacle(new Vector2(-9.07f, 1.54f), Quaternion.identity, 2);
        Obstacle Type5_Sharpener1 = new Obstacle(new Vector2(4.22f, -0.85f), Quaternion.identity, 5);
        Obstacle Type5_Sharpener2 = new Obstacle(new Vector2(-4.37f, 1.46f), Quaternion.identity, 5);

        Obstacle[] obstacles = { Type0_Star1, Type2_Scissors1, Type2_Scissors2, Type2_Scissors3, Type2_Scissors4, Type2_Scissors5, Type2_Scissors6, Type2_Scissors7, Type2_Scissors8, Type2_Scissors9, Type5_Sharpener1, Type5_Sharpener2 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL33()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.73f, 0.00f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(7.59f, -3.31f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(7.08f, 3.11f), Quaternion.identity, 0);
        Obstacle Type3_RotatingWall1 = new Obstacle(new Vector2(-2.56f, 0.00f), Quaternion.identity, 3);
        Obstacle Type3_RotatingWall2 = new Obstacle(new Vector2(-2.56f, 0.00f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 3);
        Obstacle Type3_RotatingWall3 = new Obstacle(new Vector2(1.55f, 0.00f), Quaternion.identity, 3);
        Obstacle Type3_RotatingWall4 = new Obstacle(new Vector2(1.55f, 0.00f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 3);
        Obstacle Type3_RotatingWall5 = new Obstacle(new Vector2(5.65f, 0.00f), Quaternion.Euler(0.00f, 180.00f, 180.00f), 3);
        Obstacle Type3_RotatingWall6 = new Obstacle(new Vector2(5.65f, 0.00f), Quaternion.Euler(0.00f, 180.00f, 270.00f), 3);
        Obstacle Type2_Scissors1 = new Obstacle(new Vector2(8.52f, 0.00f), Quaternion.identity, 2);
        Obstacle Type2_Scissors2 = new Obstacle(new Vector2(9.82f, 0.00f), Quaternion.identity, 2);
        Obstacle Type2_Scissors3 = new Obstacle(new Vector2(10.99f, 0.00f), Quaternion.identity, 2);

        Obstacle[] obstacles = { Type0_Star1, Type3_RotatingWall1, Type3_RotatingWall2, Type3_RotatingWall3, Type3_RotatingWall4, Type3_RotatingWall5, Type3_RotatingWall6, Type2_Scissors1, Type2_Scissors2, Type2_Scissors3 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL34()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.54f, -4.12f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-7.49f, 2.36f), Quaternion.Euler(0, 0, 286.83f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(-2.94f, 4.32f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(-5.15f, 1.80f), Quaternion.Euler(0.00f, 0.00f, 275.00f), 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(-2.78f, 2.00f), Quaternion.Euler(0.00f, 0.00f, 275.00f), 1);
        Obstacle Type1_Wall3 = new Obstacle(new Vector2(-0.39f, 2.23f), Quaternion.Euler(0.00f, 0.00f, 275.00f), 1);
        Obstacle Type5_Sharpener1 = new Obstacle(new Vector2(2.01f, 2.34f), Quaternion.identity, 5);
        Obstacle Type5_Sharpener2 = new Obstacle(new Vector2(5.60f, -1.68f), Quaternion.identity, 5);
        Obstacle Type2_Scissors1 = new Obstacle(new Vector2(-0.63f, -3.22f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 2);
        Obstacle Type2_Scissors2 = new Obstacle(new Vector2(-1.48f, 0.10f), Quaternion.Euler(0.00f, 0.00f, 123.66f), 2);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2, Type1_Wall3, Type5_Sharpener1, Type5_Sharpener2, Type2_Scissors1, Type2_Scissors2 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL35()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(5.92f, -2.04f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-6.19f, -4.68f), Quaternion.identity);

        Obstacle Type0_Star1 = new Obstacle(new Vector2(-6.08f, 2.65f), Quaternion.identity, 0);
        Obstacle Type2_Scissors1 = new Obstacle(new Vector2(3.21f, -1.80f), Quaternion.identity, 2);
        Obstacle Type2_Scissors2 = new Obstacle(new Vector2(3.21f, -0.72f), Quaternion.identity, 2);
        Obstacle Type2_Scissors3 = new Obstacle(new Vector2(3.21f, 0.45f), Quaternion.identity, 2);
        Obstacle Type2_Scissors4 = new Obstacle(new Vector2(8.54f, -1.92f), Quaternion.identity, 2);
        Obstacle Type2_Scissors5 = new Obstacle(new Vector2(8.54f, -0.84f), Quaternion.identity, 2);
        Obstacle Type2_Scissors6 = new Obstacle(new Vector2(8.54f, 0.33f), Quaternion.identity, 2);
        Obstacle Type2_Scissors7 = new Obstacle(new Vector2(-6.08f, 0.45f), Quaternion.identity, 2);
        Obstacle Type5_Sharpener1 = new Obstacle(new Vector2(5.90f, 2.04f), Quaternion.identity, 5);
        Obstacle Type4_Eraser1 = new Obstacle(new Vector2(-6.00f, 1.00f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 4);

        Obstacle[] obstacles = { Type0_Star1, Type2_Scissors1, Type2_Scissors2, Type2_Scissors3, Type2_Scissors4, Type2_Scissors5, Type2_Scissors6, Type2_Scissors7, Type5_Sharpener1, Type4_Eraser1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL36()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6.76f, -0.51f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(8.80f, 0.12f), Quaternion.Euler(0, 0, 90.00f));

        Obstacle Type0_Star1 = new Obstacle(new Vector2(7.88f, 2.05f), Quaternion.identity, 0);
        Obstacle Type1_Wall1 = new Obstacle(new Vector2(-3.30f, -1.14f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall2 = new Obstacle(new Vector2(-0.90f, -1.14f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall3 = new Obstacle(new Vector2(1.50f, -1.14f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall4 = new Obstacle(new Vector2(3.99f, -1.14f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall5 = new Obstacle(new Vector2(6.42f, -1.14f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall6 = new Obstacle(new Vector2(-3.30f, 3.90f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall7 = new Obstacle(new Vector2(-0.90f, 3.90f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall8 = new Obstacle(new Vector2(1.50f, 3.90f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall9 = new Obstacle(new Vector2(3.99f, 3.90f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type1_Wall10 = new Obstacle(new Vector2(6.42f, 3.90f), Quaternion.Euler(0.00f, 0.00f, 90.00f), 1);
        Obstacle Type5_Sharpener1 = new Obstacle(new Vector2(-0.72f, 0.84f), Quaternion.Euler(0.00f, 180.00f, 0.00f), 5);
        Obstacle Type2_Scissors1 = new Obstacle(new Vector2(-0.75f, -0.30f), Quaternion.identity, 2);
        Obstacle Type2_Scissors2 = new Obstacle(new Vector2(-0.75f, 0.75f), Quaternion.identity, 2);
        Obstacle Type2_Scissors3 = new Obstacle(new Vector2(2.04f, 2.28f), Quaternion.identity, 2);
        Obstacle Type4_Eraser1 = new Obstacle(new Vector2(-0.69f, 2.55f), Quaternion.identity, 4);

        Obstacle[] obstacles = { Type0_Star1, Type1_Wall1, Type1_Wall2, Type1_Wall3, Type1_Wall4, Type1_Wall5, Type1_Wall6, Type1_Wall7, Type1_Wall8, Type1_Wall9, Type1_Wall10, Type5_Sharpener1, Type2_Scissors1, Type2_Scissors2, Type2_Scissors3, Type4_Eraser1 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    //obstacle types: star - 0, wall - 1, scissors - 2, wall rotating - 3, eraser - 4, sharpener - 5
}


