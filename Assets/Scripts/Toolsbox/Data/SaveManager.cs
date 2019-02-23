using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Toolsbox.Patterns;
using UnityEngine;

namespace Assets.Scripts.Toolsbox.Data {
    /// <summary>
    /// Save data manager
    /// </summary>
    public class SaveManager : Singleton<SaveManager> {
        private string _saveName = $"{Application.productName}.gd";

        private DataToSave _data;
        private BinaryFormatter _formatter;

        /// <summary>
        /// Child initialization
        /// </summary>
        protected override void ChildAwake() {
            _formatter = new BinaryFormatter();
        }

        /// <summary>
        /// Save game data
        /// </summary>
        public void Save() {
            var filePath = Path.Combine(Application.persistentDataPath, _saveName);
            var file = File.Open(filePath, FileMode.OpenOrCreate);
            _formatter.Serialize(file, _data);
            file.Close();
        }

        /// <summary>
        /// Load game data or create new
        /// </summary>
        public void Load() {
            var filePath = Path.Combine(Application.persistentDataPath, _saveName);
            if (File.Exists(filePath)) {
                var file = File.Open(filePath, FileMode.Open);
                _data = (DataToSave) _formatter.Deserialize(file);
                file.Close();
            }
            else
                _data = new DataToSave();
        }

        /// <summary>
        /// Returns game data
        /// </summary>
        public DataToSave GetData() => _data;
    }
}