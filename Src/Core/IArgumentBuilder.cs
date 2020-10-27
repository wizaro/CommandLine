// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     Builds an <see cref="Argument"/>.
    /// </summary>
    [CLSCompliant(false)]
    public interface IArgumentBuilder
    {
        /// <summary>
        ///     Builds an <see cref="Argument"/>.
        /// </summary>
        /// <returns>Argument built.</returns>
        Argument Build();
    }
}