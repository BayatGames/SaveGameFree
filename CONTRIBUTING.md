# Contributing to Save Game Free

:tada: First off, thanks for taking the time to contribute! :tada:

The following is a set of guidelines for contributing to Save Game Free. These are mostly guidelines, not rules. Use your best judgment, and feel free to propose changes to this document in a pull request.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [How Can I Contribute?](#how-can-i-contribute)
  - [Reporting Bugs](#reporting-bugs)
  - [Suggesting Enhancements](#suggesting-enhancements)
  - [Your First Code Contribution](#your-first-code-contribution)
  - [Pull Requests](#pull-requests)
- [Style Guides](#style-guides)
  - [Git Commit Messages](#git-commit-messages)
  - [C# Style Guide](#c-style-guide)
  - [Documentation Style Guide](#documentation-style-guide)
- [Additional Notes](#additional-notes)

## Code of Conduct

This project and everyone participating in it is governed by the [Code of Conduct](CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code.

## How Can I Contribute?

### Reporting Bugs

Before creating bug reports, please check the [issue list](https://github.com/BayatGames/SaveGameFree/issues) as you might find out that you don't need to create one. When you are creating a bug report, please include as many details as possible.

**How Do I Submit A Good Bug Report?**

Bugs are tracked as [GitHub issues](https://github.com/BayatGames/SaveGameFree/issues). Create an issue and provide the following information:

- **Use a clear and descriptive title** for the issue to identify the problem.
- **Describe the exact steps which reproduce the problem** in as many details as possible.
- **Provide specific examples to demonstrate the steps**.
- **Describe the behavior you observed after following the steps** and point out what exactly is the problem with that behavior.
- **Explain which behavior you expected to see instead and why.**
- **Include Unity version, platform, and SaveGameFree version.**

### Suggesting Enhancements

Enhancement suggestions are tracked as [GitHub issues](https://github.com/BayatGames/SaveGameFree/issues). Create an issue and provide the following information:

- **Use a clear and descriptive title** for the issue to identify the suggestion.
- **Provide a step-by-step description of the suggested enhancement** in as many details as possible.
- **Provide specific examples to demonstrate the steps** or mockups.
- **Describe the current behavior** and **explain which behavior you expected to see instead** and why.
- **Explain why this enhancement would be useful** to most Save Game Free users.

### Your First Code Contribution

Unsure where to begin? You can start by looking through `good-first-issue` and `help-wanted` issues:

- **Good first issues** - issues which should only require a few lines of code, and a test or two.
- **Help wanted issues** - issues which should be a bit more involved than `good-first-issue` issues.

### Pull Requests

The process described here has several goals:

- Maintain Save Game Free's quality
- Fix problems that are important to users
- Engage the community in working toward the best possible Save Game Free
- Enable a sustainable system for Save Game Free's maintainers to review contributions

Please follow these steps to have your contribution considered by the maintainers:

1. **Follow the [style guides](#style-guides)**
2. **Follow the [commit message convention](#git-commit-messages)**
3. **Include tests** when adding new features or fixing bugs
4. **Update documentation** to reflect changes
5. **Verify all tests pass** before submitting
6. **Create a meaningful PR title** following conventional commits format

## Style Guides

### Git Commit Messages

This project follows [Conventional Commits](https://www.conventionalcommits.org/) specification.

**Format:**
```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

**Types:**
- `feat`: A new feature
- `fix`: A bug fix
- `docs`: Documentation only changes
- `style`: Changes that do not affect the meaning of the code (white-space, formatting, etc)
- `refactor`: A code change that neither fixes a bug nor adds a feature
- `perf`: A code change that improves performance
- `test`: Adding missing tests or correcting existing tests
- `build`: Changes that affect the build system or external dependencies
- `ci`: Changes to CI configuration files and scripts
- `chore`: Other changes that don't modify src or test files

**Examples:**
```
feat: add async save and load methods

Add SaveAsync and LoadAsync methods for non-blocking file operations.
This improves performance when saving large files.

Closes #123
```

```
fix: prevent stream resource leaks

Implement using statements for all stream operations to ensure
proper resource disposal.
```

```
docs: update README with async examples

Add code examples showing how to use the new async methods.
```

**Breaking Changes:**

Commits that introduce breaking changes should include `BREAKING CHANGE:` in the footer:

```
refactor!: replace hardcoded password with device-specific generation

BREAKING CHANGE: Default encryption password is now device-specific.
Users relying on the default password need to set a custom password.
```

### C# Style Guide

- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use modern C# features (C# 8.0+): null-coalescing operators, using statements, async/await
- Use `PascalCase` for public members and `camelCase` for private fields with `m_` prefix
- Always use XML documentation comments for public APIs
- Prefer `string interpolation` over `String.Format()`
- Use `using` statements for `IDisposable` resources
- Include comprehensive error handling with meaningful exception messages

**Example:**
```csharp
/// <summary>
/// Saves data asynchronously using the identifier.
/// </summary>
/// <param name="identifier">Unique identifier for the save data.</param>
/// <param name="objToSave">The object to save.</param>
/// <typeparam name="T">The type of object to save.</typeparam>
public static async Task SaveAsync<T>(string identifier, T objToSave)
{
    ValidateIdentifier(identifier);

    try
    {
        using (var stream = new MemoryStream())
        {
            // Implementation
        }
    }
    catch (Exception ex)
    {
        string errorMsg = $"Failed to save data with identifier '{identifier}': {ex.Message}";
        throw new InvalidOperationException(errorMsg, ex);
    }
}
```

### Documentation Style Guide

- Use [Markdown](https://guides.github.com/features/mastering-markdown/) for all documentation
- Keep line length to 100 characters for better readability
- Use code blocks with language specification
- Include examples for all public APIs
- Follow [Keep a Changelog](https://keepachangelog.com/en/1.1.0/) format for CHANGELOG.md
- Update CHANGELOG.md with every change following conventional commit types

## Additional Notes

### Issue and Pull Request Labels

This section lists the labels we use to help us track and manage issues and pull requests.

- `bug` - Issues that are bugs
- `enhancement` - Issues that are feature requests
- `documentation` - Issues or PRs related to documentation
- `good-first-issue` - Good for newcomers
- `help-wanted` - Extra attention is needed
- `question` - Further information is requested
- `wontfix` - This will not be worked on
- `duplicate` - This issue or pull request already exists
- `invalid` - This doesn't seem right

### Adding Content to Wiki

You can add content to the [Wiki](https://github.com/BayatGames/SaveGameFree/wiki), including examples and tutorials. All contributions are welcome!

## License

MIT License

---

Made with :heart: by [Bayat](https://bayat.io)
