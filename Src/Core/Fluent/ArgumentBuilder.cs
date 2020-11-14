// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Saves all state necessary to build an <see cref="Argument"/>.
    /// </summary>
    /// <typeparam name="T">Type of the argument.</typeparam>
    /// <threadsafety static="true" instance="false"/>
    public sealed class ArgumentBuilder<T>
        : IArgumentBuilder
    {
        /// <summary>
        ///     Name of the argument.
        /// </summary>
        private string name;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgumentBuilder{T}"/> class.
        /// </summary>
        /// <param name="name">Name of the argument. It cannot be <see langword="null"/> or whitespace-only.</param>
        /// <exception cref="ArgumentException"><paramref name="name"/> is <see langword="null"/> or composed of only whitespaces.</exception>
        public ArgumentBuilder(string name)
        {
            this.name = Validations.IsValidArgumentName(name) ? name : throw Exceptions.BuildArgumentInvalidArgumentName(nameof(name));
        }

        /// <summary>
        ///     Gets or sets the name of the argument.
        /// </summary>
        /// <value>The name of the argument. It cannot be <see langword="null"/> or whitespace-only.</value>
        /// <exception cref="ArgumentException"><paramref name="value"/> is <see langword="null"/> or composed of only whitespaces.</exception>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = Validations.IsValidArgumentName(value) ? value : throw Exceptions.BuildArgumentInvalidArgumentName(nameof(value));
            }
        }

        /// <summary>
        ///     Gets or sets the argument description.
        /// </summary>
        /// <value>The argument description.</value>
        public string? Description { get; set; }

        /// <summary>
        ///     Gets or sets the argument arity.
        /// </summary>
        /// <value>The argument arity.</value>
        [CLSCompliant(false)]
        public IArgumentArity? Arity { get; set; }

        /// <summary>
        ///     Gets or sets the default value factory to be used for the argument.
        /// </summary>
        /// <value>The default value factory to be used for the argument.</value>
        /// <remarks>The factory will only be used if <see cref="Parser"/> is set to <see langword="null"/>.</remarks>
        public Func<T>? DefaultFactory { get; set; }

        /// <summary>
        ///     Gets or sets the parser to be used for the argument.
        /// </summary>
        /// <value>The parser to be used for the argument.</value>
        [CLSCompliant(false)]
        public ParseArgument<T>? Parser { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the <see cref="Parser"/> will be used as the default factory.
        /// </summary>
        /// <value><see langword="true"/> if the <see cref="Parser"/> will be used as the default factory; <see langword="false"/> otherwise. The default is <see langword="false"/>.</value>
        public bool UseParserAsDefaultFactory { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the argument will be hidden in the command help.
        /// </summary>
        /// <value><see langword="true"/> if the argument will be hidden in the command help; <see langword="false"/> otherwise. The default is <see langword="false"/>.</value>
        public bool IsHidden { get; set; }

        /// <summary>
        ///     Gets the collection of suggestion sources.
        /// </summary>
        /// <value>Collection of suggestion sources.</value>
        [CLSCompliant(false)]
        public SuggestionSourceCollection Suggestions { get; } = new SuggestionSourceCollection();

        /// <summary>
        ///     Gets the collection of argument validators.
        /// </summary>
        /// <value>Collection of argument validators.</value>
        [CLSCompliant(false)]
        public ArgumentValidatorCollection Validators { get; } = new ArgumentValidatorCollection();

        /// <summary>
        ///     Gets the collection of additional argument build configurations.
        /// </summary>
        /// <value>Collection of additional argument build configurations.</value>
        /// <remarks>The configurations are actions to be made onto the created argument before returning it by <see cref="Build"/>.</remarks>
        [CLSCompliant(false)]
        public ArgumentConfigurationCollection BuildConfigurations { get; } = new ArgumentConfigurationCollection();

        /// <inheritdoc/>
        [CLSCompliant(false)]
        public Argument Build()
        {
            var newArgument = this.Parser is null
                ? this.DefaultFactory is null ? new Argument<T>(this.Name) : new Argument<T>(this.Name, this.DefaultFactory)
                : new Argument<T>(this.Name, this.Parser, this.UseParserAsDefaultFactory);

            newArgument.Description = this.Description;
            newArgument.Arity = this.Arity!;
            newArgument.IsHidden = this.IsHidden;

            foreach (var suggestion in this.Suggestions)
            {
                newArgument.Suggestions.Add(suggestion);
            }

            foreach (var validator in this.Validators)
            {
                newArgument.AddValidator(validator);
            }

            foreach (var configuration in this.BuildConfigurations)
            {
                configuration(newArgument);
            }

            return newArgument;
        }
    }
}