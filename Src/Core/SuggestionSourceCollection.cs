// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.ObjectModel;
using System.CommandLine.Suggestions;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     Collection of suggestion sources.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    [CLSCompliant(false)]
    public sealed class SuggestionSourceCollection
        : Collection<ISuggestionSource>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SuggestionSourceCollection"/> class.
        /// </summary>
        internal SuggestionSourceCollection()
        {
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected override void InsertItem(int index, ISuggestionSource item)
        {
            base.InsertItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected override void SetItem(int index, ISuggestionSource item)
        {
            base.SetItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }
    }
}