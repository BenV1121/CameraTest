using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float lookSensitivity = 5f;
    public float xRotation;
    public float yRotation;
    public float currentXRotation;
    public float currentYRotation;
    public float xRotationV;
    public float yRotationV;
    public float lookSmoothDamp = 0.1f;

    public List<Transform> targets;
    public Transform selectedTarget;

    public GameObject[] enemies;

    private Transform playerTransform;

    void Start()
    {
        targets = new List<Transform>();
        selectedTarget = null;
        playerTransform = transform;

        AddAllEnemies();
    }

    public void AddAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemies)
        {
            AddTarget(enemy.transform);
        }
    }

    public void AddTarget(Transform enemy)
    {
        targets.Add(enemy);
    }

    private void SortTargetsByDistance()
    {
        targets.Sort(delegate (Transform t1, Transform t2)
        {
            return Vector3.Distance(t1.position, playerTransform.position).CompareTo(Vector3.Distance(t2.position, playerTransform.position));
        });
    }

    private void TargetEnemy()
    {
        if(selectedTarget == null)
        {
            SortTargetsByDistance();
            selectedTarget = targets[0];
        }
        else
        {
            int index = targets.IndexOf(selectedTarget);
            if(index < targets.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            DeselectTarget();
            selectedTarget = targets[index];
        }
        SelectTarget();
    }

    private void SelectTarget()
    {
        //selectedTarget.renderer.material.color = Color.red;
    }

    private void DeselectTarget()
    {
        selectedTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        //xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        transform.rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        Lock();

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            TargetEnemy();
        }

        transform.LookAt(selectedTarget);
    }

    void Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
