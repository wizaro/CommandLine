// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.ObjectModel;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Collection of option builders.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    [CLSCompliant(false)]
    public sealed class OptionBuilderCollection
        : Collection<IOptionBuilder>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OptionBuilderCollection"/> class.
        /// </summary>
        internal OptionBuilderCollection()
        {
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected override void InsertItem(int index, IOptionBuilder item)
        {
            base.InsertItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected override void SetItem(int index, IOptionBuilder item)
        {
            base.SetItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }
    }
}