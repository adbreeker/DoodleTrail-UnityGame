using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBreak : MonoBehaviour
{
    public List<Sprite> fragmentsImages = new List<Sprite>();
    public GameObject playerFragmentPrefab;

    List<GameObject> fragments;
    public void Break(Vector3 center_pos, Vector2 player_velocity, float dispresion)
    {
        StartCoroutine(TearingSound(center_pos));
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
    }

    IEnumerator TearingSound(Vector3 pos)
    {
        for(int i=0; i<4; i++)
        {
            SoundManager.Instance.PlaySound3D(SoundEnum.EFFECT_TEAR, pos, SoundType.GetType_OneShotMultiUse());
            yield return new WaitForSeconds(0.3f);
        }
    }
}
