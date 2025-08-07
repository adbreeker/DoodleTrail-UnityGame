using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBreak : MonoBehaviour
{
    public List<Sprite> fragmentsImages = new List<Sprite>();
    public Sprite engineFragment;
    public GameObject playerFragmentPrefab;

    List<GameObject> fragments;
    public void Break(Vector3 center_pos, Vector2 player_velocity, float dispresion)
    {
        //circle fragments
        fragments = new List<GameObject>();
        Vector2 frag_vel = Vector2Extension.Rotate(player_velocity, -(fragmentsImages.Count/2) * dispresion);
        foreach(Sprite image in fragmentsImages)
        {
            GameObject fragment = Instantiate(playerFragmentPrefab, center_pos, Quaternion.identity);
            fragment.GetComponent<SpriteRenderer>().sprite = image;
            fragment.AddComponent<PolygonCollider2D>();
            fragment.GetComponent<Rigidbody2D>().linearVelocity = frag_vel;
            fragments.Add(fragment);

            frag_vel = Vector2Extension.Rotate(frag_vel, dispresion);
        }

        //engine
        GameObject engine = Instantiate(playerFragmentPrefab, center_pos, Quaternion.identity);
        engine.GetComponent<SpriteRenderer>().sprite = engineFragment;
        engine.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        engine.transform.localScale = new Vector3(0.2f,0.2f,0);
        engine.AddComponent<PolygonCollider2D>();
        engine.GetComponent<Rigidbody2D>().linearVelocity = player_velocity;
    }
}
