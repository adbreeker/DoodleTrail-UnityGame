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

    Level CreateLvL0()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6, -4), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6, -4), Quaternion.identity);

        Obstacle obs1 = new Obstacle(new Vector2(0, -3), Quaternion.identity, 0);
        Obstacle[] obstacles = {obs1};

        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL1()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6, 2), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6, -4), Quaternion.identity);
        Obstacle[] obstacles = { };
        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL2()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(6, 2), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(-6, -4), Quaternion.identity);
        Obstacle[] obstacles = { };
        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL3()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0, 3), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0, -4), Quaternion.identity);
        Obstacle[] obstacles = { };
        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL4()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(-6, -3), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(6, 3), Quaternion.identity);
        Obstacle[] obstacles = { };
        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL5()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0, 1.5f), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0, -1.5f), Quaternion.identity);
        Obstacle[] obstacles = { };
        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

    Level CreateLvL6()
    {
        PlayerFinish player = new PlayerFinish(new Vector2(0, -3), Quaternion.identity);
        PlayerFinish finish = new PlayerFinish(new Vector2(0, 3), Quaternion.identity);
        Obstacle[] obstacles = { };
        Level lvl = new Level(player, finish, obstacles);

        return lvl;
    }

}


