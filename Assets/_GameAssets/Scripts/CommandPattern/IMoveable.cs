using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void MoveUp();
    void MoveDiagonalLeft();
    void MoveDiagonalRight();
    void MoveJump();

    /*
    void MoveDown();
    void MoveLeft();
    void MoveRight();
    */
}
