// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.ObjectModel;
using System.CommandLine;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     Collection of configuration actions unto an <see cref="Argument"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    [CLSCompliant(false)]
    public sealed class ArgumentConfigurationCollection
        : Collection<Action<Argument>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentConfigurationCollection"/> class.
        /// </summary>
        internal ArgumentConfigurationCollection()
        {
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected override void InsertItem(int index, Action<Argument> item)
        {
            base.InsertItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected override void SetItem(int index, Action<Argument> item)
        {
            base.SetItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }
    }
}