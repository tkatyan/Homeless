using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{

    public int window = 0;
    public string s;
    public float moveV;
    public float moveH;
    public int posV = 0;
    public int maxPosV;
    public int posH = 0;
    private int maxPosH;
    public float timer = 0;
    // Use this for initialization
    private void Start()
    {

    }

    private void FixedUpdate()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        moveV = Input.GetAxis("Vertical");
        if (timer > 0)
        {
            moveV = 0;
            moveH = 0;
        }
        if (moveV > 0.2f)
        {
            if (posV == 0)
                posV = maxPosV;
            else
            {
                posV--;
            }
            timer = 0.2f;
        }
        if (moveV < -0.2f)
        {
            if (posV == maxPosV)
                posV = 0;
            else
            {
                posV++;
            }
            timer = 0.2f;
        }
        moveH = Input.GetAxis("Horizontal");
        if (moveH > 0.2f)
        {
            posH = 1;
            timer = 0.2f;
        }
        if (moveH < -0.2f)
        {
            posH = -1;
            timer = 0.2f;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    void OnGUI()
    {
        s = GUI.GetNameOfFocusedControl();
        //GUI.FocusControl("Новая игра");
        //GUI.FocusControl("Выход");
        //GUI.SetNextControlName("Выход");
        if (window == 0) //главное меню
        {
            maxPosV = 2;
            //GUI.SetNextControlName("Новая игра");
            GUI.Box(new Rect(Screen.width / 2 - 105, Screen.height / 2 - 150, 210, 300), "МЕНЮ");
            GUI.SetNextControlName("Новая игра");
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 50), "Новая игра") 
                || GUI.GetNameOfFocusedControl() == "Новая игра" && posH == 1)
            {
                posH = 0;
                Application.LoadLevel(1);
            }
            GUI.SetNextControlName("Настройки");
            //GUI.FocusControl("Настройки");
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 50), "Настройки")
                || GUI.GetNameOfFocusedControl() == "Настройки" && posH == 1)
            {
                window = 1;
                posH = 0;
                posV = 0;
                Debug.Log("window 1");
            }
            GUI.SetNextControlName("Выход");
            //GUI.FocusControl("Выход");
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 100,  200, 50), "Выход") 
                || GUI.GetNameOfFocusedControl() == "Выход" && posH == 1 || posH == -1)
            {
                posH = 0;
                Application.Quit();
            }
            if (window == 0)
            {
                if (posV == 0)
                {
                    GUI.FocusControl("Новая игра");
                    Debug.Log("NG");
                }
                if (posV == 1)
                    GUI.FocusControl("Настройки");
                if (posV == 2)
                    GUI.FocusControl("Выход");
            }

        }
        if (window == 1) // Настройки
            {
                maxPosV = 0;
                GUI.Box(new Rect(Screen.width / 2 - 105, Screen.height / 2 - 150, 210, 300), "НАСТРОЙКИ");
                GUI.SetNextControlName("Назад");
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 50), "Назад")
                    || GUI.GetNameOfFocusedControl() == "Назад" && posH == 1 || posH == -1)
                {
                    window = 0;
                    posV = 0;
                    posH = 0;
                    Debug.Log("window 0");
                }
                if (window == 1)
                {
                    if (posV == 0)
                    {
                        GUI.FocusControl("Назад");
                        Debug.Log("NAZAD");
                    }
                }
            }
    }
     
    /*
    public string username = "username";
    public string pwd = "a pwd";
    
    private void OnGUI()
    {
        GUI.SetNextControlName("MyTextField");
        username = GUI.TextField(new Rect(10, 10, 100, 20), username);
        GUI.SetNextControlName("piy");
        pwd = GUI.TextField(new Rect(10, 40, 100, 20), pwd);
        if (GUI.Button(new Rect(10, 70, 80, 20), "Move Focus"))
            GUI.FocusControl("MyTextField");
        if (GUI.Button(new Rect(10, 100, 80, 20), "BITCH"))
            GUI.FocusControl("piy");
    }
     */
}
