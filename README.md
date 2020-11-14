# WiZaRo's Command Line Interface Library

<!--{Badges}-->

<!--This section lets the people know what the project can do specifically.-->
Support library to build CLI applications in .Net.

## Features
<!--This section contains a list of the features this project provides, including differentiating factors to alternatives.-->
- WiZaRo.CommandLine.Fluent: Provides fluent extensions for [System.CommandLine].

See the latest changes in the [Change Log].

<!--## Background-->
<!--This section contains the background for the project, including context and links to references that might be needed to understand it.-->
<!--{Background}-->

## Usage
### Prerequisites
<!--This section details if a project runs only in a specific context like a particular programming language version or operating system, or has dependencies that have to be installed manually.-->
Coming soon…

### Installation
<!--This section is the full guidance to install the project. Listing specific steps removes ambiguity and gets people to using the project as quickly as possible.-->
Coming soon…

### Examples
<!--This section shows examples of usage and the expected output if possible. It's helpful to have the smallest example of usage that can be demonstrated inline, while providing links to more sophisticated examples if they are too long to include reasonably.-->
Buildable examples can be found in the [Xmpl] directory.

#### WiZaRo.CommandLine.Fluent
The static class `WiZaRo.CommandLine.Fluent.Start` contains the entry points to the fluent API.

- The method `DefineCommandLine(Action<CommandBuilder>)` allows to define a command line API explicitly in a fluent way, i.e.:
  ```C#
  Start.DefineCommandLine(rootBuilder => rootBuilder
    .AddCommand("HelloWorld", commandBuilder => commandBuilder
        .SetDescription("Salutes the globe.")
        .UseHandler(CommandHandler.Create(HelloWorld)))
    .AddCommand("Show", commandBuilder => commandBuilder
        .SetDescription("Shows a message prettily.")
        .AddOption<string>(new[] { "--message", "-m" },   optionBuilder => optionBuilder
            .Require())
        .UseHandler(CommandHandler.Create<string, IFormatter>(Show))))
  ```
  The previous example creates two commands:
  - The `HelloWorld` command with no options managed by the `HelloWorld()` method; and
  - The `Show` command with a required `--message` option and managed by the `Show(string, IFormatter)` method.

- The methods `DefineCommandLineFrom` allows to define a command line API from a pre-existing or generated `RootCommand`.

A complete example can be found at [Fluent Example].

### Documentation
<!--This section provides a link to the project's documentation.-->
Coming soon…

### Support
<!--This section tells people where they can go to for help. It can be in the combination of an issue tracker, a chat room, an email address, etc.-->
Open a new issue at [Issues].

## Roadmap
<!--This section contains ideas for releases in the future, or an indication if development has slowed down or stopped completely. Someone may choose to fork your project or volunteer to step in as a maintainer or owner, allowing it to keep going. An explicit request for maintainers can be made.-->
- Create a Nuget package.
- Add prerequisites and intallation guidance.
- Add documentation.

## Contributing
Please read our [Contribution Guidelines] for details on our code of conduct, how to set your environment and the process for submitting pull requests to us.

## Authors
<!--This section contains appreciation to those who have contributed to the project.-->
[Wilhelm Zapiain]

## Acknowledgments
<!-- This sections contains any acknowledgments that need to be made.-->
Thanks to all contributors at [System.CommandLine].

## License
[MIT] - [Summary]

<!--Links-->
[Change Log]: ./CHANGELOG.md
[Contribution Guidelines]: ./CONTRIBUTING.md
[Fluent Example]: ./Xmpl/Core/Fluent/FluentExample.cs
[Issues]: https://github.com/wizaro/CommandLine/issues
[MIT]: ./LICENSE
[System.CommandLine]: https://github.com/dotnet/command-line-api
[Summary]: https://choosealicense.com/licenses/mit/ "Choose a License's MIT Summary"
[Wilhelm Zapiain]: https://github.com/wilhelmzapiain
[Xmpl]: ./Xmpl "Examples"