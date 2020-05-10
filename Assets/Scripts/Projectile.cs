using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 _direction;
    Transform _transform;
    Camera _camera;

    public float shotSpeed;

    private float lifeTime;
    private Vector3 velocity;

    private const float maxLifeTime = 10;
    
    //We set values in a Init method. Virtual, so we can extend it later :)
    public virtual Projectile Init(Vector3 direction){
        this._direction = direction;

        this._direction = Camera.main.ScreenToWorldPoint(direction);
        this._direction.z = 0;

        return this;
    }

    void Start()
    {
        this._transform = this.transform;
        this._camera = Camera.main;
        //this.Rotate();
        lifeTime = 0;
        velocity = (this._direction - this._transform.position).normalized;
    }

    void Update()
    {
        this._transform.position += velocity * Time.deltaTime * shotSpeed;
        lifeTime += Time.deltaTime;
        if(lifeTime > maxLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    
    void Rotate(){
        Vector3 pos = this._transform.position + this._direction;
        float AngleRad = Mathf.Atan2(pos.y - this._transform.position.y, pos.x - this._transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this._transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
    }
}
