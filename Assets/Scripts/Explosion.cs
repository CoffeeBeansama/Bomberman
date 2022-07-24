using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bomberman
{
public class Explosion : MonoBehaviour
{

    public SpriteAnimator start;
    public SpriteAnimator middle;
    public SpriteAnimator end;

    public void SetActiveRenderer(SpriteAnimator animator)
    {
        start.enabled = animator == start;
        middle.enabled = animator == middle;
        end.enabled = animator == end;
    }
    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg,Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject,seconds);
    }

   

  
}
}