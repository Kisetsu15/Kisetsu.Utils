namespace Kisetsu.Utils {
    /// <summary>
    /// Provides utility methods for interacting with the console, including input and message display.
    /// </summary>
    public static class Terminal {

        /// <summary>
        /// Prompts the user for input with a specified console text color and an optional message.
        /// </summary>
        /// <param name="color">The color of the console text for the prompt.</param>
        /// <param name="message">An optional message to display before reading input.</param>
        /// <returns>The input entered by the user as a string.</returns>
        public static string Input(ConsoleColor color, string message = "") {
            Console.ForegroundColor = color;
            Console.Write(message);
            string input = Console.ReadLine()!;
            Console.ResetColor();
            return input;
        }

        /// <summary>
        /// Prompts the user for input and attempts to convert it to a specified value type.
        /// </summary>
        /// <typeparam name="T">The type to which the input should be converted. Must be a value type.</typeparam>
        /// <param name="color">The color of the console text for the prompt.</param>
        /// <param name="message">An optional message to display before reading input.</param>
        /// <returns>
        /// The input converted to the specified type, or the default value of the type if conversion fails.
        /// </returns>
        public static T Input<T>(ConsoleColor color, string message = "") where T : struct {
            string input = Input(color, message);
            try {
                return (T) Convert.ChangeType(input, typeof(T));
            } catch (Exception e) {
                Error(e);
                return default;
            }
        }

        /// <summary>
        /// Prompts the user for input using the current console text color and an optional message.
        /// </summary>
        /// <param name="message">An optional message to display before reading input.</param>
        /// <returns>The input entered by the user as a string.</returns>
        public static string Input(string message = "") => Input(Console.ForegroundColor, message);

        /// <summary>
        /// Prompts the user for input and attempts to convert it to a specified value type.
        /// </summary>
        /// <typeparam name="T">The type to which the input should be converted. Must be a value type.</typeparam>
        /// <param name="message">An optional message to display before reading input.</param>
        /// <returns>
        /// The input converted to the specified type, or the default value of the type if conversion fails.
        /// </returns>
        public static T Input<T>(string message = "") where T : struct {
            return Input<T>(Console.ForegroundColor, message);
        }

        /// <summary>
        /// Displays a message in the console with a specified text color.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="color">The color of the console text for the message.</param>
        public static void WriteLine(string message, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays an error message in the console with a red text color.
        /// </summary>
        /// <param name="exception">The exception whose message will be displayed as an error.</param>
        public static void Error(Exception exception) => WriteLine($"Error: {exception.Message}", ConsoleColor.Red);

        /// <summary>
        /// Writes a message to the console with the specified color.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="color">The color to use for the message.</param>
        public static void Write(string message, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays a message in the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public static void WriteLine(string message) => Console.WriteLine(message);

        /// <summary>
        /// Writes a message to the console.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public static void Write(string message) => Console.Write(message);
    }
}