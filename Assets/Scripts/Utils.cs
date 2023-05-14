using UnityEngine;

public class Utils {
    public static RaycastHit2D Raycast(Vector3 start, Vector2 dir, float dist, LayerMask mask) {
        RaycastHit2D hit = Physics2D.Raycast(start, dir, dist, mask);
        Debug.DrawRay(start, dir * dist, Color.green);
        return hit;
    }

    public static RaycastHit2D Boxcast(Vector3 start, Vector2 size, Vector2 dir, float dist, LayerMask mask) {
        var hit = Physics2D.BoxCast(start, size, 0, dir, dist, mask);

        //TODO debug draw
                
        return hit;
    }
}