// All actors that have movement use this interface

using UnityEngine;

public interface iMobile // Can move and attack
{
        void Idle();
        void Attack(int dmg);

        bool Patrol();

        void Chase();

        void Jump();

        void Move(Vector2 dir);
        
    
}
