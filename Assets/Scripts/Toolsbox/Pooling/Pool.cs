namespace Assets.Scripts.Toolsbox.Pooling {
    /// <summary>
    /// Common information about pooled objects
    /// </summary>
    [System.Serializable]
    public class Pool {
        public PoolType Type;
        public PoolBehavior Prefab;
        public int Count;
    }
}