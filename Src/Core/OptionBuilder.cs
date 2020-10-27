// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Linq;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     Saves all state necessary to build an <see cref="Option"/>.
    /// </summary>
    /// <typeparam name="T">Type of the option.</typeparam>
    /// <threadsafety static="true" instance="false"/>
    public sealed class OptionBuilder<T>
        : IOptionBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OptionBuilder{T}"/> class.
        /// </summary>
        /// <param name="aliases">Aliases of the option. At least one must be defined. They cannot be <see langword="null"/>, empty, or contain whitespaces only, after removing the prefixes.</param>
        /// <exception cref="ArgumentException">Either <paramref name="aliases"/> is empty or contains an invalid alias.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="aliases"/> is <see langword="null"/>.</exception>
        public OptionBuilder(params string[] aliases)
        {
            this.Aliases = new AliasCollection(aliases);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OptionBuilder{T}"/> class.
        /// </summary>
        /// <param name="aliases">Aliases of the option. At least one must be defined. They cannot be <see langword="null"/>, empty, or contain whitespaces only, after removing the prefixes.</param>
        /// <exception cref="ArgumentException">Either <paramref name="aliases"/> is empty or contains an invalid alias.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="aliases"/> is <see langword="null"/>.</exception>
        public OptionBuilder(IEnumerable<string> aliases)
            : this(aliases is null ? throw Exceptions.BuildArgumentNull(nameof(aliases)) : aliases.ToArray())
        {
        }

        /// <summary>
        ///     Gets the collection of aliases for the option.
        /// </summary>
        /// <value>Collection of aliases for the option.</value>
        public AliasCollection Aliases { get; }

        /// <summary>
        ///     Gets or sets the option description.
        /// </summary>
        /// <value>The option description.</value>
        public string? Description { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the option is required.
        /// </summary>
        /// <value><see langword="true"/> if the option is required; <see langword="false"/> otherwise. The default is <see langword="false"/>.</value>
        public bool IsRequired { get; set; }

        /// <summary>
        ///     Gets or sets the option's argument arity.
        /// </summary>
        /// <value>The option's argument arity.</value>
        [CLSCompliant(false)]
        public IArgumentArity? Arity { get; set; }

        /// <summary>
        ///     Gets or sets the default value factory to be used for the option's argument.
        /// </summary>
        /// <value>The default value factory to be used for the option's argument.</value>
        /// <remarks>The factory will only be used if <see cref="Parser"/> is set to <see langword="null"/>.</remarks>
        public Func<T>? DefaultFactory { get; set; }

        /// <summary>
        ///     Gets or sets the parser to be used for the option's argument.
        /// </summary>
        /// <value>The parser to be used for the option's argument.</value>
        [CLSCompliant(false)]
        public ParseArgument<T>? Parser { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the <see cref="Parser"/> will be used as the default factory.
        /// </summary>
        /// <value><see langword="true"/> if the <see cref="Parser"/> will be used as the default factory; <see langword="false"/> otherwise. The default is <see langword="false"/>.</value>
        public bool UseParserAsDefaultFactory { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the option will be hidden in the command help.
        /// </summary>
        /// <value><see langword="true"/> if the option will be hidden in the command help; <see langword="false"/> otherwise. The default is <see langword="false"/>.</value>
        public bool IsHidden { get; set; }

        /// <summary>
        ///     Gets the collection of suggestion sources.
        /// </summary>
        /// <value>Collection of suggestion sources.</value>
        [CLSCompliant(false)]
        public SuggestionSourceCollection Suggestions { get; } = new SuggestionSourceCollection();

        /// <summary>
        ///     Gets the collection of option validators.
        /// </summary>
        /// <value>Collection of option validators.</value>
        [CLSCompliant(false)]
        public OptionValidatorCollection OptionValidators { get; } = new OptionValidatorCollection();

        /// <summary>
        ///     Gets the collection of argument validators.
        /// </summary>
        /// <value>Collection of argument validators.</value>
        [CLSCompliant(false)]
        public ArgumentValidatorCollection ArgumentValidators { get; } = new ArgumentValidatorCollection();

        /// <summary>
        ///     Gets the collection of additional option build configurations.
        /// </summary>
        /// <value>Collection of additional option build configurations.</value>
        /// <remarks>The configurations are actions to be made onto the created option before returning it by <see cref="Build"/>.</remarks>
        [CLSCompliant(false)]
        public OptionConfigurationCollection BuildConfigurations { get; } = new OptionConfigurationCollection();

        /// <inheritdoc/>
        [CLSCompliant(false)]
        public Option Build()
        {
            var newOption = this.Parser is null
                ? this.DefaultFactory is null ? new Option<T>(this.Aliases.ToArray()) : new Option<T>(this.Aliases.ToArray(), this.DefaultFactory)
                : new Option<T>(this.Aliases.ToArray(), this.Parser, this.UseParserAsDefaultFactory);

            newOption.Description = this.Description;
            newOption.IsRequired = this.IsRequired;
            newOption.Argument.Arity = this.Arity!;
            newOption.IsHidden = this.IsHidden;

            foreach (var suggestion in this.Suggestions)
            {
                newOption.Argument.Suggestions.Add(suggestion);
            }

            foreach (var validator in this.OptionValidators)
            {
                newOption.AddValidator(validator);
            }

            foreach (var validator in this.ArgumentValidators)
            {
                newOption.Argument.AddValidator(validator);
            }

            foreach (var configuration in this.BuildConfigurations)
            {
                configuration(newOption);
            }

            return newOption;
        }
    }
}