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
        PlayerFinish player = new PlayerFinish(new Vector2(-6, -4), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6, -4), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(0, -3), Quaternion.identity, 0);
        Obstacle[] obstacles = {star};

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL1()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6, 2), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6, -4), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(0, 0), Quaternion.identity, 0);
        Obstacle[] obstacles = { star };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL2()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(6, 2), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-6, -4), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(5, 0.5f), Quaternion.identity, 0);
        Obstacle[] obstacles = { star };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL3()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0, 3), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0, -4), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(0, 2.4f), Quaternion.identity, 0);
        Obstacle[] obstacles = { star };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL4()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6, -4f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6, 2), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(0, -3), Quaternion.identity, 0);
        Obstacle[] obstacles = { star };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL5()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0, 1), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0, -2), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(1.5f, 4.5f), Quaternion.identity, 0);
        Obstacle[] obstacles = { star };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL6()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0, -4), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0, 2.5f), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(-5, -3), Quaternion.identity, 0);
        Obstacle[] obstacles = { star };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL7()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6, 0), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6, 0), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(0, -1.5f), Quaternion.identity, 0);
        Obstacle wall1 = new Obstacle(new Vector2(-2, 1.5f), Quaternion.identity, 1);
        Obstacle wall2 = new Obstacle(new Vector2(2, 1.5f), Quaternion.identity, 1);
        Obstacle[] obstacles = { star, wall1, wall2 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL8()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6, -4), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(4.5f, 2), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(3, -3.5f), Quaternion.identity, 0);
        Obstacle wall1 = new Obstacle(new Vector2(1, 2), Quaternion.Euler(0, 0, 90), 1);
        Obstacle wall2 = new Obstacle(new Vector2(-1.5f, 2), Quaternion.Euler(0,0,90), 1);
        Obstacle wall3 = new Obstacle(new Vector2(-4, 2), Quaternion.Euler(0, 0, 90), 1);
        Obstacle wall4 = new Obstacle(new Vector2(-6.5f, 2), Quaternion.Euler(0, 0, 90), 1);
        Obstacle[] obstacles = { star, wall1, wall2, wall3, wall4 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL9()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6, -3), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(4.5f, -3), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(3, 3), Quaternion.identity, 0);
        Obstacle wall1 = new Obstacle(new Vector2(0, -4), Quaternion.identity, 1);
        Obstacle wall2 = new Obstacle(new Vector2(0, -1.5f), Quaternion.identity, 1);
        Obstacle wall3 = new Obstacle(new Vector2(0, 1), Quaternion.identity, 1);
        Obstacle[] obstacles = { star, wall1, wall2, wall3};

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL10()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(6, -3), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-4.5f, 0), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(-4.5f, 2), Quaternion.identity, 0);
        Obstacle wall1 = new Obstacle(new Vector2(-6, 1.25f), Quaternion.identity, 1);
        Obstacle wall2 = new Obstacle(new Vector2(-3, 1.25f), Quaternion.identity, 1);
        Obstacle wall3 = new Obstacle(new Vector2(-1, 4), Quaternion.Euler(0, 0, -45), 1);
        Obstacle wall4 = new Obstacle(new Vector2(0, 5), Quaternion.Euler(0, 0, -45), 1);
        Obstacle[] obstacles = { star, wall1, wall2, wall3, wall4};

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL11()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6, -2), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(8, 2), Quaternion.Euler(0,0,90));

        Obstacle star = new Obstacle(new Vector2(-2, -3.5f), Quaternion.identity, 0);
        Obstacle wall1 = new Obstacle(new Vector2(7, 3.5f), Quaternion.Euler(0, 0, 90), 1);
        Obstacle wall2 = new Obstacle(new Vector2(7, 0.5f), Quaternion.Euler(0, 0, 90), 1);
        Obstacle wall3 = new Obstacle(new Vector2(6, 3.5f), Quaternion.Euler(0, 0, -45), 1);
        Obstacle wall4 = new Obstacle(new Vector2(6, 0.5f), Quaternion.Euler(0, 0, 45), 1);
        Obstacle[] obstacles = { star, wall1, wall2, wall3, wall4 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL12()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0, -4), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0, 2), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(0, 3.5f), Quaternion.identity, 0);
        Obstacle wall1 = new Obstacle(new Vector2(-1.5f, 1.5f), Quaternion.identity, 1);
        Obstacle wall2 = new Obstacle(new Vector2(1.5f, 1.5f), Quaternion.identity, 1);
        Obstacle wall3 = new Obstacle(new Vector2(-1.5f, 2.5f), Quaternion.Euler(0, 0, -45), 1);
        Obstacle wall4 = new Obstacle(new Vector2(1.5f, 2.5f), Quaternion.Euler(0, 0, 45), 1);
        Obstacle[] obstacles = { star, wall1, wall2, wall3, wall4 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL13()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0, 3), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0, -4), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(0, 2), Quaternion.identity, 0);
        Obstacle wall1 = new Obstacle(new Vector2(-1.25f, -2), Quaternion.Euler(0, 0, 90), 1);
        Obstacle wall2 = new Obstacle(new Vector2(1.25f, -2), Quaternion.Euler(0, 0, 90), 1);
        Obstacle wall3 = new Obstacle(new Vector2(-3.5f, -2), Quaternion.Euler(0, 0, 90), 1);
        Obstacle wall4 = new Obstacle(new Vector2(3.5f, -2), Quaternion.Euler(0, 0, 90), 1);
        Obstacle[] obstacles = { star, wall1, wall2, wall3, wall4 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL14()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(7, 0), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-7, 0), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(0, 0), Quaternion.identity, 0);
        Obstacle saw1 = new Obstacle(new Vector2(3, 0.5f), Quaternion.identity, 2);
        Obstacle saw2 = new Obstacle(new Vector2(0, 2), Quaternion.identity, 2);
        Obstacle saw3 = new Obstacle(new Vector2(-3, 0.5f), Quaternion.identity, 2);
        Obstacle[] obstacles = { star, saw1, saw2, saw3 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL15()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-7, -4), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(7, -4), Quaternion.identity);

        Obstacle star = new Obstacle(new Vector2(8, -2), Quaternion.identity, 0);
        Obstacle saw1 = new Obstacle(new Vector2(0, -4), Quaternion.identity, 2);
        Obstacle saw2 = new Obstacle(new Vector2(0, -3), Quaternion.identity, 2);
        Obstacle saw3 = new Obstacle(new Vector2(0, -2), Quaternion.identity, 2);
        Obstacle saw4 = new Obstacle(new Vector2(0, -1), Quaternion.identity, 2);
        Obstacle saw5 = new Obstacle(new Vector2(0, 0), Quaternion.identity, 2);
        Obstacle saw6 = new Obstacle(new Vector2(0, 1), Quaternion.identity, 2);
        Obstacle saw7 = new Obstacle(new Vector2(0, 2), Quaternion.identity, 2);
        Obstacle saw8 = new Obstacle(new Vector2(0, 3), Quaternion.identity, 2);
        Obstacle saw9 = new Obstacle(new Vector2(0, 4), Quaternion.identity, 2);
        Obstacle[] obstacles = { star, saw1, saw2, saw3, saw4, saw5, saw6, saw7, saw8, saw9 };

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    //obstacle types: star - 0, wall - 1, saw - 2
}


