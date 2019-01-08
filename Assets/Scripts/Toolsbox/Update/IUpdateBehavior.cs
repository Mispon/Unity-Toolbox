namespace Assets.Scripts.Toolsbox.Update {
    /// <summary>
    /// Defines the controlled behavior of updates
    /// </summary>
    public interface IUpdateBehavior {
        /// <summary>
        /// Regular update handler
        /// </summary>
        void Tick();

        /// <summary>
        /// Fixed 
        /// </summary>
        void FixedTick();

        /// <summary>
        /// Late update handler
        /// </summary>
        void LateTick();
    }
}
