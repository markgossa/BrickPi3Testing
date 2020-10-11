using static LegoBuggy.Application.Services.Movement;

namespace LegoBuggy.Application.Services
{
    public interface IMovement
    {
        void TurnAroundOnTheSpot(TurnDirection direction = TurnDirection.Right);
        void Stop();
        void Turn(int speed, int sharpness, TurnDirection direction, int duration = -1, bool endMovement = false);
        void Move(int speed, MoveDirection moveDirection, int duration = -1, bool endMovement = false);
    }
}