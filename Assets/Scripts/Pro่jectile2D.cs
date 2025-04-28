using UnityEngine;

public class Pro : MonoBehaviour
{
    [SerializeField]  Transform shooterPoint;
    [SerializeField]  GameObject target;
    [SerializeField] Rigidbody2D bulletPrefab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);
            
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                //show target at click point
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log("hit " + hit.collider.name);
                
                //calculate projectile velocity
                Vector2 projectileVelocity = CalculateProjectileVelocity(shooterPoint.position, hit.point, 1f);
                
                //shoot bullet prefab using rigibody2D
                Rigidbody2D shooterBullet = Instantiate(bulletPrefab, shooterPoint.position, Quaternion.identity);
                
                //add projectile velocity vector to the bullet rigibody
                shooterBullet.linearVelocity = projectileVelocity;
            }
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
        
        //find velocity of x and y axis
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
        
        //get projectile vector
        Vector2 projectileVelocity = new Vector2(velocityX, velocityY);
        
        return projectileVelocity;
    }
    
}
