using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWP = 0;
    private GameObject currentDeposit;

    public float speed = 10.0f;
    public float rotSpeed = 10.0f;
    public float fuelSpeed = 0.5f;
    public float fuelCapacity = 10.0f;
    public float currentFuel;
    [SerializeField] private TextMeshProUGUI txtWin;
    [SerializeField] private TextMeshProUGUI txtFuel;


    // Start is called before the first frame update
    void Start()
    {
        currentFuel = fuelCapacity;
        currentDeposit = waypoints[currentWP];
        txtWin.gameObject.SetActive(false);
        txtFuel.gameObject.SetActive(true);
    }

    void LoseGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void WinGame()
    {
        Time.timeScale = 0.0f;
        txtWin.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            WinGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        txtFuel.text = "Fuel: " + currentFuel.ToString("F1") + "/10";
        if (Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position) < 3)
        {
            currentWP++;
            if ((currentFuel += currentDeposit.GetComponent<DepositManager>().ReadBenzine()) > 10.0)
            {
                currentFuel = 10.0f;
            }
            else
            {
                currentFuel += currentDeposit.GetComponent<DepositManager>().ReadBenzine();
            }
            

        }
        
        if (currentWP >= waypoints.Length)
            currentWP = 0;

        //this.transform.LookAt(waypoints[currentWP].transform);

        Quaternion lookatWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookatWP, rotSpeed * Time.deltaTime);
        if (currentFuel <= 0)
        {
            LoseGame();
        }
        else
        {
            currentFuel -= (Time.deltaTime * fuelSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        
    }
}
