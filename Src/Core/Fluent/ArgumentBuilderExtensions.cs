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
    ///     Fluent extensions for <see cref="ArgumentBuilder{T}"/>.
    /// </summary>
    /// <threadsafety static="false"/>
    public static class ArgumentBuilderExtensions
    {
        /// <summary>
        ///     Sets the <see cref="ArgumentBuilder{T}.Name"/> in the argument builder.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="name">Name of the argument. It cannot be <see langword="null"/> or whitespace-only.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentException">The name of the argument is <see langword="null"/> or composed of only whitespaces.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<T> SetName<T>(this ArgumentBuilder<T> builder, string name)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Name = Validations.IsValidArgumentName(name) ? name : throw Exceptions.BuildArgumentInvalidArgumentName(nameof(name));
            return builder;
        }

        /// <summary>
        ///     Sets the <see cref="ArgumentBuilder{T}.Description"/> in the argument builder.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="description">Description of the argument.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<T> SetDescription<T>(this ArgumentBuilder<T> builder, string? description)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Description = description;
            return builder;
        }

        /// <summary>
        ///     Sets the <see cref="ArgumentBuilder{T}.Arity"/> in the argument builder.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="arity">Arity of the argument.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static ArgumentBuilder<T> SetArity<T>(this ArgumentBuilder<T> builder, IArgumentArity? arity)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Arity = arity;
            return builder;
        }

        /// <summary>
        ///     Sets the <see cref="ArgumentBuilder{T}.Parser"/> in the argument builder.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="parser">The parser to be used for the argument.</param>
        /// <param name="useAsDefaultFactory"><see langword="true"/> if the parser will be used as a default value factory; <see langword="false"/> otherwise. The default is <see langword="false"/>.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static ArgumentBuilder<T> ParseWith<T>(this ArgumentBuilder<T> builder, ParseArgument<T> parser, bool useAsDefaultFactory = false)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Parser = parser;
            builder.UseParserAsDefaultFactory = useAsDefaultFactory;
            return builder;
        }

        /// <summary>
        ///     Sets the <see cref="ArgumentBuilder{T}.DefaultFactory"/> in the argument builder.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="defaultFactory">Default value factory to be used for the argument.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<T> GetDefaultFrom<T>(this ArgumentBuilder<T> builder, Func<T> defaultFactory)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .DefaultFactory = defaultFactory;
            return builder;
        }

        /// <summary>
        ///     Sets whether the argument will be hidden in the command help.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="value"><see langword="true"/> if the argument will be hidden in the command help; <see langword="false"/> otherwise. The default is <see langword="true"/>.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<T> Hide<T>(this ArgumentBuilder<T> builder, bool value = true)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .IsHidden = value;
            return builder;
        }

        /// <summary>
        ///     Adds a suggestion source to <see cref="ArgumentBuilder{T}.Suggestions"/>.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="suggestionSource">Suggestion source to add to the argument.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="suggestionSource"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static ArgumentBuilder<T> AddSuggestions<T>(this ArgumentBuilder<T> builder, ISuggestionSource suggestionSource)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Suggestions.Add(suggestionSource ?? throw Exceptions.BuildArgumentNull(nameof(suggestionSource)));
            return builder;
        }

        /// <summary>
        ///     Adds a suggestion source built from the parameters to <see cref="ArgumentBuilder{T}.Suggestions"/>.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="suggestions">Array of suggestions.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<T> AddSuggestions<T>(this ArgumentBuilder<T> builder, params string[] suggestions)
        {
            return builder.AddSuggestions((IEnumerable<string>)suggestions);
        }

        /// <summary>
        ///     Adds a suggestion source from a collection to <see cref="ArgumentBuilder{T}.Suggestions"/>.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="suggestions">Collection of suggestions.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<T> AddSuggestions<T>(this ArgumentBuilder<T> builder, IEnumerable<string> suggestions)
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
        ///     Adds a suggestion source from a delegate to <see cref="ArgumentBuilder{T}.Suggestions"/>.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="suggestionsSource">Delegate that generates suggestions.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="suggestionsSource"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static ArgumentBuilder<T> AddSuggestions<T>(this ArgumentBuilder<T> builder, SuggestDelegate suggestionsSource)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddSuggestions(new AnonymousSuggestionSource(suggestionsSource ?? throw Exceptions.BuildArgumentNull(nameof(suggestionsSource))));
        }

        /// <summary>
        ///     Adds a validator to <see cref="ArgumentBuilder{T}.Validators"/>.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="validator">Validator to use in the argument.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="validator"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static ArgumentBuilder<T> AddValidator<T>(this ArgumentBuilder<T> builder, ValidateSymbol<ArgumentResult> validator)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .Validators.Add(validator ?? throw Exceptions.BuildArgumentNull(nameof(validator)));
            return builder;
        }

        /// <summary>
        ///     Adds a build configuration to <see cref="ArgumentBuilder{T}.BuildConfigurations"/>.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="configuration">Build configuration to add for the argument. Configurations are actions to be made onto the created argument while building.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="configuration"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static ArgumentBuilder<T> AddBuildConfiguration<T>(this ArgumentBuilder<T> builder, Action<Argument> configuration)
        {
            (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .BuildConfigurations.Add(configuration ?? throw Exceptions.BuildArgumentNull(nameof(configuration)));
            return builder;
        }

        /// <summary>
        ///     Limits the accepted values for the argument to the provided ones.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="values">Values to accept.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<T> LimitTo<T>(this ArgumentBuilder<T> builder, params string[] values)
        {
            return builder.LimitTo((IEnumerable<string>)values);
        }

        /// <summary>
        ///     Limits the accepted values for the argument to the ones in the provided collection.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <param name="values">Values to accept.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<T> LimitTo<T>(this ArgumentBuilder<T> builder, IEnumerable<string> values)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(argument => argument.FromAmong((values ?? Enumerable.Empty<string>()).ToArray()));
        }

        /// <summary>
        ///     Limits the accepted values to existing <see cref="FileInfo"/>.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<FileInfo> ExistingOnly(this ArgumentBuilder<FileInfo> builder)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(argument => ((Argument<FileInfo>)argument).ExistingOnly());
        }

        /// <summary>
        ///     Limits the accepted values to existing <see cref="DirectoryInfo"/>.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<DirectoryInfo> ExistingOnly(this ArgumentBuilder<DirectoryInfo> builder)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(argument => ((Argument<DirectoryInfo>)argument).ExistingOnly());
        }

        /// <summary>
        ///     Limits the accepted values to existing <see cref="FileSystemInfo"/>.
        /// </summary>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<FileSystemInfo> ExistingOnly(this ArgumentBuilder<FileSystemInfo> builder)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(argument => ((Argument<FileSystemInfo>)argument).ExistingOnly());
        }

        /// <summary>
        ///     Limits the accepted values to existing <see cref="FileSystemInfo"/>.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<T> ExistingOnly<T>(this ArgumentBuilder<T> builder)
            where T : IEnumerable<FileSystemInfo>
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(argument => ((Argument<T>)argument).ExistingOnly());
        }

        /// <summary>
        ///     Limits the accepted values to legal paths.
        /// </summary>
        /// <typeparam name="T">Type of the argument.</typeparam>
        /// <param name="builder">Source builder.</param>
        /// <returns><paramref name="builder"/>, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
        public static ArgumentBuilder<T> LegalFilePathsOnly<T>(this ArgumentBuilder<T> builder)
        {
            return (builder ?? throw Exceptions.BuildArgumentNull(nameof(builder)))
                .AddBuildConfiguration(argument => argument.LegalFilePathsOnly());
        }
    }
}