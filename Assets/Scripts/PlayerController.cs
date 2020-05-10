using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Transform _transform;
    Camera _camera;

    public float speed;
    public Projectile laserShot;

    private float shotTimer;

    private const float shotDelay = 0.2f;

    void Start()
    {
        this._transform = transform;
        this._camera = Camera.main;

        shotTimer = 0;
    }

    //Standard UpdateLoop (once per Frame)
    void Update()
    {
        this.Rotate();
        
        if (Input.GetKey("w"))
        {
            this.MoveUp();
        }
        if (Input.GetKey("s"))
        {
            this.MoveDown();
        }
        if (Input.GetKey("a"))
        {
            this.MoveLeft();
        }
        if (Input.GetKey("d"))
        {
            this.MoveRight();
        }

        shotTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if(shotTimer < 0)
            {
                this.FireShot(Input.mousePosition);
                shotTimer = shotDelay;
            }          
        }
    }

    void Rotate(){
        Vector2 mousePos = this._camera.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePos.y - this._transform.position.y, mousePos.x - this._transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        this._transform.rotation = Quaternion.Euler(0, 0, angleDeg - 90);//diese -90 sind nötig für Sprites, die nach oben zeigen. Nutzen Sie andere Assets, könnte es sein, dass die das anpassen müssen       
    }

    void MoveUp()
    {
        this._transform.position += Time.deltaTime * speed * new Vector3(0, 1, 0);
    }

    void MoveDown()
    {
        this._transform.position += Time.deltaTime * speed * new Vector3(0, -1, 0);
    }

    void MoveLeft()
    {
        this._transform.position += Time.deltaTime * speed * new Vector3(-1, 0, 0);
    }

    void MoveRight()
    {
        this._transform.position += Time.deltaTime * speed * new Vector3(1, 0, 0);
    }

    void FireShot(Vector3 direction)
    {
        Instantiate(laserShot, this._transform.position, this._transform.rotation).Init(direction);
    }
}
