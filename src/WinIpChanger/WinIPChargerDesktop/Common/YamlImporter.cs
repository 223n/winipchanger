using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WinIPChanger.Desktop.Common
{
    /// <summary>
    /// Import / Export Yaml File Helper
    /// </summary>
    internal class YamlHelper
    {

        /// <summary>
        /// Serialize
        /// </summary>
        /// <param name="path">Yaml File Path</param>
        /// <param name="value">Export Data</param>
        public static void Serialize(string path, object value)
        {
            var serializer = new SerializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
            File.WriteAllText(path, serializer.Serialize(value), Encoding.UTF8);
        }

        /// <summary>
        /// Deserialize
        /// </summary>
        /// <typeparam name="T">Result Type</typeparam>
        /// <param name="path">Yaml File Path</param>
        /// <returns>Deserialized Yaml File Value</returns>
        public static T Deserialize<T>(string path)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
            return deserializer.Deserialize<T>(File.ReadAllText(path, Encoding.UTF8));
        }

    }
}
