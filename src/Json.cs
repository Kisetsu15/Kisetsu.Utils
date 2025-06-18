using Newtonsoft.Json;

namespace Kisetsu.Utils {
    /// <summary>
    /// Provides utility methods for JSON file operations.
    /// </summary>
    public static class Json {
        /// <summary>
        /// Loads a JSON file and deserializes it into a dictionary.
        /// </summary>
        /// <typeparam name="T">The type of the values in the dictionary.</typeparam>
        /// <param name="path">The path to the JSON file.</param>
        /// <returns>A dictionary containing the deserialized data.</returns>
        public static Dictionary<string, T> Load<T>(string path) => File.Exists(path) ?
        JsonConvert.DeserializeObject<Dictionary<string, T>>(File.ReadAllText(path)) ??
        [] :
        [];

        /// <summary>
        /// Serializes a dictionary and saves it to a JSON file.
        /// </summary>
        /// <typeparam name="T">The type of the values in the dictionary.</typeparam>
        /// <param name="path">The path to the JSON file.</param>
        /// <param name="data">The dictionary to serialize and save.</param>
        public static void Save<T>(string path, Dictionary<string, T> data) {
            if (!Directory.Exists(Path.GetDirectoryName(path)!)) {
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}
