// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Builds a <see cref="Command"/>.
    /// </summary>
    [CLSCompliant(false)]
    public interface ICommandBuilder
    {
        /// <summary>
        ///     Gets a value indicating whether the command built will be a root command.
        /// </summary>
        /// <value><see langword="true"/> if the command will be a root command; <see langword="false"/> otherwise.</value>
        bool IsRoot { get; }

        /// <summary>
        ///     Builds a <see cref="Command"/>.
        /// </summary>
        /// <returns>Command built.</returns>
        Command Build();
    }
}