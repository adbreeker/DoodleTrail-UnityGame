using UnityEngine;

public class ScaleToFitScreen : MonoBehaviour
{
    private SpriteRenderer sr;
    private MeshRenderer mr;

    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mr = GetComponent<MeshRenderer>();

        // world height is always camera's orthographicSize * 2
        float worldScreenHeight = Camera.main.orthographicSize * 2;

        // world width is calculated by dividing world height with screen height
        // then multiplying it with screen width
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 boundsSize;

        if (sr != null && sr.sprite != null)
        {
            boundsSize = sr.sprite.bounds.size;
        }
        else if (mr != null)
        {
            boundsSize = mr.bounds.size*0.5f;
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer or MeshRenderer found on " + gameObject.name);
            return;
        }

        // to scale the game object we divide the world screen width with the
        // size x of the bounds, and we divide the world screen height with the
        // size y of the bounds
        transform.localScale = new Vector3(
            worldScreenWidth / boundsSize.x,
            worldScreenHeight / boundsSize.y,
            1);
    }
} 
