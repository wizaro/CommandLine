// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;

namespace WiZaRo.CommandLine.Support
{
    /// <summary>
    ///     Contains builders for the exceptions used in the project.
    /// </summary>
    /// <threadsafety static="true" />
    internal static class Exceptions
    {
        /// <summary>
        ///     Builds an <see cref="ArgumentNullException" /> when an argument is null.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <returns><see cref="ArgumentNullException" /> with the desired data.</returns>
        public static ArgumentNullException BuildArgumentNull(string paramName)
        {
            return new ArgumentNullException(paramName);
        }
    }
}