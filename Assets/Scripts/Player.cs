using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // config parameters
    [SerializeField] float moveSpeed;
    [SerializeField] float projectileSpeed;
    [SerializeField] float rateOfFire;
    [SerializeField] GameObject laserPrefab;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float padding = 1;

	// Use this for initialization
	void Start () {
        SetUpMoveBoundaries();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        Fire();
	}

    private void Move()
    {
        var newXPos = Mathf.Clamp(transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(1f / rateOfFire);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
