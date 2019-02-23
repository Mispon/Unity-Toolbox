namespace Assets.Scripts.Toolsbox.UpdateBehavior {
    /// <summary>
    /// Defines the controlled behavior of late updates
    /// </summary>
    public interface ILateTick {
        /// <summary>
        /// Late update handler
        /// </summary>
        void LateTick();
    }
}