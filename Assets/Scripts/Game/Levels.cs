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

    //obstacle types: star - 0, wall - 1, scissors - 2, wall rotating - 3, eraser - 4
}


