using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGun : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public ParticleSystem muzzleFlash;
    public GameObject muzzleLight;
    
    public GameObject gunShootPoint;
    
    // Update is called once per frame
    void Update()
    {
        TargetMouse();
        if (Input.GetButtonDown("Fire1"))
        {
            var muzzle = Instantiate(muzzleFlash);
            var gunPosition = gunShootPoint.transform.position;
            muzzle.transform.position = gunPosition;
            var light = Instantiate(muzzleLight);
            light.transform.position = new Vector3(gunPosition.x, gunPosition.y, -0.3f);

        }

        muzzleFlash.transform.localRotation = transform.rotation;
    }

    private void TargetMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
