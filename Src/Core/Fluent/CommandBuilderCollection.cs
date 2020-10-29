// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.ObjectModel;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Collection of command builders.
    /// </summary>
    /// <remarks>Only accepts non-root command builders.</remarks>
    /// <threadsafety static="true" instance="false"/>
    [CLSCompliant(false)]
    public sealed class CommandBuilderCollection
        : Collection<ICommandBuilder>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBuilderCollection"/> class.
        /// </summary>
        internal CommandBuilderCollection()
        {
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException"><paramref name="item"/> builds a root command.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected override void InsertItem(int index, ICommandBuilder item)
        {
            if (item is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(item));
            }
            else if (item.IsRoot)
            {
                throw Exceptions.BuildArgumentBuilderIsRoot(nameof(item));
            }
            else
            {
                base.InsertItem(index, item);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException"><paramref name="item"/> builds a root command.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected override void SetItem(int index, ICommandBuilder item)
        {
            if (item is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(item));
            }
            else if (item.IsRoot)
            {
                throw Exceptions.BuildArgumentBuilderIsRoot(nameof(item));
            }
            else
            {
                base.SetItem(index, item);
            }
        }
    }
}