using MoreMountains.TopDownEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheatLog : MonoBehaviour
{
    private bool showConsole = false;
    private string input;
    [SerializeField] private Character playerChar;

    public static DebugCommand DAMAGE;
    public static DebugCommand DOUBLE_DAMAGE;
    public static DebugCommand ADD_POINTS;
    public static DebugCommand ADD_RANDOM_POINTS;

    public List<object> commandList;

    private void Awake()
    {
        // Create commands
        DAMAGE = new DebugCommand("damage", "Damages the player", "damage", () =>
        {
            //playerChar.Damage(10);

            Health playerHealth = playerChar.GetComponent<Health>();
            playerHealth.Damage(10f, this.gameObject, 0.1f, 0.1f, new Vector3(0, 0, 0));
        });

        DOUBLE_DAMAGE = new DebugCommand("double_damage", "Deals more damage", "double_damage", () =>
        {
            Health playerHealth = playerChar.GetComponent<Health>();
            playerHealth.Damage(20f, this.gameObject, 0.1f, 0.1f, new Vector3(0, 0, 0));
        });

        ADD_POINTS = new DebugCommand("dingdingding", "Grants the player points", "dingdingding", () =>
        {
            GameManager.Instance.AddPoints(100);
        });

        ADD_RANDOM_POINTS = new DebugCommand("randdingdingding", "Grants the player random amount of points", "randdingdingding", () => 
        {
            int randInt = Random.Range(0, 100);

            GameManager.Instance.AddPoints(randInt);
        });

        commandList = new List<object>
        {
            DAMAGE,
            DOUBLE_DAMAGE,
            ADD_POINTS,
            ADD_RANDOM_POINTS,
        };
    }

    private void Start()
    {
        Invoke("Init", 1f);
    }

    private void Init()
    {
        playerChar = DW_GameManager.Instance.GetPlayerChar();
        Debug.Log(playerChar.ToString());
    }

    private void Update()
    {
        // Toggle console with `
        if (Input.GetButtonDown("CheatLog"))
        {
            OnToggleDebug();
        }

        // Press Enter to submit command
        if (showConsole && Input.GetButtonDown("Submit"))
        {
            OnReturn();
        }
    }

    public void OnToggleDebug()
    {
        showConsole = !showConsole;
        GameManager.Instance.Pause(PauseMethods.NoPauseMenu);
        //LevelManager.Instance.ToggleCharacterPause();
        //GUIManager.Instance.SetPauseScreen(false);
    }

    public void OnReturn()
    {
        OnToggleDebug();
        HandleInput();
        input = "";
    }

    private void OnGUI()
    {
        if (!showConsole)
            return;

        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);

        // Text field
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 120f, 20f), input);

        // Submit button
        if (GUI.Button(new Rect(Screen.width - 100f, y + 5f, 90f, 20f), "Submit"))
        {
            OnReturn();
        }
    }


    private void HandleInput()
    {
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.CommandId))
            {
                (commandList[i] as DebugCommand).Invoke();
            }
        }
    }
}
