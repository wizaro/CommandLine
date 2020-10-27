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
        ///     Builds an <see cref="ArgumentException" /> when a command builder would build a root command.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <returns><see cref="ArgumentException" /> with the desired data.</returns>
        public static ArgumentException BuildArgumentBuilderIsRoot(string paramName)
        {
            return new ArgumentException(Resources.ErrorBuilderIsRoot, paramName);
        }

        /// <summary>
        ///     Builds an <see cref="ArgumentException" /> when a command is a root command.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <returns><see cref="ArgumentException" /> with the desired data.</returns>
        public static ArgumentException BuildArgumentCommandIsRoot(string paramName)
        {
            return new ArgumentException(Resources.ErrorCommandIsRoot, paramName);
        }

        /// <summary>
        ///     Builds an <see cref="ArgumentException"/> when a collection contains an invalid alias.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        /// <returns><see cref="ArgumentException" /> with the desired data.</returns>
        public static ArgumentException BuildArgumentContainsInvalidAlias(string paramName, Exception? innerException = default)
        {
            return new ArgumentException(Resources.ErrorCollectionContainsInvalidAlias, paramName, innerException);
        }

        /// <summary>
        ///     Builds an <see cref="ArgumentException"/> when a collection does not contain aliases.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <returns><see cref="ArgumentException" /> with the desired data.</returns>
        public static ArgumentException BuildArgumentEmptyAliases(string paramName)
        {
            return new ArgumentException(Resources.ErrorAliasesCollectionEmpty, paramName);
        }

        /// <summary>
        ///     Builds an <see cref="ArgumentException" /> when an alias is invalid.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <returns><see cref="ArgumentException" /> with the desired data.</returns>
        public static Exception BuildArgumentInvalidAlias(string paramName)
        {
            return new ArgumentException(Resources.ErrorAliasInvalid, paramName);
        }

        /// <summary>
        ///     Builds an <see cref="ArgumentException" /> when an argument name is invalid.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <returns><see cref="ArgumentException" /> with the desired data.</returns>
        public static ArgumentException BuildArgumentInvalidArgumentName(string paramName)
        {
            return new ArgumentException(Resources.ErrorArgumentNameInvalid, paramName);
        }

        /// <summary>
        ///     Builds an <see cref="ArgumentException" /> when a command name is invalid.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <returns><see cref="ArgumentException" /> with the desired data.</returns>
        public static ArgumentException BuildArgumentInvalidCommandName(string paramName)
        {
            return new ArgumentException(Resources.ErrorCommandNameInvalid, paramName);
        }

        /// <summary>
        ///     Builds an <see cref="ArgumentNullException" /> when an argument is null.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the current exception.</param>
        /// <returns><see cref="ArgumentNullException" /> with the desired data.</returns>
        public static ArgumentNullException BuildArgumentNull(string paramName)
        {
            return new ArgumentNullException(paramName);
        }

        /// <summary>
        ///     Builds an <see cref="InvalidOperationException"/> when trying to remove the last alias from a collection.
        /// </summary>
        /// <returns><see cref="InvalidOperationException"/> with the desired data.</returns>
        public static InvalidOperationException BuildInvalidOperationCannotRemoveLastAlias()
        {
            return new InvalidOperationException(Resources.ErrorAliasesCollectionLastRemoved);
        }

        /// <summary>
        ///     Builds an <see cref="InvalidOperationException"/> when a builder would build a root command.
        /// </summary>
        /// <returns><see cref="InvalidOperationException"/> with the desired data.</returns>
        public static InvalidOperationException BuildInvalidOperationBuiltRootCommand()
        {
            return new InvalidOperationException(Resources.ErrorBuiltRootCommand);
        }
    }
}