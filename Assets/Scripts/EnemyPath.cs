using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {

    // config parameters
    [SerializeField] List<GameObject> waypoints;
    [SerializeField] float moveSpeed;

    int waypointIndex = 0;

	// Use this for initialization
	void Start () {
        transform.position = waypoints[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        var targetPosition = waypoints[waypointIndex].transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
        }
    }
}
