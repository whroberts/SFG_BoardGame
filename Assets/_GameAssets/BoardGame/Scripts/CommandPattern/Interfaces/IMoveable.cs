using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void MoveUp();
    void MoveDown();
    void MoveDiagonalUpLeft();
    void MoveDiagonalDownLeft();
    void MoveDiagonalUpRight();
    void MoveDiagonalDownRight();
    void MoveJumpUp();
    void MoveJumpDown();
    void MoveLeft();
    void MoveRight();

}
