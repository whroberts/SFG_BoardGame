using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BoardGame
{
    public class InputController : MonoBehaviour
    {
        public event Action PressedConfirm = delegate { };
        public event Action PressedCancel = delegate { };

        public event Action PressedLeftClick = delegate { };
        public event Action PressedRightClick = delegate { };
        public event Action PressedUp = delegate { };
        public event Action PressedDown = delegate { };
        public event Action PressedLeft = delegate { };
        public event Action PressedRight = delegate { };
        
        public event Action PressedJump = delegate { };
        public event Action PressedDiagonalLeft = delegate { };
        public event Action PressedDiagonalRight = delegate { };
        
        public event Action PressedJumpDiagonalUpLeft = delegate { };
        public event Action PressedJumpDiagonalUpRight = delegate { };
        

        private void Update()
        {
            DetectConfirm();
            DetectCancel();

            DetectLeftClick();
            DetectRightClick();

            DetectUp();
            DetectDown();
            DetectLeft();
            DetectRight();

            DetectJumpDiagonalUpLeft();
            DetectJumpDiagonalUpRight();
            DetectJump();
            DetectDiagonalLeft();
            DetectDiagonalRight();
        }

        private void DetectConfirm()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PressedConfirm?.Invoke();
            }
        }

        private void DetectCancel()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                PressedCancel?.Invoke();
            }
        }
        private void DetectLeftClick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PressedLeftClick?.Invoke();
            }
        }

        private void DetectRightClick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                PressedRightClick?.Invoke();
            }
        }

        private void DetectUp()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                PressedUp?.Invoke();
            }
        }

        private void DetectDown()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                PressedDown?.Invoke();
            }
        }

        private void DetectLeft()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PressedLeft?.Invoke();
            }
        }

        private void DetectRight()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                PressedRight?.Invoke();
            }
        }

        
        private void DetectJumpDiagonalUpLeft()
        {
            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.Q))
            {
                PressedJumpDiagonalUpLeft?.Invoke();
            }
        }

        private void DetectJumpDiagonalUpRight()
        {
            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.E))
            {
                PressedJumpDiagonalUpRight?.Invoke();
            }
        }

        private void DetectJump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PressedJump?.Invoke();
            }
        }
        
        
        private void DetectDiagonalLeft()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PressedDiagonalLeft?.Invoke();
            }
        }

        private void DetectDiagonalRight()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PressedDiagonalRight?.Invoke();
            }
        }
    }
}
