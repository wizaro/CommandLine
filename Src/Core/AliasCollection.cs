// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     A collection of option aliases.
    /// </summary>
    /// <remarks>Every add or insert validates that the alias added is valid. At least one alias must remain in the collection; <see cref="Collection{T}.Clear"/> always leaves the first alias and <see cref="Collection{T}.Remove(T)"/> will throw an exception if trying to remove the last alias.</remarks>
    /// <threadsafety static="true" instance="false"/>
    public sealed class AliasCollection
        : Collection<string>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AliasCollection"/> class.
        /// </summary>
        /// <param name="aliases">Aliases to start the collection with.</param>
        /// <exception cref="ArgumentException">Either <paramref name="aliases"/> is empty or contains an invalid alias.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="aliases"/> is <see langword="null"/>.</exception>
        [SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification = "False positive.")]
        internal AliasCollection(params string[] aliases)
        {
            if (aliases is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(aliases));
            }
            else if (aliases.Length == 0)
            {
                throw Exceptions.BuildArgumentEmptyAliases(nameof(aliases));
            }

            for (var i = 0; i < aliases.Length; i += 1)
            {
                try
                {
                    this.Add(aliases[i]);
                }
                catch (ArgumentException exArg)
                {
                    throw Exceptions.BuildArgumentContainsInvalidAlias(nameof(aliases), exArg);
                }
            }
        }

        /// <inheritdoc/>
        protected override void ClearItems()
        {
            var first = this[0];
            base.ClearItems();
            this.Add(first);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException"><paramref name="item"/> is an invalid alias.</exception>
        protected override void InsertItem(int index, string item)
        {
            base.InsertItem(index, Validations.IsValidAlias(item) ? item : throw Exceptions.BuildArgumentInvalidAlias(nameof(item)));
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">The last alias cannot be removed.</exception>
        protected override void RemoveItem(int index)
        {
            if (this.Count > 1)
            {
                base.RemoveItem(index);
            }
            else
            {
                throw Exceptions.BuildInvalidOperationCannotRemoveLastAlias();
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException"><paramref name="item"/> is an invalid alias.</exception>
        protected override void SetItem(int index, string item)
        {
            base.SetItem(index, Validations.IsValidAlias(item) ? item : throw Exceptions.BuildArgumentInvalidAlias(nameof(item)));
        }
    }
}