﻿// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.ObjectModel;
using System.CommandLine;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Collection of configuration actions unto a <see cref="Command"/>.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    [CLSCompliant(false)]
    public sealed class CommandConfigurationCollection
        : Collection<Action<Command>>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandConfigurationCollection"/> class.
        /// </summary>
        internal CommandConfigurationCollection()
        {
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected override void InsertItem(int index, Action<Command> item)
        {
            base.InsertItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/>.</exception>
        protected override void SetItem(int index, Action<Command> item)
        {
            base.SetItem(index, item ?? throw Exceptions.BuildArgumentNull(nameof(item)));
        }
    }
}