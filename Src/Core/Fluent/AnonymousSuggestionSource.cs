// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.CommandLine.Suggestions;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     A suggestion source from a delegate.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    internal sealed class AnonymousSuggestionSource
        : ISuggestionSource
    {
        /// <summary>
        ///     Delegate that generates the suggestions.
        /// </summary>
        private readonly SuggestDelegate suggest;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnonymousSuggestionSource"/> class.
        /// </summary>
        /// <param name="suggest">Delegate that generates the suggestions.</param>
        public AnonymousSuggestionSource(SuggestDelegate suggest)
        {
            this.suggest = suggest;
        }

        /// <inheritdoc/>
        public IEnumerable<string?> GetSuggestions(ParseResult? parseResult = null, string? textToMatch = null)
        {
            return this.suggest(parseResult, textToMatch);
        }
    }
}