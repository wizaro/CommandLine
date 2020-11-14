// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.CommandLine.Suggestions;
using System.IO;
using System.Linq;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Fluent extensions for <see cref="OptionBuilder{T}"/>.
    /// </summary>
    /// <threadsafety static="false"/>
    public static class OptionBuilderExtensions
    {
        /// <summary>
        ///     Adds aliases to <see cref="OptionBuilder{T}.Aliases"/>.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="aliases">Aliases to add to the option.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentException"><paramref name="aliases"/> contains an invalid alias.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> AddAliases<T>(this OptionBuilder<T> builder, params string[] aliases)
        {
            return builder.AddAliases((IEnumerable<string>)aliases);
        }

        /// <summary>
        ///     Adds aliases to <see cref="OptionBuilder{T}.Aliases"/>.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="aliases">Aliases to add to the option.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentException"><paramref name="aliases"/> contains an invalid alias.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> AddAliases<T>(this OptionBuilder<T> builder, IEnumerable<string> aliases)
        {
            if (builder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(builder));
            }

            foreach (var alias in aliases ?? Enumerable.Empty<string>())
            {
                try
                {
                    builder.Aliases.Add(alias);
                }
                catch (ArgumentException exArg)
                {
                    throw Exceptions.BuildArgumentContainsInvalidAlias(nameof(aliases), exArg);
                }
            }

            return builder;
        }

        /// <summary>
        ///     Sets the <see cref="OptionBuilder{T}.Description"/> in the option builder.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="description">Description of the option.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> SetDescription<T>(this OptionBuilder<T> builder, string description)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Description = description ?? throw Exceptions.BuildArgumentNull(nameof(description));
            return builder;
        }

        /// <summary>
        ///     Sets whether the option will be required.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="value"><see langword="true"/> if the option will be required; <see langword="false"/> otherwise. The default is <see langword="true"/>.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> Require<T>(this OptionBuilder<T> builder, bool value = true)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .IsRequired = value;
            return builder;
        }

        /// <summary>
        ///     Sets the <see cref="OptionBuilder{T}.Arity"/> in the option builder.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="arity">Arity of the option's argument.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static OptionBuilder<T> SetArity<T>(this OptionBuilder<T> builder, IArgumentArity? arity)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Arity = arity;
            return builder;
        }

        /// <summary>
        ///     Sets the <see cref="OptionBuilder{T}.Parser"/> in the option builder.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="parser">The parser to be used for the option's argument.</param>
        /// <param name="useAsDefaultFactory"><see langword="true"/> if the parser will be used as a default value factory; <see langword="false"/> otherwise. The default is <see langword="false"/>.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static OptionBuilder<T> ParseWith<T>(this OptionBuilder<T> builder, ParseArgument<T> parser, bool useAsDefaultFactory = false)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Parser = parser ?? throw Exceptions.BuildArgumentNull(nameof(parser));
            builder.UseParserAsDefaultFactory = useAsDefaultFactory;
            return builder;
        }

        /// <summary>
        ///     Sets the <see cref="OptionBuilder{T}.DefaultFactory"/> in the option builder.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="defaultFactory">Default value factory to be used for the option's argument.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> GetDefaultFrom<T>(this OptionBuilder<T> builder, Func<T> defaultFactory)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .DefaultFactory = defaultFactory ?? throw Exceptions.BuildArgumentNull(nameof(defaultFactory));
            return builder;
        }

        /// <summary>
        ///     Sets whether the option will be hidden in the command help.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="value"><see langword="true"/> if the option will be hidden in the command help; <see langword="false"/> otherwise. The default is <see langword="true"/>.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> Hide<T>(this OptionBuilder<T> builder, bool value = true)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .IsHidden = value;
            return builder;
        }

        /// <summary>
        ///     Adds a suggestion source to <see cref="OptionBuilder{T}.Suggestions"/>.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="suggestionSource">Suggestion source to add to the option.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="suggestionSource"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static OptionBuilder<T> AddSuggestions<T>(this OptionBuilder<T> builder, ISuggestionSource suggestionSource)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Suggestions.Add(suggestionSource ?? throw Exceptions.BuildArgumentNull(nameof(suggestionSource)));
            return builder;
        }

        /// <summary>
        ///     Adds a suggestion source built from the parameters to <see cref="OptionBuilder{T}.Suggestions"/>.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="suggestions">Array of suggestions.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> AddSuggestions<T>(this OptionBuilder<T> builder, params string[] suggestions)
        {
            return builder.AddSuggestions((IEnumerable<string>)suggestions);
        }

        /// <summary>
        ///     Adds a suggestion source from a collection to <see cref="OptionBuilder{T}.Suggestions"/>.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="suggestions">Collection of suggestions.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> AddSuggestions<T>(this OptionBuilder<T> builder, IEnumerable<string> suggestions)
        {
            if (builder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(builder));
            }

            var suggestionsCopy = suggestions?.ToArray() ?? Array.Empty<string>();
            return builder.AddSuggestions(GetSuggestionsFromCollection);

            IEnumerable<string> GetSuggestionsFromCollection(ParseResult? parseResult, string? textToMatch)
            {
                return suggestionsCopy;
            }
        }

        /// <summary>
        ///     Adds a suggestion source from a delegate to <see cref="OptionBuilder{T}.Suggestions"/>.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="suggestionsSource">Delegate that generates suggestions.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="suggestionsSource"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static OptionBuilder<T> AddSuggestions<T>(this OptionBuilder<T> builder, SuggestDelegate suggestionsSource)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddSuggestions(new AnonymousSuggestionSource(suggestionsSource ?? throw Exceptions.BuildArgumentNull(nameof(suggestionsSource))));
        }

        /// <summary>
        ///     Adds a validator to <see cref="OptionBuilder{T}.OptionValidators"/>.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="validator">Validator to use in the option.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="validator"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static OptionBuilder<T> AddValidator<T>(this OptionBuilder<T> builder, ValidateSymbol<OptionResult> validator)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .OptionValidators.Add(validator ?? throw Exceptions.BuildArgumentNull(nameof(validator)));
            return builder;
        }

        /// <summary>
        ///     Adds a validator to <see cref="OptionBuilder{T}.ArgumentValidators"/>.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="validator">Validator to use in the option's argument.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="validator"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static OptionBuilder<T> AddArgumentValidator<T>(this OptionBuilder<T> builder, ValidateSymbol<ArgumentResult> validator)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .ArgumentValidators.Add(validator ?? throw Exceptions.BuildArgumentNull(nameof(validator)));
            return builder;
        }

        /// <summary>
        ///     Adds a build configuration to <see cref="OptionBuilder{T}.BuildConfigurations"/>.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="configuration">Build configuration to add for the option. Configurations are actions to be made onto the created option while building.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="configuration"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static OptionBuilder<T> AddBuildConfiguration<T>(this OptionBuilder<T> builder, Action<Option> configuration)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .BuildConfigurations.Add(configuration ?? throw Exceptions.BuildArgumentNull(nameof(configuration)));
            return builder;
        }

        /// <summary>
        ///     Limits the accepted values for the option's argument to the provided ones.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="values">Values to accept.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> LimitTo<T>(this OptionBuilder<T> builder, params string[] values)
        {
            return builder.LimitTo((IEnumerable<string>)values);
        }

        /// <summary>
        ///     Limits the accepted values for the option's argument to the ones in the provided collection.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="values">Values to accept.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> LimitTo<T>(this OptionBuilder<T> builder, IEnumerable<string> values)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(option => option.FromAmong((values ?? Enumerable.Empty<string>()).ToArray()));
        }

        /// <summary>
        ///     Limits the accepted values to existing <see cref="FileInfo"/>.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<FileInfo> ExistingOnly(this OptionBuilder<FileInfo> builder)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(option => ((Option<FileInfo>)option).ExistingOnly());
        }

        /// <summary>
        ///     Limits the accepted values to existing <see cref="DirectoryInfo"/>.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<DirectoryInfo> ExistingOnly(this OptionBuilder<DirectoryInfo> builder)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(option => ((Option<DirectoryInfo>)option).ExistingOnly());
        }

        /// <summary>
        ///     Limits the accepted values to existing <see cref="FileSystemInfo"/>.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<FileSystemInfo> ExistingOnly(this OptionBuilder<FileSystemInfo> builder)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(option => ((Option<FileSystemInfo>)option).ExistingOnly());
        }

        /// <summary>
        ///     Limits the accepted values to existing <see cref="FileSystemInfo"/>.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> ExistingOnly<T>(this OptionBuilder<T> builder)
            where T : IEnumerable<FileSystemInfo>
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(option => ((Option<T>)option).ExistingOnly());
        }

        /// <summary>
        ///     Limits the accepted values to legal paths.
        /// </summary>
        /// <typeparam name="T">Type of the option.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static OptionBuilder<T> LegalFilePathsOnly<T>(this OptionBuilder<T> builder)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(option => option.LegalFilePathsOnly());
        }
    }
}