// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Name validations used in various places.
    /// </summary>
    /// <threadsafety static="true"/>
    internal static class Validations
    {
        /// <summary>
        ///     Command prefixes.
        /// </summary>
        private static readonly string[] Prefixes = { "--", "-", "/" };

        /// <summary>
        ///     Gets a value indicating whether the provided alias is a valid alias.
        /// </summary>
        /// <param name="alias">Alias to validate.</param>
        /// <returns><see langword="true"/>if <paramref name="alias"/> is a valid alias; <see langword="false"/> otherwise.</returns>
        /// <remarks>A valid alias is not <see langword="null"/>, empty, and does not contain whitespaces; after removing the prefixes.</remarks>
        public static bool IsValidAlias(string? alias)
        {
            if (alias is null)
            {
                return false;
            }

            for (var i = 0; i < Prefixes.Length; i += 1)
            {
                var prefix = Prefixes[i];
                if (alias.StartsWith(prefix, StringComparison.InvariantCulture))
                {
                    alias = alias.Substring(prefix.Length);
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(alias))
            {
                return false;
            }

            for (var i = 0; i < alias.Length; i++)
            {
                if (char.IsWhiteSpace(alias[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Gets a value indicating whether the provided name is a valid argument name.
        /// </summary>
        /// <param name="name">Name to validate.</param>
        /// <returns><see langword="true"/> if <paramref name="name"/> is a valid argument name; <see langword="false"/> otherwise.</returns>
        /// <remarks>A valid argument name is not <see langword="null"/> or composed of only whitespaces.</remarks>
        public static bool IsValidArgumentName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        /// <summary>
        ///     Gets a value indicating whether the provided name is a valid command name.
        /// </summary>
        /// <param name="name">Name to validate.</param>
        /// <returns><see langword="true"/> if <paramref name="name"/> is a valid command name; <see langword="false"/> otherwise.</returns>
        /// <remarks>A valid command name is not <see langword="null"/>, empty, and does not contain whitespaces; after removing the prefixes.</remarks>
        public static bool IsValidCommandName(string? name)
        {
            if (name is null)
            {
                return false;
            }

            for (var i = 0; i < Prefixes.Length; i += 1)
            {
                var prefix = Prefixes[i];
                if (name.StartsWith(prefix, StringComparison.InvariantCulture))
                {
                    name = name.Substring(prefix.Length);
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            for (var i = 0; i < name.Length; i++)
            {
                if (char.IsWhiteSpace(name[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}