// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.ObjectModel;
using System.CommandLine.Parsing;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     Collection of argument validators.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    [CLSCompliant(false)]
    public sealed class ArgumentValidatorCollection
        : Collection<ValidateSymbol<ArgumentResult>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentValidatorCollection"/> class.
        /// </summary>
        internal ArgumentValidatorCollection()
        {
        }

        /// <inheritdoc/>
        protected override void InsertItem(int index, ValidateSymbol<ArgumentResult> item)
        {
            base.InsertItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }

        /// <inheritdoc/>
        protected override void SetItem(int index, ValidateSymbol<ArgumentResult> item)
        {
            base.SetItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }
    }
}