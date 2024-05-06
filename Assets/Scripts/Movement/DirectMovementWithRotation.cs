namespace Movement
{
    public class DirectMovementWithRotation : DirectMovement
    {
        public override void StartMovement()
        {
            base.StartMovement();
            _transformUpdater.RotateContinuously(_properties.RotateSpeedInAngle);
        }
    }
}