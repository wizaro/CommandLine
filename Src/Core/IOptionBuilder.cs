// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     Builds an <see cref="Option"/>.
    /// </summary>
    [CLSCompliant(false)]
    public interface IOptionBuilder
    {
        /// <summary>
        ///     Builds an <see cref="Option"/>.
        /// </summary>
        /// <returns>Option built.</returns>
        Option Build();
    }
}