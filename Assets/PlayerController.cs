using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : PersonageController
{
    public float speed = 10;

    public bool openInventory;
    public bool openMenu;

    public InventoryController InventoryController;
    GameController GameController;


    // Start is called before the first frame update
    void Start()
    {
        GameController = FindObjectOfType<GameController>();
        base.Personage = GameController.Personage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Personage.isDeath) return;
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (openInventory)
            {
                InventoryController.Close();
                openInventory = false;
            }
            else
            {
                InventoryController.Open();
                openInventory = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (openInventory)
            {
                openInventory = false;
                InventoryController.Close();
            }
            else if (openMenu)
            {
                openMenu = true;
            }
            else
            {
                openMenu = false;
            }

        }

        if (openInventory || openMenu || AnimationController.State == StatePersonage.damage) return;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            MoveVertical(1);
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            MoveVertical(-1);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            MoveHorizontal(1);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            MoveHorizontal(-1);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow) &&
            !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.DownArrow) &&
            !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow) &&
            !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow))
            AnimationController.Idle();

    }

    public void Teleport(Vector3 position)
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        transform.position = position;
    }

    public override void MoveVertical(float direction)
    {
        // transform.position += Vector3.up * direction * speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, direction * speed * Time.deltaTime);

    }

    public override void MoveHorizontal(float direction)
    {
        // transform.position += Vector3.right * direction * speed;
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speed * Time.deltaTime, GetComponent<Rigidbody2D>().velocity.y);
        AnimationController.Walk(direction == 1);
    }

    public override void Attack()
    {

        var direction = new Vector3();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            direction.y = 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            direction.y = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            direction.x = 1;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            direction.x = -1;

        if (direction == Vector3.zero)
        {
            if (AnimationController.skeletonGraphic.transform.localScale.x > 0)
                direction.x = 1;
            else
                direction.x = -1;
        }

        FindObjectOfType<AttackController>().Generate(startShell.position, direction, Personage.ShellState);
        AnimationController.Attack(direction);
    }


}
