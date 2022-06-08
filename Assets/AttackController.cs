using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public void Generate(Vector3 position, Vector3 direction, ShellState shell)
    {
        var rot = new Quaternion();
        if (direction == new Vector3(-1, 0, 0))
            rot = Quaternion.Euler(0, 0, 180);
        else if (direction == new Vector3(-1, -1, 0))
            rot = Quaternion.Euler(0, 0, -135);
        else if(direction == new Vector3(0, -1, 0))
            rot = Quaternion.Euler(0, 0, -90);
        else
            rot=Quaternion.Euler(0, 0, direction.y * 45 - direction.x * 45 + 45);


        var obj = Instantiate(shell.Prefab, position, rot, transform);

        Vector3 target = position + direction * shell.Time * shell.Speed;

        obj.GetComponent<ShellController>().StartAnim(target, shell);
    }
}
