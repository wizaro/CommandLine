// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.ObjectModel;
using System.CommandLine.Parsing;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     Collection of command validators.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    [CLSCompliant(false)]
    public sealed class CommandValidatorCollection
        : Collection<ValidateSymbol<CommandResult>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandValidatorCollection"/> class.
        /// </summary>
        internal CommandValidatorCollection()
        {
        }

        /// <inheritdoc/>
        protected override void InsertItem(int index, ValidateSymbol<CommandResult> item)
        {
            base.InsertItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }

        /// <inheritdoc/>
        protected override void SetItem(int index, ValidateSymbol<CommandResult> item)
        {
            base.SetItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }
    }
}